import { Movie } from "./Movie";

export interface MovieResponse {
  data: Movie[];
  page: number;
  per_page: number;
  support: any;
  count: number;
  total_pages: number;
}
