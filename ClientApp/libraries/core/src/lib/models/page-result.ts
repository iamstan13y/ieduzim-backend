export interface PageResult<T>{
    succeeded?: boolean;
    message?: boolean;
    data: T,
    pageNumber?: number;
    pageSize?: number;
    totalPages?: number;
    totalItems?: number;
}