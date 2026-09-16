export class QueryResult {
    static buildUrl(endpoint: string, params: Record<string, unknown> = {}): string {
        const searchParams = new URLSearchParams();

        Object.entries(params).forEach(([key, val]) => {
            if (val === null || val === undefined || val === '') return;

            if (Array.isArray(val)) {
                // Generates: ?materials=Quartz&materials=Moss+Agate
                val.forEach(item => searchParams.append(key, String(item)));
            } else {
                searchParams.append(key, String(val));
            }
        });

        const queryString = searchParams.toString();
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