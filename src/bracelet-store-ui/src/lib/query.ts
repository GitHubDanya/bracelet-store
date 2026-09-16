export class QueryResult {
    static async fetch<T>(url: string, callback: (data: T) => void): Promise<T | null> {
        const res = await fetch(url);
        if (res.ok) {
            const data: T = await res.json();
            callback(data);
            return data;
        }
        return null;
    }
}