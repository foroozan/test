import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppConfiguration } from 'read-appsettings-json';
import { Country } from 'src/app/model';

@Injectable({
  providedIn: 'root'
})
export class HttpCountryService {
  private endPoint: string;

  constructor(private httpClient: HttpClient) { 
    this.endPoint  = AppConfiguration.Setting().countriesEndpoint;
  }

  public get(): Observable<Country[]> {
    return this.httpClient.get<Country[]>(this.endPoint, { responseType: 'json' });
  }
}
