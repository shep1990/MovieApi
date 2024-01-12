import { AfterViewInit, Component, Injectable, OnInit, ViewChild } from '@angular/core';
import { Movie } from '../../Movie';
import { ApiService } from '../../services/api.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  dataSource: any;
  displayedColumns: string[] = ['title', 'release_Date', 'overview', 'popularity', 'vote_count', 'vote_average', 'original_language', 'genre', 'poster_URL'];
  public movies: Movie[] = [];
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private apiService: ApiService) {
    this.getTableRecords();
  }

  getTableRecords() {
    this.apiService.get().subscribe((data) => {
      this.movies = Object.values(data)
      this.dataSource = new MatTableDataSource(this.movies)
      this.dataSource.paginator = this.paginator;
    });
  }

  search(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  title = 'angularapp';
}
