import { Component, OnInit } from '@angular/core';
import { FieldType, IForm } from '@iedu-ui-forms';
import { PageResult } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NameModel } from 'libraries/data/system-parameters/src/lib/models/name-model';
import { NzNotificationService } from 'ng-zorro-antd';
import { ACTIVEFIELDS, NAMEFIELDS } from '../../forms/name-model.form';

@Component({
  selector: 'app-teacher-documents',
  templateUrl: './teacher-documents.component.html',
  styleUrls: []
})

export class TeacherDocumentsComponent implements OnInit {
  requiredDocs: PageResult<NameModel[]>;
  visible: any;
  form: IForm;
  requiredDoc: any;
  index: number = 1;
  loading: boolean = false;

  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getRequiredDocs();
   }

  getRequiredDocs(e?){
    this.requiredDoc = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 8},'QualificationDocuments/paged').subscribe(res=> {
      if(res.succeeded){
        this.requiredDocs = res;
        this.loadForm();
      }
      else this.nzNotification.error('Error', res.message, {nzAnimate: true, nzDuration: 10000 })      
    })
  }

  loadForm(title?){
    this.form = {
      fields: [...NAMEFIELDS(title), 
        { label: 'Required', name: 'required', required: false, value: title ? title.required : false, type: FieldType.Boolean}
      ],
      buttons: [
        { name: 'Save', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: ()=> this.toggleDrawer(false)}
      ]
    }
  }

  save(request){
    this.loading = true;
    if(!this.requiredDoc)
      this.service.postToUrl('QualificationDocuments', request).subscribe(res=> this.requestResult(res))
    else this.service.updateToUrl(`QualificationDocuments/${this.requiredDoc.id}`, {id: this.requiredDoc.id, ...request}).subscribe(res=>{
      this.requestResult(res);
    })
  }

  delete(title){
    this.service.delete(`QualificationDocuments/${title.id}`).subscribe(res=> this.requestResult(res));
  }

  requestResult(res){
    if(res.succeeded) 
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true})
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000})
    this.loading = false;
    this.toggleDrawer(false)
    this.getRequiredDocs();
  }

  edit(rd){
    this.requiredDoc = rd;
    this.toggleDrawer(true, rd)
  }

  toggleDrawer(visible, title?){
    this.visible = visible;
    this.loadForm(title)
  }

  changeStatus(status: boolean, data) {    
    this.service.updateToUrl(`QualificationDocuments/${data.id}`, data).subscribe(r => {
        this.nzNotification.success('Success', `${data.name} ${status ? 'now required' : 'now optional'}`, { nzDuration: 10000, nzAnimate: true });
        this.getRequiredDocs()
      });
  }
}
