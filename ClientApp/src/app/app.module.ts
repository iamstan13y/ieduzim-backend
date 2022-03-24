import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgZorroAntdModule, NZ_I18N, en_US, NzIconModule } from 'ng-zorro-antd';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { CoreModule } from '@iedu/core';
import { LayoutModule } from '@iedu-ui-layout';
import { JwtHelperService } from '@auth0/angular-jwt';
import { TermsAndConditionsComponent } from './terms-and-conditions/terms-and-conditions.component';

registerLocaleData(en);

@NgModule({
  declarations: [
    AppComponent,
    TermsAndConditionsComponent
  ],
  imports: [
    BrowserModule,
    CoreModule,
    AppRoutingModule,
    NgZorroAntdModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    LayoutModule,
    NzIconModule,
  ],
  providers: [{ provide: NZ_I18N, useValue: en_US }, JwtHelperService],
  bootstrap: [AppComponent]
})
export class AppModule { }
