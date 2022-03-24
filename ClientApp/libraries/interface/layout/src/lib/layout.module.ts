import { NgModule } from '@angular/core';
import { LayoutComponent } from './layout.component';
import { NgZorroAntdModule, NzDrawerModule, NzButtonModule, NzBadgeModule, NzIconModule, NzCollapseModule, NzMenuModule, NzLayoutModule } from 'ng-zorro-antd';
import { FormsModule } from '@angular/forms';
import { TopMenuComponent } from './components/top-menu/top-menu.component';
import { LayoutRoutingModule } from './layout-routing.module';
import { CommonModule } from '@angular/common';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { ChartsModule, ThemeService } from 'ng2-charts';
import { FormModule } from '@iedu-ui-forms';
import { MainContentComponent } from './components/main-content/main-content.component';

@NgModule({
  declarations: [LayoutComponent, TopMenuComponent, PageNotFoundComponent, ResetPasswordComponent, MainContentComponent],
  imports: [
    CommonModule,
    NgZorroAntdModule,
    FormsModule,
    LayoutRoutingModule,
    NzDrawerModule,
    NzButtonModule,
    NzBadgeModule,
    NzIconModule,
    NzCollapseModule,
    ChartsModule,
    FormModule,
    NzMenuModule,
    NzLayoutModule
  ],
  exports: [LayoutComponent],
  providers:[ThemeService]
})
export class LayoutModule { }
