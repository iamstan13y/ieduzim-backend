import { NgModule } from '@angular/core';
import { AccountsComponent } from './accounts.component';
import  { AccountsModule as AccountsDataModule } from '@iedu-data-accounts'
import { AccountsRoutingModule } from './accounts-routing.module';
import { COMPONENT } from './pages';
import { NzCardModule, NzInputModule, NzInputNumberModule, NzIconModule, NzFormModule, NzButtonModule } from 'ng-zorro-antd';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { ResetPasswordComponent } from './pages/reset-password/reset-password.component';

@NgModule({
  declarations: [...COMPONENT, AccountsComponent, MainPageComponent],
  imports: [
    AccountsDataModule,
    AccountsRoutingModule,
    NzCardModule,
    NzInputModule,
    NzInputNumberModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    NzIconModule,
    NzFormModule,
    NzButtonModule
  ],
  exports: [AccountsComponent, ResetPasswordComponent],
  providers:[JwtHelperService, Service]
})
export class AccountsModule { }
