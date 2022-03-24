import { Component, OnInit } from '@angular/core';
import { IForm } from '@iedu-ui-forms';
import { PageResult } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NzNotificationService } from 'ng-zorro-antd';
import { NAMEFIELDS, ACTIVEFIELDS } from '../../forms/name-model.form';

@Component({
  selector: 'app-exam-types',
  templateUrl: './exam-types.component.html',
  styleUrls: []
})
export class ExamTypesComponent implements OnInit {
  examTypes: PageResult<any[]>;
  visible: any;
  form: IForm;
  examType: any;
  index: number = 1;
  loading: boolean = false;

  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getExamTypes();
   }

  getExamTypes(e?){
    this.examType = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 8},'ExamTypes/paged').subscribe(res=> {
      if(res.succeeded){
        this.examTypes = res;
        this.loadForm();
      }
      else this.nzNotification.error('Error', res.message, {nzAnimate: true, nzDuration: 10000 })      
    })
  }

  loadForm(examType?){
    this.form = {
      fields: [...NAMEFIELDS(examType), ...ACTIVEFIELDS(examType)],
      buttons: [
        { name: 'Save Exam Type', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: ()=> this.toggleDrawer(false)}
      ]
    }
  }

  save(request){
    this.loading = true;
    if(!this.examType)
      this.service.postToUrl('ExamTypes', request).subscribe(res=> this.requestResult(res))
    else this.service.updateToUrl(`ExamTypes/${this.examType.id}`, {id: this.examType.id, ...request}).subscribe(res=>{
      this.requestResult(res);
    })
  }

  delete(examType){
    this.service.delete(`ExamTypes/${examType.id}`).subscribe(res=> this.requestResult(res));
  }

  requestResult(res){
    if(res.succeeded) 
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true})
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000})
    this.loading = false;
    this.toggleDrawer(false)
    this.getExamTypes();
  }

  edit(examType){
    this.examType = examType;
    this.toggleDrawer(true, examType)
  }

  toggleDrawer(visible, examType?){
    this.visible = visible;
    this.loadForm(examType)
  }

  changeStatus(status: boolean, data) {    
    this.service.updateToUrl(`ExamTypes/${data.id}`, data).subscribe(r => {
        this.nzNotification.success('Success', `${data.name} ${status ? 'Activated' : 'Deactivated'}`, { nzDuration: 10000, nzAnimate: true });
        this.getExamTypes()
      });
  }
}