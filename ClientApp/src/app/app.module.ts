import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule , Routes } from '@angular/router';
import { TransferHttpCacheModule } from '@nguniversal/common';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';


import { HttpClientModule } from '@angular/common/http';

import { NotFoundComponent } from './not-found/not-found.component';

// import 'hammerjs';

import { MyFormComponent } from './my-form/my-form.component';
import { MatExpansionModule, MatRadioModule, MatInputModule, MatCheckboxModule, MatButtonModule, MatCardModule, MatOptionModule, MatSelectModule, MatIconModule } from '@angular/material';
import { MatDialogModule } from '@angular/material';
import { DialogDemoComponent } from './dialog-demo/dialog-demo.component';
import { MyDialogComponent } from './my-dialog/my-dialog.component';
import { ExerciseComponent } from './exercise/exercise.component';

// const routes: Routes = [
//   { path: '', component: HomeComponent },
//   { path: '**', component: NotFoundComponent }
// ];

// import { InjectionToken } from '@angular/core';

// export const BASE_URL = new InjectionToken<string>('BASE_URL');
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    NotFoundComponent,
    MyFormComponent,
    DialogDemoComponent,
    MyDialogComponent,
    ExerciseComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      // { path: 'en', component: HomeComponent, pathMatch: 'full' },
      // { path: 'sv', component: HomeComponent, pathMatch: 'full' },
      // { path: 'zh-hans', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'exercise', component: ExerciseComponent },
      { path: '**', component: NotFoundComponent },
    ]),
    TransferHttpCacheModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatExpansionModule,
    MatRadioModule,
    MatCheckboxModule,
    MatInputModule,
    MatButtonModule,
    MatDialogModule,
    MatCardModule,
    MatOptionModule, MatSelectModule, MatIconModule
  ],
  entryComponents: [
    MyDialogComponent
  ],
  providers: [  ],
  bootstrap: [AppComponent]
})
export class AppModule {

}
