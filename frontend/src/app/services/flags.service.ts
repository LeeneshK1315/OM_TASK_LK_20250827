import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Country } from '../shared/models';

@Injectable({ providedIn: 'root' })
export class FlagsService {
  private http = inject(HttpClient);
  private base = environment.apiBaseUrl;

  getAllFlags() {
    return this.http.get<Country[]>(`${this.base}/countries`);
  }
}