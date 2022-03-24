import { Component, OnInit, Input } from '@angular/core';
import { FieldType } from '../../model';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {

  @Input() fields;
  @Input() data;

  constructor() { }

  ngOnInit() {
    if (!this.fields) return;
    this.fields.forEach(field => {
      field.value = field.value || this.data[field.name]});
  }

  filteredItems() { return this.fields ? this.fields.filter(f => f.value != null) : [] }; 
}
