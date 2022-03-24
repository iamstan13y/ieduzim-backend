import { NgModule } from '@angular/core';
import { AccountsComponent } from './accounts.component';
import { SERVICES } from './services';



@NgModule({
  declarations: [AccountsComponent],
  imports: [
  ],
  exports: [AccountsComponent],
  providers:[...SERVICES]
})
export class AccountsModule { }
