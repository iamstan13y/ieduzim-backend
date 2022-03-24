import { Component, OnInit } from '@angular/core';
import { IForm } from '@iedu-ui-forms';
import { PageResult } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NzNotificationService } from 'ng-zorro-antd';

@Component({
  selector: 'app-awaiting-verification',
  templateUrl: './awaiting-verification.component.html',
  styleUrls: []
})
export class AwaitingVerificationComponent implements OnInit {
  teachers: PageResult<any[]>;
  visible: any;
  form: IForm;
  teacher: any;
  index: number = 1;
  loading: boolean = false;
  modalVisible: boolean = false;
  modalData: any;
  activeDocuments: any;
  documentVisible: any;
  url: string;

  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getTeachers();
  }

  toggleModal(visible, modalData) {
    this.modalData = modalData;
    this.modalVisible = visible;
  }

  toggleDocumentModal(visible, url?) {
    this.documentVisible = visible;
    this.url = url;
  }

  toggleDrawer(visible, data?) {
    this.visible = visible;
    this.teacher = data;
    this.activeDocuments = null;
    if (this.visible)
      this.service.get(`TeacherDocuments/by-user-id/${this.teacher.userId}`).subscribe(res => {
        this.activeDocuments = res.data;
      })
  }

  getTeachers(e?) {
    this.teacher = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 10 }, 'Teachers/subscribed-and-not-verified').subscribe(res => {
      if (res.succeeded) {
        this.teachers = res;
      }
      else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000 })
    })
  }

  save(request) {
    this.loading = true;
    this.service.updateToUrl(`TeacherDocuments/update-verification/${request.userId}`, {}).subscribe(res => {
      this.requestResult(res);
    })
  }

  authenticate(request) {
    this.loading = true;
    this.service.get(`TeacherDocuments/authenticate/${request.userId}/${request.id}`).subscribe(res => {
      if (res.succeeded)
        this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true })
      else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000 })
      this.loading = false;
      this.toggleDrawer(false);
      this.toggleDocumentModal(false);
      this.getTeachers();
    })
  }

  requestResult(res) {
    if (!res.data) this.nzNotification.error('Error', 'You can only mark the Teacher as verified if all qualification documents have been authenticated.', { nzAnimate: true, nzDuration: 10000 })
    else if (res.succeeded)
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true })
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000 })
    this.loading = false;
    this.getTeachers();
  }
}
