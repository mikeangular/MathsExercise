import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl, FormGroupDirective, NgForm } from '@angular/forms';
import { ErrorStateMatcher} from '@angular/material/core';
import { Guid } from 'guid-typescript';
import { config } from 'rxjs';


/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

class Exercises {
  id: number;
  index: number;
  settingId: number;
  formula: string;
  private _userAnswer: string;
  get userAnswer(): string {
    return this._userAnswer;
  }

  set userAnswer(newAnswer: string) {
    if ( newAnswer.trim().length >= 0 ) {
      this._userAnswer = newAnswer;
      this._saveTime = new Date();
    }
    if ( newAnswer.trim() === this.rightAnswer.trim() ) {
      this._isRight = true;
    } else {
      this._isRight = false;
    }

  }
  rightAnswer: string;
  createTime: Date;
  private _saveTime: Date;
  get saveTime(): Date {
    return this._saveTime;
  }
  private _isRight: boolean;
  get IsRight(): boolean {
      return this._isRight;

  }
}
class JsonExercises {
  id: number;
  index: number;
  settingId: number;
  formula: string;
  userAnswer: string;
  rightAnswer: string;
  createTime: Date;
  saveTime: Date;
}
class PutExerciseClass {
  config: ConfigClass;
  expresses: JsonExercises[];
}
class ConfigClass {
  amount: number;
  quantity: number;
  operators: string;
  maxvalue: number;
  createtime: Date;

}

interface ReturnClass {
  message: string;
}
interface JsonData {
  id: number;
  message: string;
}
@Component({
  selector: 'app-exercise',
  templateUrl: './exercise.component.html',
  styleUrls: ['./exercise.component.css']
})

export class ExerciseComponent {
  public exercises: Exercises[];
  addition = true;
  subtraction = true;
  multiplication = true;
  division = true;
  amount = '5';
  quantityarithmetic = '3';
  MaxValueType = '2';
  MaxValue = 50;
  teststring = '';
  showAnswer = false;
  createtime: Date ;
  private http: HttpClient;
  private baseUrl: string;
  Radnomdisabled = false;
  Submitdisabled = true;


  constructor(Http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    // console.log('baseUrl' + baseUrl);
    // http.get<WeatherForecast[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
    //   this.forecasts = result;
    // }, error => console.error(error));
    this.http = Http;
    // baseUrl += 'en/';
    console.log('Inject baseUrl=' + baseUrl);
    const index  = baseUrl.indexOf('/en');
    console.log('index=' + index.toString());
    // if ( index > 0 ) {
    //   console.log('lang = en');
    //   this.baseUrl = baseUrl.substr(0, index + 1);
    // } else {
    //   index  = baseUrl.indexOf('/sv');
    //   if (index > 0) {
    //     console.log('lang = sv');
    //     this.baseUrl = baseUrl.substr(0, index + 1);
    //   } else {
    //     index  = baseUrl.indexOf('/zh-hans');
    //     if (index > 0) {
    //       console.log('lang = cn');

    //       this.baseUrl = baseUrl.substr(0, index + 1);
    //     } else {
    //       this.baseUrl = baseUrl;
    //     }
    //   }


    // }
    this.baseUrl = baseUrl;
    console.log('After check Language');
    console.log('this.baseUrl=' + this.baseUrl);
    console.log('original baseUrl=' + baseUrl);

    // Http.get<Exercises[]>(baseUrl + 'api/ME/GetQuestion/' + Guid.create() + '/20/abcd').subscribe(result => {
    // // http.get<Exercises[]>(baseUrl + 'api/ME/GetExample').subscribe(result => {
    //     this.exercises = result;
    // }, error => console.error(error));
  }
  onTestGet() {

    this.http.get<ReturnClass>(this.baseUrl + 'en/api/ME/TestGet/' + this.MaxValue.toString() ).subscribe(result => {
      this.teststring  = result.message;
    }, error => console.error(error));
  }
  onTestOuterGet() {

    this.http.get<ReturnClass>('http://localhost:8090/api/ME/TestGet/' + this.MaxValue.toString()).subscribe(result => {
      this.teststring  = result.message;
    }, error => console.error(error));
  }
  onTestPut() {
    const jsondata: JsonData = { id: this.MaxValue, message : '*en/api this message is created by angular*' };

    this.http.put<ReturnClass>(this.baseUrl + 'en/api/ME/TestPut', jsondata ).subscribe(result => {
      this.teststring = result.message.toString();
      // this.showAnswer = true;
    }, error => console.error(error));
  }

  onTest() {
    // this.http.get<string>(this.baseUrl + 'api/ME/Test/5').subscribe(result => {
    //   this.teststring = result;
    // }, error => console.error(error));


    this.teststring = (new Date()).toString();


  }
  onGetExemple() {
    // http.get<Exercises[]>(baseUrl + 'api/ME/GetQuestion/' + Guid.create() + '/20/abcd').subscribe(result => {
    this.http.get<Exercises[]>(this.baseUrl + 'api/ME/GetExample/5').subscribe(result => {
          this.exercises = result;
    }, error => console.error(error));
  }
  onRandom() {
    if (this.TypeCheck()) {
      this.Radnomdisabled = true;
      this.Submitdisabled = true;
      let param: string = Guid.create() + '/' + this.amount;
      param += '/';
      if (this.addition) { param += 'a'; }
      if (this.subtraction) { param += 'b'; }
      if (this.multiplication) { param += 'c'; }
      if (this.division) { param += 'd'; }
      param += '/' + this.quantityarithmetic.toString();
      param += '/' + this.MaxValueType.toString();
      param += '/' + this.MaxValue.toString();




      this.http.get<Exercises[]>(this.baseUrl + 'api/ME/GetQuestion/' + param).subscribe(result => {
          this.showAnswer = false;
      //  this.http.get<Exercises[]>(this.baseUrl + 'api/ME/GetQuestion/jk').subscribe(result => {
          this.Radnomdisabled = false;
          this.createtime = new Date();
          this.teststring = '';
          if ( result.length === 0 ) {
            this.teststring = '0 express is created!Please try it again';
          } else {
            let item: any ;
            // 如果直接使用this.exercises = result。则实际上，使用的不是Exercises类，而是一个从json转化而来的类，将缺少Exercises的自定义，例如_saveTime, _userAnswer等等
            this.exercises = new Array(result.length);
            for (item of  Object.keys(result)) {
              this.exercises[item] = new Exercises();
              this.exercises[item].id = result[item].id;
              this.exercises[item].index = +item + 1;
              this.exercises[item].settingId = result[item].settingId;
              this.exercises[item].rightAnswer = result[item].rightAnswer;
              this.exercises[item].createTime = result[item].createTime;
              this.exercises[item].formula = result[item].formula;
              this.exercises[item].userAnswer = '';

            }

            this.Submitdisabled = false;
          }

      }, error => {
        console.error(error);
        this.Radnomdisabled = false;
      });
    }
  }
  onSubmit() {
    this.Radnomdisabled = true;
    this.Submitdisabled = true;

    let val: any;
    this.teststring = '';
    const jsondata: PutExerciseClass = new PutExerciseClass();

    jsondata.expresses = new Array(this.exercises.length);
    jsondata.config = new ConfigClass();
    jsondata.config.createtime = this.createtime;
    jsondata.config.maxvalue = this.MaxValue;
    jsondata.config.amount = Number(this.amount);
    jsondata.config.quantity = Number(this.quantityarithmetic);
    jsondata.config.operators = '[';
    if (this.addition) { jsondata.config.operators += '+'; }
    if (this.subtraction) { jsondata.config.operators += '-'; }
    if (this.multiplication) { jsondata.config.operators += '*'; }
    if (this.division) { jsondata.config.operators += '/'; }
    jsondata.config.operators += ']';

    for (val of  Object.keys(this.exercises)) {
      jsondata.expresses[val] =  new JsonExercises();
      jsondata.expresses[val].id = this.exercises[val].id;
      jsondata.expresses[val].createTime = this.exercises[val].createTime;
      jsondata.expresses[val].saveTime  = this.exercises[val].saveTime;
      jsondata.expresses[val].settingId = this.exercises[val].settingId;
      jsondata.expresses[val].userAnswer = this.exercises[val].userAnswer;
      jsondata.expresses[val].rightAnswer = this.exercises[val].rightAnswer;
      jsondata.expresses[val].formula = this.exercises[val].formula;

      // this.teststring += 'ID : '  + this.exercises[val].id + ' RightAnswer：' + this.exercises[val].rightAnswer.toString() + ' your answer:' + this.exercises[val].userAnswer + 'savetime:' +  '\r\n'  ;  // statements
    }
    this.http.put<ReturnClass>(this.baseUrl + 'api/ME/PutResult', jsondata ).subscribe(result => {
      this.teststring = result.message.toString();
      this.showAnswer = true;
      this.Radnomdisabled = false;
      this.Submitdisabled = false;
    }, error => {
      console.error(error);
      this.Radnomdisabled = false;
    });
  }
  TypeCheck() {
    if (this.addition) { return true;  }
    if (this.subtraction) { return true;  }
    if (this.multiplication) { return true;  }
    if (this.division) { return true;  }
    return false;
    //
  }
    // amount = new FormControl('valid', [
  //   Validators.required,
  //   Validators.pattern('20'),
  // ]);

  // selectFormControl = new FormControl('valid', [
  //   Validators.required,
  //   Validators.pattern('valid'),
  // ]);

  // nativeSelectFormControl = new FormControl('valid', [
  //   Validators.required,
  //   Validators.pattern('valid'),
  // ]);

  // matcher = new MyErrorStateMatcher();


}

