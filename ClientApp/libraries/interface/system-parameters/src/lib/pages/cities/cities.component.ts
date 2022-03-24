import { Component, OnInit } from '@angular/core';
import { NameModelService, City } from '@iedu-data-system-parameters';
import { NzNotificationService } from 'ng-zorro-antd';
import { IForm } from '@iedu-ui-forms';
import { ACTIVEFIELDS, NAMEFIELDS } from '../../forms/name-model.form';
import { PageResult } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: []
})
export class CitiesComponent implements OnInit {
  cities: PageResult<City[]>;
  visible: any;
  form: IForm;
  city: any;
  index: number = 1;
  loading: boolean = false;

  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getCities();
   }

  getCities(e?){
    this.city = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 8},'Cities/paged').subscribe(res=> {
      if(res.succeeded){
        this.cities = res;
        this.loadForm();
      }
      else this.nzNotification.error('Error', res.message, {nzAnimate: true, nzDuration: 10000 })      
    })
  }

  loadForm(city?){
    this.form = {
      fields: [...NAMEFIELDS(city), ...ACTIVEFIELDS(city)],
      buttons: [
        { name: 'Save City', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: ()=> this.toggleDrawer(false)}
      ]
    }
  }

  save(request){
    this.loading = true;
    if(!this.city)
      this.service.postToUrl('Cities', request).subscribe(res=> this.requestResult(res))
    else this.service.updateToUrl(`Cities/${this.city.id}`, {id: this.city.id, ...request}).subscribe(res=>{
      this.requestResult(res);
    })
  }

  delete(city){
    this.service.delete(`Cities/${city.id}`).subscribe(res=> this.requestResult(res));
  }

  requestResult(res){
    if(res.succeeded) 
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true})
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000})
    this.loading = false;
    this.toggleDrawer(false)
    this.getCities();
  }

  edit(city){
    this.city = city;
    this.toggleDrawer(true, city)
  }

  toggleDrawer(visible, city?){
    this.visible = visible;
    this.loadForm(city)
  }

  changeStatus(status: boolean, data) {    
    this.service.updateToUrl(`Cities/${data.id}`, data).subscribe(r => {
        this.nzNotification.success('Success', `${data.name} ${status ? 'Activated' : 'Deactivated'}`, { nzDuration: 10000, nzAnimate: true });
        this.getCities()
      });
  }
}
