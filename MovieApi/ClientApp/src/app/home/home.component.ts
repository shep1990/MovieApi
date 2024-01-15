import { AfterViewInit, Component, Injectable, OnInit, ViewChild } from '@angular/core';
import { Movie } from '../../Movie';
import { ApiService } from '../../services/api.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { tap } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements AfterViewInit, OnInit {
  dataSource: MatTableDataSource<Movie> = new MatTableDataSource();
  displayedColumns: string[] = ['title', 'release_Date', 'overview', 'popularity', 'vote_count', 'vote_average', 'original_language', 'genre', 'poster_URL'];
  public movies: Movie[] = [];
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  totalRows = 0;
  pageSize = 10;
  currentPage = 0;
  pageSizeOptions: number[] = [5, 10, 25, 100];

  constructor(private apiService: ApiService, private route: ActivatedRoute, private router: Router) {

  }

  ngAfterViewInit(): void {
    this.paginator.page.subscribe(() => {
      this.router.navigate([''], {
        relativeTo: this.route,
        queryParams: { page: this.paginator.pageIndex + 1 },
        queryParamsHandling: 'merge',
      });
    });
  }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.apiService.getByPaged(this.currentPage, this.pageSize).subscribe((data) => {
      this.movies = Object.values(data.data)
      this.dataSource.data = this.movies
      this.paginator.pageIndex = this.currentPage;
      this.totalRows = data.count
    });
  }

  search(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }


  onPageChange(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.loadData();
  }

  title = 'angularapp';
}
