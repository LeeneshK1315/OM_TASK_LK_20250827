import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { FlagsService } from './flags.service';

describe('FlagsService', () => {
  let service: FlagsService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({ imports: [HttpClientTestingModule], providers: [FlagsService] });
    service = TestBed.inject(FlagsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('calls restcountries endpoint', () => {
    service.getAllFlags().subscribe();
    const req = httpMock.expectOne('https://restcountries.com/v3.1/all?fields=name,flags');
    expect(req.request.method).toBe('GET');
    req.flush([]);
  });
});