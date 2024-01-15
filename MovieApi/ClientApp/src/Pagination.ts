export interface Pagination<T>
{
  count: number;
  pageIndex: number;
  pageSize: number;
  items: T[];
}
