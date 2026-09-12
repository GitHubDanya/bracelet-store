CREATE TABLE bracelets(
    id SERIAL PRIMARY KEY,
    available BOOLEAN NOT NULL DEFAULT true,
    price DECIMAL(10, 2) NOT NULL,
    thumbnail_urls TEXT[] NOT NULL DEFAULT '{}',
    name JSONB NOT NULL,
    description JSONB NOT NULL,
    materials JSONB NOT NULL,
    color JSONB NOT NULL
);