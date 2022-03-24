import { Component, OnInit } from '@angular/core';
import { IForm } from '@iedu-ui-forms';
import { PageResult } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NameModel } from 'libraries/data/system-parameters/src/lib/models/name-model';
import { NzNotificationService } from 'ng-zorro-antd';
import { ACTIVEFIELDS, NAMEFIELDS } from '../../forms/name-model.form';

@Component({
  selector: 'app-provinces',
  templateUrl: './provinces.component.html',
  styleUrls: []
})
export class ProvincesComponent implements OnInit {
  provinces: PageResult<NameModel[]>;
  visible: any;
  form: IForm;
  province: any;
  index: number = 1;
  loading: boolean = false;

  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getProvinces();
   }

  getProvinces(e?){
    this.province = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 8},'Provinces/paged').subscribe(res=> {
      if(res.succeeded){
        this.provinces = res;
        this.loadForm();
      }
      else this.nzNotification.error('Error', res.message, {nzAnimate: true, nzDuration: 10000 })      
    })
  }

  loadForm(province?){
    this.form = {
      fields: [...NAMEFIELDS(province), ...ACTIVEFIELDS(province)],
      buttons: [
        { name: 'Save Province', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: ()=> this.toggleDrawer(false)}
      ]
    }
  }

  save(request){
    this.loading = true;
    if(!this.province)
      this.service.postToUrl('Provinces', request).subscribe(res=> this.requestResult(res))
    else this.service.updateToUrl(`Provinces/${this.province.id}`, {id: this.province.id, ...request}).subscribe(res=>{
      this.requestResult(res);
    })
  }

  delete(province){
    this.service.delete(`Provinces/${province.id}`).subscribe(res=> this.requestResult(res));
  }

  requestResult(res){
    if(res.succeeded) 
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true})
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000})
    this.loading = false;
    this.toggleDrawer(false)
    this.getProvinces();
  }

  edit(province){
    this.province = province;
    this.toggleDrawer(true, province)
  }

  toggleDrawer(visible, province?){
    this.visible = visible;
    this.loadForm(province)
  }

  changeStatus(status: boolean, data) {    
    this.service.updateToUrl(`Provinces/${data.id}`, data).subscribe(r => {
        this.nzNotification.success('Success', `${data.name} ${status ? 'Activated' : 'Deactivated'}`, { nzDuration: 10000, nzAnimate: true });
        this.getProvinces()
      });
  }
}
