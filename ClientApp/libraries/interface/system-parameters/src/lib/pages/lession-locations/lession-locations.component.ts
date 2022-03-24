import { Component, OnInit } from '@angular/core';
import { IForm } from '@iedu-ui-forms';
import { PageResult } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NzNotificationService } from 'ng-zorro-antd';
import { NAMEFIELDS, ACTIVEFIELDS } from '../../forms/name-model.form';

@Component({
  selector: 'app-lession-locations',
  templateUrl: './lession-locations.component.html',
  styleUrls: []
})
export class LessionLocationsComponent implements OnInit {
  locations: PageResult<any[]>;
  visible: any;
  form: IForm;
  location: any;
  index: number = 1;
  loading: boolean = false;

  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getLocations();
   }

  getLocations(e?){
    this.location = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 8},'LessonLocations/paged').subscribe(res=> {
      if(res.succeeded){
        this.locations = res;
        this.loadForm();
      }
      else this.nzNotification.error('Error', res.message, {nzAnimate: true, nzDuration: 10000 })      
    })
  }

  loadForm(location?){
    this.form = {
      fields: [...NAMEFIELDS(location), ...ACTIVEFIELDS(location)],
      buttons: [
        { name: 'Save Lesson Location', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: ()=> this.toggleDrawer(false)}
      ]
    }
  }

  save(request){
    this.loading = true;
    if(!this.location)
      this.service.postToUrl('LessonLocations', request).subscribe(res=> this.requestResult(res))
    else this.service.updateToUrl(`LessonLocations/${this.location.id}`, {id: this.location.id, ...request}).subscribe(res=>{
      this.requestResult(res);
    })
  }

  delete(location){
    this.service.delete(`LessionLocations/${location.id}`).subscribe(res=> this.requestResult(res));
  }

  requestResult(res){
    if(res.succeeded) 
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true})
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000})
    this.loading = false;
    this.toggleDrawer(false)
    this.getLocations();
  }

  edit(location){
    this.location = location;
    this.toggleDrawer(true, location)
  }

  toggleDrawer(visible, location?){
    this.visible = visible;
    this.loadForm(location)
  }

  changeStatus(status: boolean, data) {    
    this.service.updateToUrl(`LessonLocations/${data.id}`, data).subscribe(r => {
        this.nzNotification.success('Success', `${data.name} ${status ? 'Activated' : 'Deactivated'}`, { nzDuration: 10000, nzAnimate: true });
        this.getLocations()
      });
  }
  
  changeCostStatus(status: boolean, data) {    
    this.service.updateToUrl(`LessonLocations/${data.id}`, data).subscribe(r => {
        this.nzNotification.success('Success', `${data.name} ${status ? 'Travelling Costs Activated' : 'Travelling Costs Deactivated'}`, { nzDuration: 10000, nzAnimate: true });
        this.getLocations();
      });
  }

}