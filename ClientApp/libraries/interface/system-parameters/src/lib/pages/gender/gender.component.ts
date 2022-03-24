import { Component, OnInit } from '@angular/core';
import { Gender, NameModelService } from '@iedu-data-system-parameters';
import { IForm } from '@iedu-ui-forms';
import { PageResult } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NameModel } from 'libraries/data/system-parameters/src/lib/models/name-model';
import { NzNotificationService } from 'ng-zorro-antd';
import { ACTIVEFIELDS, NAMEFIELDS } from '../../forms/name-model.form';

@Component({
  selector: 'app-gender',
  templateUrl: './gender.component.html',
  styleUrls: []
})
export class GenderComponent implements OnInit {
  genders: PageResult<NameModel[]>;
  visible: any;
  form: IForm;
  gender: any;
  index: number = 1;
  loading: boolean = false;

  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getGenders();
   }

  getGenders(e?){
    this.gender = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 8},'Genders/paged').subscribe(res=> {
      if(res.succeeded){
        this.genders = res;
        this.loadForm();
      }
      else this.nzNotification.error('Error', res.message, {nzAnimate: true, nzDuration: 10000 })      
    })
  }

  loadForm(gender?){
    this.form = {
      fields: [...NAMEFIELDS(gender), ...ACTIVEFIELDS(gender)],
      buttons: [
        { name: 'Save Gender', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: ()=> this.toggleDrawer(false)}
      ]
    }
  }

  save(request){
    this.loading = true;
    if(!this.gender)
      this.service.postToUrl('Genders', request).subscribe(res=> this.requestResult(res))
    else this.service.updateToUrl(`Genders/${this.gender.id}`, {id: this.gender.id, ...request}).subscribe(res=>{
      this.requestResult(res);
    })
  }

  delete(gender){
    this.service.delete(`Genders/${gender.id}`).subscribe(res=> this.requestResult(res));
  }

  requestResult(res){
    if(res.succeeded) 
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true})
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000})
    this.loading = false;
    this.toggleDrawer(false)
    this.getGenders();
  }

  edit(gender){
    this.gender = gender;
    this.toggleDrawer(true, gender)
  }

  toggleDrawer(visible, gender?){
    this.visible = visible;
    this.loadForm(gender)
  }

  changeStatus(status: boolean, data) {    
    this.service.updateToUrl(`Genders/${data.id}`, data).subscribe(r => {
        this.nzNotification.success('Success', `${data.name} ${status ? 'Activated' : 'Deactivated'}`, { nzDuration: 10000, nzAnimate: true });
        this.getGenders()
      });
  }
}
