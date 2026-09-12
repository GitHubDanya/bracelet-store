import argparse
from dataclasses import dataclass
import inquirer
import json
import psycopg2
from psycopg2 import sql
from psycopg2.extras import Json
from psycopg2.extensions import connection
from enum import Enum


class UserAction(Enum):
    ADD_ITEM = 1
    GET_SCHEMA = 2
    REMOVE_ITEM = 3


class ColumnStatus(Enum):
    REQUIRED = 1
    OPTIONAL = 2
    SYSTEM_MANAGED = 3


@dataclass
class ColumnInfo:
    name: str
    data_type: str
    is_nullable: bool
    has_default: bool
    status: ColumnStatus

    def to_string(self) -> str:
        nullable_str = " (nullable)" if self.is_nullable else ""
        default_str = " (default)" if self.has_default else ""

        return f"{self.name} [{self.data_type}]{nullable_str}{default_str} {self.status.name}"

def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser("Tool for talking with a postgres database.")

    parser.add_argument("user", help="PostgreSQL Username")
    parser.add_argument("password", help="PostgreSQL Password")
    parser.add_argument("database", help="PostgreSQL Database name")
    parser.add_argument("-i", "--input", help="Path to input file for automated additions")

    return parser.parse_args()


def connect_to_db(user: str, password: str, dbname: str) -> connection | None:
    try:
        connection = psycopg2.connect(
            host="localhost",
            port="5432",
            dbname=dbname,
            user=user,
            password=password
        )

        print(f"Connected to {dbname}:{user}.\n")

        return connection

    except psycopg2.Error as error:
        print(f"Failed to connect to the database: {error}")


def get_tables(db_conn: connection):
    cursor = connection.cursor(db_conn)
    cursor.execute("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'")
    tables = cursor.fetchall()
    return [row[0] for row in tables]


def get_table_schema(db_conn: connection, table_name: str) -> list[ColumnInfo]:
    cursor = db_conn.cursor()

    query = """
        SELECT column_name, data_type, is_nullable, column_default,
            CASE
                WHEN is_identity = 'YES' OR is_generated != 'NEVER' OR column_default LIKE 'nextval%%'
                    THEN 'SYSTEM_MANAGED'
                WHEN is_nullable = 'NO' AND column_default IS NULL
                    THEN 'REQUIRED'
                ELSE 'OPTIONAL'
            END AS field_requirement
        FROM information_schema.columns
        WHERE table_schema = 'public' AND table_name = %s
        ORDER BY ordinal_position;    
    """

    cursor.execute(query, (table_name,))
    columns = cursor.fetchall()

    return [
        ColumnInfo(
            name=row[0],
            data_type=row[1],
            is_nullable=(row[2] == 'YES'),
            has_default=(row[3] is not None),
            status=ColumnStatus[row[4]]
        )
        for row in columns
    ]


def get_user_selected_table(db_conn: connection) -> str | None:
    tables = get_tables(db_conn)
    questions = [
        inquirer.List(
            'target_table',
            message="Which table do you want to select?",
            choices=tables,
        )
    ]

    answers = inquirer.prompt(questions)

    if answers:
        return answers['target_table']
    return None


def get_user_action() -> UserAction | None:
    enum_choices = [(action.name.replace('_', ' ').capitalize(), action) for action in UserAction]

    questions = [
        inquirer.List(
            'action',
            message="Which action do you want to perform?",
            choices=enum_choices,
        )
    ]

    answers = inquirer.prompt(questions)

    if answers:
        return answers['action']
    return None


def get_inquirer_question_for_field(field: ColumnInfo):
    label = f"{field.name} [{field.data_type}]"
    if field.status == ColumnStatus.OPTIONAL: label += " [OPTIONAL]"

    if field.data_type == 'boolean':
        return inquirer.Confirm(field.name, message=f"Is {field.name} enabled?", default=True)
    else:
        return inquirer.Text(label, message=f"Enter {label}")


def prompt_user_insert(table_schema: list[ColumnInfo]) -> dict:
    questions = []

    print("\nPlease fill out these schema parameters: ")

    for field in table_schema:
        if field.status == ColumnStatus.SYSTEM_MANAGED:
            print(f'{field.name} is system managed, skipping...')
            continue
        questions.append(get_inquirer_question_for_field(field))

    answers = inquirer.prompt(questions)

    return answers


def query_raw_db_insert(db_connection: connection, table_name: str, data: dict):
    print(data)
    columns = list(data.keys())
    column_names = ", ".join(columns)
    placeholders = ", ".join(["%s"] * len(columns))
    values = [data[col] for col in columns]

    query = sql.SQL("INSERT INTO {} ({}) VALUES ({});").format(
        sql.Identifier(table_name),
        sql.SQL(', ').join(map(sql.Identifier, columns)),
        sql.SQL(', ').join(sql.Placeholder() * len(columns))
    )

    with db_connection.cursor() as cursor:
        cursor.execute(query, values)
        db_connection.commit()
        print(f"Successfully inserted row into database!")


def insert_user_prompt_into_db(db_connection: connection, table_name: str, prompt: dict):
    cleaned_data = {}

    for raw_key, value in prompt.items():
        clean_key = raw_key.split(' ')[0]

        if value == '' or value is None:
            continue

        if '[jsonb]' in raw_key:
            if isinstance(value, str):
                cleaned_data[clean_key] = Json(json.loads(value))
            else:
                cleaned_data[clean_key] = Json(value)

        elif '[ARRAY]' in raw_key:
            if isinstance(value, str):
                try:
                    parsed_list = json.loads(value)

                    cleaned_data[clean_key] = [
                        Json(item) if isinstance(item, dict) else item
                        for item in parsed_list
                    ]
                except json.JSONDecodeError:
                    cleaned_data[clean_key] = [item.strip() for item in value.split(',') if item.strip()]
            else:
                cleaned_data[clean_key] = value

        elif '[numeric]' in raw_key:
            cleaned_data[clean_key] = float(value) if '.' in str(value) else int(value)

        else:
            cleaned_data[clean_key] = value

    if not cleaned_data:
        print("No valid fields to insert.")
        return

    query_raw_db_insert(db_connection, table_name, cleaned_data)


def file_to_prompt(filepath: str, table_schema: list[ColumnInfo]) -> dict:
    file_content = {}
    result = {}

    with open(filepath, 'r') as file:
        for line in file:
            if ':' not in line: continue
            key, value = line.split(':', 1)
            file_content[key.strip()] = value.strip()

    for index, value in enumerate(table_schema):
        if str(index) not in file_content: continue
        key_name = f"{value.name} [{value.data_type}]"
        result[key_name] = file_content[str(index)]

    return result


def main():
    arguments = parse_args()
    print("\nConnecting to db...")
    db_connection = connect_to_db(arguments.user, arguments.password, arguments.database)
    if db_connection is None: return

    selected_table = get_user_selected_table(db_connection)
    if selected_table is None:
        print("Failed to fetch table.")
        return

    table_schema = get_table_schema(db_connection, selected_table)

    if arguments.input is not None:
        prompt = file_to_prompt(arguments.input, table_schema)
        insert_user_prompt_into_db(db_connection, selected_table, prompt)
        print(f"Inserted {arguments.input} into {selected_table}.")
        return

    selected_action = get_user_action()

    match selected_action:
        case UserAction.ADD_ITEM:
            prompt = prompt_user_insert(table_schema)
            print(prompt)
            insert_user_prompt_into_db(db_connection, selected_table, prompt)
        case UserAction.GET_SCHEMA:
            for i, column in enumerate(table_schema):
                print(f'{i}. {column.to_string()}')
            print('\nDone.')
        case _:
            print("nil")


if __name__ == "__main__":
    main()