//libs
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CalendarModule } from 'angular-calendar';
import { CommonModule, registerLocaleData } from '@angular/common';
import localeIt from '@angular/common/locales/it';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
//Modules
import { AppRoutingModule } from './app-routing.module';
import { ApiModule, ConfigurationParameters, Configuration, AppuntamentiService, AziendeService } from './api';
//Components
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TodayComponent } from './dashboard/today/today.component';
import { AppuntamentoComponent } from './appuntamento/appuntamento.component';
import { CalendarComponent } from './appuntamento/calendar/calendar.component';
import {AppointmentComponent} from './appointment/appointment.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { LoginComponent } from './account/login/login.component';

registerLocaleData(localeIt);
const parm: ConfigurationParameters = {
  username: '',
  password: '',
  accessToken: '',
  basePath:'http://api.splendorsrl.it',
  //basePath: 'http://api.fatture-online.local',
  withCredentials: false
};
export function serverFactory() {
  return new Configuration(parm);
}
@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    TodayComponent,
    AppuntamentoComponent,
    CalendarComponent,
    AppointmentComponent,
    PagenotfoundComponent,
    LoginComponent
  ],
  imports: [CommonModule, NgSelectModule,
    FormsModule,
    BrowserAnimationsModule,
    BrowserModule, NgbModule.forRoot(), AppRoutingModule, CalendarModule.forRoot(),
    ApiModule.forRoot(serverFactory)
  ],
  providers: [AppuntamentiService, AziendeService],
  bootstrap: [AppComponent]
})
export class AppModule { }
