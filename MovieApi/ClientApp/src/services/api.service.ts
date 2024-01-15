import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Movie } from '../Movie';
import { MovieResponse } from '../MovieResponse';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) {
  }

  public get(): Observable<Movie[]> {
    return this.http.get<MovieResponse>('api/Movie').pipe(map(response => response.data));
  }

  public getByPaged(currentPage: number, pageSize: number): Observable<MovieResponse> {
    const params = new HttpParams()
      .set('page', currentPage)
      .set('pageSize', pageSize)
    return this.http.get<MovieResponse>('api/Movie/PagedMovies', { params }).pipe(map(response => response));
  }
}
