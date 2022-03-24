import { Component, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IForm } from '@iedu-ui-forms';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NzNotificationService } from 'ng-zorro-antd';
import { ACTIVEFIELDS, NAMEFIELDS } from '../../forms/name-model.form';

@Component({
  selector: 'app-view-level-details',
  templateUrl: './view-level-details.component.html',
  styleUrls: []
})
export class ViewLevelDetailsComponent implements OnInit {
  level: any;
  form: IForm;
  visible: boolean;

  constructor(activatedRoute: ActivatedRoute, private service: Service, private nzNotification: NzNotificationService) {
    this.level = activatedRoute.snapshot.data.level.data;
  }

  ngOnInit() {
  }

  loadForm() {
    this.toggleDrawer(true);
    this.form = {
      fields: [...NAMEFIELDS(this.level), ...ACTIVEFIELDS(this.level)],
      buttons: [
        { name: 'Save Level', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: () => this.toggleDrawer(false) }
      ]
    }
  }

  toggleDrawer(visible) {
    this.visible = visible;
  }

  save(request) {
    this.service.updateToUrl(`Level/${this.level.id}`, { id: this.level.id, ...request }).subscribe(res => {
      if (res.succeeded)
        this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true })
      else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000 })
      this.toggleDrawer(false)
      this.getLevel();
    })
  }

  getLevel(){
    this.service.get(`Level/${this.level.id}`).subscribe(res=> {
      if(res.succeeded)
         this.level = res.data 
      else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000 })
    })
  }
}
