import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundComponent } from 'libraries/interface/layout/src/lib/pages/page-not-found/page-not-found.component';
import { AuthGuard } from 'libraries/core/src/lib/guards/auth.guard';
import { MainContentComponent } from 'libraries/interface/layout/src/lib/components/main-content/main-content.component';
import { TermsAndConditionsComponent } from './terms-and-conditions/terms-and-conditions.component';

const routes: Routes = [
  {
    path: 'account', loadChildren: () => import('@iedu-ui-accounts').then(a => a.AccountsModule)
  },
  {
    path: 'terms-and-conditions', component: TermsAndConditionsComponent
  },
  {
    path: '', component: MainContentComponent, canActivate: [AuthGuard],
    children: [
      {
        path: 'admin', loadChildren: () => import('@iedu-ui-system-parameters').then(a => a.SystemParametersModule)
      },
      { path: '**', component: PageNotFoundComponent}
    ]
  },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
