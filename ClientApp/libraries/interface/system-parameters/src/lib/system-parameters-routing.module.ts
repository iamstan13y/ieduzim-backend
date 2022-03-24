import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViewLevelDetailsComponent } from './components/view-level-details/view-level-details.component';
import { BanksComponent } from './pages/banks/banks.component';
import { CitiesComponent } from './pages/cities/cities.component';
import { CurrenciesComponent } from './pages/currencies/currencies.component';
import { DistancePricesComponent } from './pages/distance-prices/distance-prices.component';
import { ExamTypesComponent } from './pages/exam-types/exam-types.component';
import { FeesComponent } from './pages/fees/fees.component';
import { GenderComponent } from './pages/gender/gender.component';
import { LessionLocationsComponent } from './pages/lession-locations/lession-locations.component';
import { LevelsComponent } from './pages/levels/levels.component';
import { LocationParametersComponent } from './pages/location-parameters/location-parameters.component';
import { PaymentPeriodsComponent } from './pages/payment-periods/payment-periods.component';
import { PaymentSettingsComponent } from './pages/payment-settings/payment-settings.component';
import { ProvincesComponent } from './pages/provinces/provinces.component';
import { RolePaymentsComponent } from './pages/role-payments/role-payments.component';
import { StudentsComponent } from './pages/students/students.component';
import { SubjectsParametersComponent } from './pages/subjects-parameters/subjects-parameters.component';
import { SubjectsComponent } from './pages/subjects/subjects.component';
import { TeacherDocumentsComponent } from './pages/teacher-documents/teacher-documents.component';
import { AwaitingVerificationComponent } from './pages/teachers/awaiting-verification/awaiting-verification.component';
import { TeachersComponent } from './pages/teachers/teachers.component';
import { UnsubscribedTeachersComponent } from './pages/teachers/unsubscribed-teachers/unsubscribed-teachers.component';
import { VerifiedTeachersComponent } from './pages/teachers/verified-teachers/verified-teachers.component';
import { TitleComponent } from './pages/title/title.component';
import { UserParametersComponent } from './pages/user-parameters/user-parameters.component';
import { UserProfileComponent } from './pages/user-profile/user-profile.component';
import { UsersComponent } from './pages/users/users.component';
import { RESOLVER } from './resolvers';
import { LevelResolver } from './resolvers/level-resolver';

const routes: Routes = [
      {
        path: 'levels', component: LevelsComponent
      },
      {
        path: 'levels/:id', component: ViewLevelDetailsComponent,  resolve: { level: LevelResolver }
      },
      {
        path: 'subjects', component: SubjectsComponent
      },
      {
        path: 'user-parameters', component: UserParametersComponent,
        children:[
          {
            path: 'titles', component: TitleComponent
          },
          {
            path: 'gender', component: GenderComponent
          },
        ]
      },
      {
        path: 'location-parameters', component: LocationParametersComponent,
        children:[
          {
            path: 'cities', component: CitiesComponent
          },
          {
            path: 'provinces', component: ProvincesComponent
          },
        ]
      },
      {
        path: 'subjects-parameters', component: SubjectsParametersComponent,
        children:[
          {
            path: 'exam-types', component: ExamTypesComponent
          },
          {
            path: 'lession-locations', component: LessionLocationsComponent
          },
        ]
      },
      {
        path: 'payment-settings', component: PaymentSettingsComponent,
        children:[
          {
            path: 'currencies', component: CurrenciesComponent
          },
          {
            path: 'payment-periods', component: PaymentPeriodsComponent
          },
          {
            path: 'banks', component: BanksComponent
          }
        ]
      },

      {
        path: 'fees', component: DistancePricesComponent
      },

      // {
      //   path: 'fees', component: FeesComponent,
      //   children:[
      //     {
      //       path: 'traveling-fees', component: DistancePricesComponent
      //     },
      //     {
      //       path: 'subject-fees', component: BanksComponent
      //     }
      //   ]
      // },

       {
        path: 'users', component: UsersComponent,
        children:[
          {
            path: 'teachers', component: TeachersComponent,
            children: [
              {
                path: 'verified', component: VerifiedTeachersComponent
              },
              {
                path: 'awaiting-verification', component: AwaitingVerificationComponent
              },
              {
                path: 'unsubscribed', component: UnsubscribedTeachersComponent
              }
            ]
          },
          {
            path: 'students', component: StudentsComponent
          }
        ]
      },
      {
        path: 'profile', component: UserProfileComponent
      },
      {
        path: 'role-payment-period', component: RolePaymentsComponent
      },
      {
        path: 'required-documents', component: TeacherDocumentsComponent
      }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [...RESOLVER]
})
export class SystemParametersRoutingModule {
}
