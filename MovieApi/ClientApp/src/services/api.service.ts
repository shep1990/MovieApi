import { HttpClient } from '@angular/common/http';
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
}
