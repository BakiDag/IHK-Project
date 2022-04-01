import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule, routingComponents } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';

import { LoginServiceService } from './services/login-service.service';
import { ReportControlComponent } from './ausbilderComponents/report-control/report-control.component';




@NgModule({
  declarations: [
    AppComponent,    
    routingComponents, ReportControlComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,        
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [        
    LoginServiceService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
