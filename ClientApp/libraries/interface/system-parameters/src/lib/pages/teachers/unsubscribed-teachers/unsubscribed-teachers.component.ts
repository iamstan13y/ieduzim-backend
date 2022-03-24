import { Component, OnInit } from '@angular/core';
import { IForm } from '@iedu-ui-forms';
import { PageResult } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NzNotificationService } from 'ng-zorro-antd';

@Component({
  selector: 'app-unsubscribed-teachers',
  templateUrl: './unsubscribed-teachers.component.html',
  styleUrls: []
})
export class UnsubscribedTeachersComponent implements OnInit {
  teachers: PageResult<any[]>;
  visible: any;
  form: IForm;
  teacher: any;
  index: number = 1;
  loading: boolean = false;
  modalVisible: boolean = false;
  modalData: any;

  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getTeachers();
  }

  toggleModal(visible, modalData){
    this.modalData = modalData;
    this.modalVisible = visible;
  }

  toggleDrawer(visible, data?){
    this.visible = visible;
    this.teacher = data;
  }

  getTeachers(e?) {
    this.teacher = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 10 }, 'Teachers/unsubscribed').subscribe(res => {
      if (res.succeeded) {
        this.teachers = res;
      }
      else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000 })
    })
  }

  save(request) {
    this.loading = true;
    this.service.updateToUrl(`Teachers/unsubscribed/${request.userId}`, request).subscribe(res => {
      this.requestResult(res);
    })
  }

  requestResult(res) {
    if (res.succeeded)
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true })
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000 })
    this.loading = false;
    this.getTeachers();
  }
}
