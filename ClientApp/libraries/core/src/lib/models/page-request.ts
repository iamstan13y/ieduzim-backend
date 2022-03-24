

export interface PageRequest {
    pageNumber: number;
    pageSize: number;
    sortParam?: string;
    Desc?: boolean;
    searchParam?: string;
    last?: boolean;
    [key: string]: any;
}

export const REQUESTPARAMS = function get(request: PageRequest): string {
    if (!request) return '';
    let url = `?`;
    for (let [key, value] of Object.entries(request)) {
        if (value === undefined || value === null) continue;
        url += `&${key}=${value}`;
    }
    return url;
}