import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Country, CountryDetails } from '../shared/models';

@Injectable({ providedIn: 'root' })
export class CountryApiService {
  private http = inject(HttpClient);
  private base = environment.apiBaseUrl;

  getCountries() { return this.http.get<Country[]>(`${this.base}/countries`); }
  getCountry(name: string) { return this.http.get<CountryDetails>(`${this.base}/countries/${encodeURIComponent(name)}`); }
}