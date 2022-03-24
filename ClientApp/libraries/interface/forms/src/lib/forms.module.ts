import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { FormComponent } from './components/form/form.component';
import { TableComponent } from './components/table/table.component';
import { CommonModule } from '@angular/common';
import { FieldValuesComponent } from './components/field-values/field-values.component';
import { OptionsService } from './options-service';


@NgModule({
  declarations: [FormComponent, TableComponent, FieldValuesComponent],
  imports: [
    NgZorroAntdModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule
  ],
  exports: [FormComponent, TableComponent],
  providers: [OptionsService]
})
export class FormModule { }
