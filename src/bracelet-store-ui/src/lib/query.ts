export class QueryResult {
    static buildUrl(endpoint: string, params: Record<string, unknown> = {}): string {
        const activeParams = Object.fromEntries(
            Object.entries(params).filter(([_, val]) => val !== null && val !== undefined && val !== '')
        );
        const queryString = new URLSearchParams(activeParams as Record<string, string>).toString();
        return queryString ? `${endpoint}?${queryString}` : endpoint;
    }

    static async fetch<T>(
        endpoint: string,
        callback: (data: T) => void,
        params: Record<string, unknown> = {}
    ): Promise<T | null> {
        const url = this.buildUrl(endpoint, params);
        const res = await fetch(url);
        if (res.ok) {
            const data: T = await res.json();
            callback(data);
            return data;
        }
        return null;
    }
}