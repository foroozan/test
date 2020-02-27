import { Component, OnInit } from '@angular/core';
import { HttpCountryService } from './services/http-country.service';
import { Country } from '../model';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.scss']
})
export class CountriesComponent implements OnInit {

  countries: Array<Country> = [];
  pageOfCountries: Array<Country>;

  constructor(private httpCountryService: HttpCountryService) { }

  ngOnInit() {
    this.httpCountryService.get().subscribe((countries)=>{
        this.countries = countries;
    });
  }

  onChangePage(pageOfCountries: Array<any>) {
    this.pageOfCountries = pageOfCountries;
  }
}
