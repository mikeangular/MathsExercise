import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  public exercises: Exercises[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    console.log('baseUrl' + baseUrl);
    // http.get<WeatherForecast[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
    //   this.forecasts = result;
    // }, error => console.error(error));

    http.get<Exercises[]>(baseUrl + 'api/ME/GetExample').subscribe(result => {
      this.exercises = result;
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface Exercises {
  id: number;
  hashValue: string;
  formula: string;
  anwser: string;
  createTime: Date;
  saveTime: Date;
}
