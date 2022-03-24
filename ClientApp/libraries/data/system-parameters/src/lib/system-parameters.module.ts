import { NgModule } from '@angular/core';
import { SystemParametersComponent } from './system-parameters.component';
import { SERVICES } from './services';

@NgModule({
  declarations: [SystemParametersComponent],
  imports: [
  ],
  exports: [SystemParametersComponent],
  providers:[...SERVICES]
})
export class SystemParametersModule { }
