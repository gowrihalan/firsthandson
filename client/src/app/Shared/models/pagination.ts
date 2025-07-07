export type pagination<T> = {
    count: number
    pageSize: number
    pageIndex: number
    data: T[]

}