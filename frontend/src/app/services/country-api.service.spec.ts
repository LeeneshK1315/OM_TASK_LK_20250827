import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CountryApiService } from './country-api.service';

describe('CountryApiService', () => {
  let service: CountryApiService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({ imports: [HttpClientTestingModule], providers: [CountryApiService] });
    service = TestBed.inject(CountryApiService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('requests /countries', () => {
    service.getCountries().subscribe();
    const req = httpMock.expectOne('http://localhost:7114/countries');
    expect(req.request.method).toBe('GET');
    req.flush([]);
  });

  it('requests /countries/{name}', () => {
    service.getCountry('Aland').subscribe();
    const req = httpMock.expectOne('http://localhost:7114/countries/Aland');
    expect(req.request.method).toBe('GET');
    req.flush({});
  });
});