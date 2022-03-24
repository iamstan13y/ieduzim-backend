import { NgModule } from '@angular/core';
import { SystemParametersRoutingModule } from './system-parameters-routing.module';
import { PAGES } from './pages';
import { NzTabsModule, NzTableModule, NzButtonModule, NzDrawerModule, NzIconModule, NzPopconfirmModule, NzToolTipModule, NzUploadModule, NzDatePickerModule, NzSwitchModule, NzAvatarModule, NzModalModule, NzGridModule } from 'ng-zorro-antd';
import { SystemParametersModule as SystemParametersDataModule } from '@iedu-data-system-parameters'
import { CommonModule } from '@angular/common';
import { FormModule } from '@iedu-ui-forms';
import { COMPONENTS } from './components';
import { FormsModule } from '@angular/forms';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { AccountsModule } from '@iedu-ui-accounts';
import { VerifiedTeachersComponent } from './pages/teachers/verified-teachers/verified-teachers.component';
import { AwaitingVerificationComponent } from './pages/teachers/awaiting-verification/awaiting-verification.component';
import { UnsubscribedTeachersComponent } from './pages/teachers/unsubscribed-teachers/unsubscribed-teachers.component';

@NgModule({
  declarations: [...PAGES, ...COMPONENTS, VerifiedTeachersComponent, AwaitingVerificationComponent, UnsubscribedTeachersComponent],
  imports: [
    SystemParametersRoutingModule,
    SystemParametersDataModule,
    NzTabsModule,
    NzTableModule,
    CommonModule,
    NzButtonModule,
    NzDrawerModule,
    FormModule,
    NzIconModule,
    NzPopconfirmModule,
    NzToolTipModule,
    NzUploadModule,
    FormsModule,
    NzDatePickerModule,
    NzSwitchModule,
    NzAvatarModule,
    AccountsModule,
    NzTabsModule,
    NzModalModule,
    NzGridModule
  ],
  exports: [],
  providers: [Service]
})
export class SystemParametersModule { }
