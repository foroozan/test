import { HttpCountryService } from './http-country.service';
import { TestBed} from '@angular/core/testing';
import { HttpClientTestingModule , HttpTestingController } from '@angular/common/http/testing';
import { Country } from 'src/app/model';

describe('HttpCountryService', () => {
  let httpTestingController: HttpTestingController;
  let sut: HttpCountryService;
  let token: string;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [HttpCountryService]
    });

    sut = TestBed.get(HttpCountryService);
    httpTestingController = TestBed.get(HttpTestingController);

  });

  it('should be created', () => {
    expect(sut).toBeTruthy();
  });

  it('should retrieve countries from API via GET', () => {

    const expectedCountries: Country[] = [{
      name: "test name",
      capital:"test capital",
      flag: "test flag",
      population: 70000000,
      timezones: ["utc"]
    }];

    sut.get().subscribe( countries => {
        expect(countries).toBeTruthy();
        expect(countries[0].name).toEqual(expectedCountries[0].name);
    });

    const response = httpTestingController.expectOne((request) => {
      return  request.method === 'GET';
    });

    response.flush(expectedCountries);
  });
});


