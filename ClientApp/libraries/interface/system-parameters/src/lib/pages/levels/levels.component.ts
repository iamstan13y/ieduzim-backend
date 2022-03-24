import { Component, OnInit } from '@angular/core';
import { IForm } from '@iedu-ui-forms';
import { PageResult } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NameModel } from 'libraries/data/system-parameters/src/lib/models/name-model';
import { NzNotificationService } from 'ng-zorro-antd';
import { ACTIVEFIELDS, NAMEFIELDS } from '../../forms/name-model.form';

@Component({
  selector: 'app-levels',
  templateUrl: './levels.component.html',
  styleUrls: []
})
export class LevelsComponent implements OnInit {
  levels: PageResult<NameModel[]>;
  visible: any;
  form: IForm;
  level: any;
  index: number = 1;
  loading: boolean = false;

  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getLevels();
   }

  getLevels(e?){
    this.level = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 8},'Level/paged').subscribe(res=> {
      if(res.succeeded){
        this.levels = res;
        this.loadForm();
      }
      else this.nzNotification.error('Error', res.message, {nzAnimate: true, nzDuration: 10000 })      
    })
  }

  loadForm(level?){
    this.form = {
      fields: [...NAMEFIELDS(level), ...ACTIVEFIELDS(level)],
      buttons: [
        { name: 'Save Level', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: ()=> this.toggleDrawer(false)}
      ]
    }
  }

  save(request){
    this.loading = true;
    if(!this.level)
      this.service.postToUrl('Level', request).subscribe(res=> this.requestResult(res))
    else this.service.updateToUrl(`Level/${this.level.id}`, {id: this.level.id, ...request}).subscribe(res=>{
      this.requestResult(res);
    })
  }

  delete(level){
    this.service.delete(`Level/${level.id}`).subscribe(res=> this.requestResult(res));
  }

  requestResult(res){
    if(res.succeeded) 
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true})
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000})
    this.loading = false;
    this.toggleDrawer(false)
    this.getLevels();
  }

  edit(level){
    this.level = level;
    this.toggleDrawer(true, level)
  }

  toggleDrawer(visible, level?){
    this.visible = visible;
    this.loadForm(level)
  }

  changeStatus(status: boolean, data) {    
    this.service.updateToUrl(`Level/${data.id}`, data).subscribe(r => {
        this.nzNotification.success('Success', `${data.name} ${status ? 'Activated' : 'Deactivated'}`, { nzDuration: 10000, nzAnimate: true });
        this.getLevels()
      });
  }
}
