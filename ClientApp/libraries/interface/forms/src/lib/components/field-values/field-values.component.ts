import { Component, OnInit, Input } from '@angular/core';
import { FieldType, IField } from '../../model';
import { OptionsService } from '../../options-service';

@Component({
  selector: 'app-field-values',
  templateUrl: './field-values.component.html',
  styleUrls: []
})
export class FieldValuesComponent implements OnInit {

  @Input() field: IField;
  @Input() data: any;

  constructor(private service: OptionsService) { }

  ngOnInit() {
  }

  ngOnChanges(){
    let data = this.data ? this.data : {};
    switch (this.field.type) {
      case FieldType.Number:
      case FieldType.Money:
        this.field.value = this.field.value | data[this.field.name] | 0;
        break;
      case FieldType.Boolean:
        this.field.value = this.field.value ? true : false;
        break;
      default:
        this.field.value = this.field.value ? this.field.value : data[this.field.name];
        break;
    }
  }

}
