import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CountriesComponent } from './countries.component';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpCountryService } from './services/http-country.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Country } from '../model';
import { of } from 'rxjs';

describe('CountriesComponent', () => {
  let component: CountriesComponent;
  let fixture: ComponentFixture<CountriesComponent>;
  let httpCountryService: HttpCountryService;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CountriesComponent ],
      imports: [HttpClientTestingModule],
      providers: [HttpCountryService],
      schemas:      [ NO_ERRORS_SCHEMA ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    httpCountryService = TestBed.get(HttpCountryService);
  });

  it('should create', () => {
     expect(component).toBeTruthy();
  });

  it('should create empty country list', () => {
      expect(component.countries.length).toBe(0);
  });

  it('ngOnInit updates the list of countries', () => {
      // arrange
      const expectedCountries: Country[] = [{
        name: "test name",
        capital:"test capital",
        flag: "test flag",
        population: 70000000,
        timezones: ["utc"]
      }];
      spyOn(httpCountryService, 'get').and.returnValue(of(expectedCountries));

    // act
    component.ngOnInit();

    // assert
    expect(component.countries.length).toBe(1);
    expect(httpCountryService.get).toHaveBeenCalled();
  });
});
