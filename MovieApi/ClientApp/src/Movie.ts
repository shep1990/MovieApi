import { DecimalPipe } from "@angular/common";

export interface Movie {
  title: string;
  release_Date: string;
  overview: string | undefined;
  popularity: DecimalPipe;
  vote_Count: number;
  vote_Average: DecimalPipe
  original_Language: string
  genre: string
  poster_Url: string
}
