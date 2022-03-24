import { Component, OnInit, OnChanges, Input, Output, EventEmitter } from '@angular/core';
import { IForm, IField, FieldComparer, FieldType, IButton } from '../../model';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { OptionsService } from '../../options-service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.less']
})
export class FormComponent implements OnInit, OnChanges {
  @Input() form: IForm;
  @Output() result = new EventEmitter<any>();
  validateForm: FormGroup;
  fields: IField[];

  constructor(private fb: FormBuilder, private service: OptionsService) { 
    
  }

  ngOnInit(): void {
    this.loadForm();
  }

  ngOnChanges(): void {
    this.loadForm();
  }

  loadForm() {
    this.validateForm = this.fb.group({}, {
      validators: FieldComparer(this.form.fields)
    });
    this.loadFields();
  }

  getValidators(field: IField) {
    var validators = [];
    if (field.type == FieldType.Email)
      validators.push(Validators.email);
    if (field.required)
      validators.push(Validators.required);
    if (field.type == FieldType.Select || field.type == FieldType.Number)
      validators.push(Validators.min(1))
    return validators;
  }

  loadFields() {
    this.form.fields.forEach(field =>
      this.validateForm.addControl(field.name, new FormControl(field.value, this.getValidators(field))));
    this.fields = this.form.fields;
   this.fields.forEach(field => this.loadOptions(field));
  }

  loadOptions(field: IField) {
    if (!(field.type == FieldType.Select) || !field.options) return;
      this.service.getData(field.options)
        .subscribe(option => {
          field.options.pageRequest = option.pageRequest;
          field.options.pageResult = option.pageResult;
         } );
  }

  loadMoreOptions(field: IField) {
    field.options.pageRequest.pageNumber++;
    this.loadOptions(field);
  }

  loadPreviousOptions(field: IField){
    field.options.pageRequest.pageNumber--;
    this.loadOptions(field)
  }


  click(button: IButton) {
    if (button.validates) {
      for (const i in this.validateForm.controls) {
        this.validateForm.controls[i].markAsDirty();
        this.validateForm.controls[i].updateValueAndValidity();
      }
      if (!this.validateForm.valid) return;
    }
    if (button.action)
      button.action(this.getResult());
    this.result.emit(this.getResult());
  }

  getResult(): any {
    if (!this.form.data) this.form.data = {};
    this.form.fields.forEach(field =>
      this.form.data[field.name] = this.validateForm.value[field.name]);
    return this.form.data;
  }

  getError(field: IField): string {
    var control = this.validateForm.controls[field.name];
    if (!control.errors) return;
    if (control.errors.compare)
      return field.compare.error;
    return `${field.label} is required`;
  }

  getFieldType(field: IField) {
    switch (field.type) {
      case FieldType.Number:
        return 'number';
      case FieldType.Email:
        return 'email';
      case FieldType.Password:
        return 'password';
      default:
        return 'text';
    }
  }

  onChange(data) { }
}