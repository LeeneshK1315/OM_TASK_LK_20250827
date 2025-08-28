import { TestBed, waitForAsync } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HomeComponent } from './home.component';
import { FlagsService } from '../../services/flags.service';

describe('HomeComponent', () => {
  let httpMock: HttpTestingController;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [FlagsService]
    }).compileComponents();

    httpMock = TestBed.inject(HttpTestingController);
  }));

  it('loads flags and populates items', (done) => {
    const fixture = TestBed.createComponent(HomeComponent as any);
    const comp = fixture.componentInstance as HomeComponent;

    const mockResponse = [
      { name: { common: 'Aland' }, flags: { svg: 'https://a.svg' } },
      { name: { common: 'Belgium' }, flags: { svg: 'https://b.svg' } }
    ];

    fixture.detectChanges();

    const req = httpMock.expectOne('https://restcountries.com/v3.1/all?fields=name,flags');
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);

    setTimeout(() => {
      expect(comp.items.length).toBe(2);
      expect(comp.items[0].name).toBe('Aland');
      done();
    }, 0);
  });
});