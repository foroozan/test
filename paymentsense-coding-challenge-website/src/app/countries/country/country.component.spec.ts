import { async, ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';

import { CountryComponent } from './country.component';
import { NO_ERRORS_SCHEMA } from '@angular/core';

describe('CountryComponent', () => {
  let component: CountryComponent;
  let fixture: ComponentFixture<CountryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CountryComponent ],
      schemas:      [ NO_ERRORS_SCHEMA ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountryComponent);
    component = fixture.componentInstance;
    component.country = {
        name: "test name",
        capital:"test capital",
        flag: "test flag",
        population: 70000000,
        timezones: ["utc"]
      };
  
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should expand on click', fakeAsync(() => {
    // arrange 
    fixture.detectChanges();
    const panel = fixture.nativeElement.querySelector('mat-expansion-panel');

    // act
    panel.click();
    tick();
    fixture.detectChanges();
    
    // assert panel is open
  }));
});
