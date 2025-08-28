import { TestBed, waitForAsync } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ActivatedRoute } from '@angular/router';
import { DetailComponent } from './detail.component';
import { CountryApiService } from '../../services/country-api.service';

describe('DetailComponent', () => {
  let httpMock: HttpTestingController;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        CountryApiService,
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: { get: () => 'Aland' } } } }
      ]
    }).compileComponents();

    httpMock = TestBed.inject(HttpTestingController);
  }));

  it('fetches country details and sets vm', (done) => {
    const fixture = TestBed.createComponent(DetailComponent as any);
    const comp = fixture.componentInstance as DetailComponent;

    fixture.detectChanges();

    const req = httpMock.expectOne('http://localhost:7114/countries/Aland');
    expect(req.request.method).toBe('GET');
    req.flush({ name: 'Aland', population: 12345, capital: 'Mariehamn', flag: 'https://a.svg' });

    setTimeout(() => {
      expect(comp.vm?.name).toBe('Aland');
      expect(comp.vm?.population).toBe(12345);
      done();
    }, 0);
  });
});