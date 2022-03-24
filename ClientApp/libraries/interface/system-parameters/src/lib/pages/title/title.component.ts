import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { IForm } from '@iedu-ui-forms';
import { NameModelService } from '@iedu-data-system-parameters';
import { NzNotificationService } from 'ng-zorro-antd';
import { ACTIVEFIELDS, NAMEFIELDS } from '../../forms/name-model.form';
import { NameModel } from 'libraries/data/system-parameters/src/lib/models/name-model';
import { PageResult } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';

@Component({
  selector: 'app-title',
  templateUrl: './title.component.html',
  styleUrls: []
})
export class TitleComponent implements OnInit {
  titles: PageResult<NameModel[]>;
  visible: any;
  form: IForm;
  title: any;
  index: number = 1;
  loading: boolean = false;

  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getTitles();
   }

  getTitles(e?){
    this.title = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 8},'Titles/paged').subscribe(res=> {
      if(res.succeeded){
        this.titles = res;
        this.loadForm();
      }
      else this.nzNotification.error('Error', res.message, {nzAnimate: true, nzDuration: 10000 })      
    })
  }

  loadForm(title?){
    this.form = {
      fields: [...NAMEFIELDS(title), ...ACTIVEFIELDS(title)],
      buttons: [
        { name: 'Save Title', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: ()=> this.toggleDrawer(false)}
      ]
    }
  }

  save(request){
    this.loading = true;
    if(!this.title)
      this.service.postToUrl('Titles', request).subscribe(res=> this.requestResult(res))
    else this.service.updateToUrl(`Titles/${this.title.id}`, {id: this.title.id, ...request}).subscribe(res=>{
      this.requestResult(res);
    })
  }

  delete(title){
    this.service.delete(`Titles/${title.id}`).subscribe(res=> this.requestResult(res));
  }

  requestResult(res){
    if(res.succeeded) 
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true})
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000})
    this.loading = false;
    this.toggleDrawer(false)
    this.getTitles();
  }

  edit(title){
    this.title = title;
    this.toggleDrawer(true, title)
  }

  toggleDrawer(visible, title?){
    this.visible = visible;
    this.loadForm(title)
  }

  changeStatus(status: boolean, data) {    
    this.service.updateToUrl(`Titles/${data.id}`, data).subscribe(r => {
        this.nzNotification.success('Success', `${data.name} ${status ? 'Activated' : 'Deactivated'}`, { nzDuration: 10000, nzAnimate: true });
        this.getTitles()
      });
  }
}
