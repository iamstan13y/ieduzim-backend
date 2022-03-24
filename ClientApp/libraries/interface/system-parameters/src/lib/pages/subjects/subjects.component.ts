import { Component, Input, OnInit } from '@angular/core';
import { IForm } from '@iedu-ui-forms';
import { PageResult } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NameModel } from 'libraries/data/system-parameters/src/lib/models/name-model';
import { NzNotificationService } from 'ng-zorro-antd';
import { ACTIVEFIELDS, NAMEFIELDS } from '../../forms/name-model.form';
import { SUBJECTSFIELDS } from '../../forms/subjects.form';

@Component({
  selector: 'app-subjects',
  templateUrl: './subjects.component.html',
  styleUrls: []
})
export class SubjectsComponent implements OnInit {
  @Input() level: NameModel;
  subjects: PageResult<NameModel[]>;
  visible: any;
  form: IForm;
  subject: any;
  index: number = 1;
  loading: boolean = false;

  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getSubjects();
   }

  getSubjects(e?){
    this.subject = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 8},`Subjects/paged/by-level/${this.level.id}`).subscribe(res=> {
      if(res.succeeded){
        this.subjects = res;
        this.loadForm();
      }
      else this.nzNotification.error('Error', res.message, {nzAnimate: true, nzDuration: 10000 })      
    })
  }

  loadForm(subject?){
    this.form = {
      fields: [
        ...NAMEFIELDS(subject), 
        ...SUBJECTSFIELDS(subject),
        ...ACTIVEFIELDS(subject), 
        ],
      buttons: [
        { name: 'Save Subject', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: ()=> this.toggleDrawer(false)}
      ]
    }
  }

  save(request){
    request.levelId = this.level.id;
    this.loading = true;
    if(!this.subject)
      this.service.postToUrl('Subjects', request).subscribe(res=> this.requestResult(res))
    else this.service.updateToUrl(`Subjects/${this.subject.id}`, {id: this.subject.id, ...request}).subscribe(res=>{
      this.requestResult(res);
    })
  }

  delete(subject){
    this.service.delete(`Subject/${subject.id}`).subscribe(res=> this.requestResult(res));
  }

  requestResult(res){
    if(res.succeeded) 
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true})
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000})
    this.loading = false;
    this.toggleDrawer(false)
    this.getSubjects();
  }

  edit(subject){
    this.subject = subject;
    this.toggleDrawer(true, subject)
  }

  toggleDrawer(visible, city?){
    this.visible = visible;
    this.loadForm(city)
  }

  changeStatus(status: boolean, data) {    
    this.service.updateToUrl(`Subjects/${data.id}`, data).subscribe(r => {
        this.nzNotification.success('Success', `${data.name} ${status ? 'Activated' : 'Deactivated'}`, { nzDuration: 10000, nzAnimate: true });
        this.getSubjects()
      });
  }
}
