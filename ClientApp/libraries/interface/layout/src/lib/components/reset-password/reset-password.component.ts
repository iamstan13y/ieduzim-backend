import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { PasswordService, AuthService, RequestService } from '@iedu/core';
import { NzNotificationService } from 'ng-zorro-antd';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.less']
})
export class ResetPasswordComponent implements OnInit {
  @Input() visible: any;
  currentPassword: string;
  newPassword: string;
  confirm: string;
  view: boolean;
  viewConfirmation: boolean;
  viewC: boolean = false;
  matchError: boolean;
  confirmPassword: string;
  @Output() result = new EventEmitter<any>()
  service: any;

  constructor(private authService: AuthService, public pService: PasswordService, private notification: NzNotificationService, http: HttpClient) {
    this.service = new RequestService<any>(http, '')
   }

  ngOnInit() {
  }

  ngOnChanges(){
    this.toggleDrawer(this.visible)
  }
  
  toggleDrawer(visible) { this.visible = visible }

  comparePassword() {
    this.newPassword != this.confirmPassword ?
      this.matchError = true : this.matchError = false;
  }

  change() {
    this.service.postToUrl('Recovery/change', { userId: this.authService.tokenPayload.sub, newPassword: this.newPassword, currentPassword: this.currentPassword}).subscribe(res => {
      if (res.succeeded)
        this.notification.success('Success',res.message, { nzDuration: 10000, nzAnimate: true}) 
      else this.notification.error( 'Error',res.message, { nzAnimate: true, nzDuration: 10000});
      this.result.emit(false)
    })
  }

  validate() {
    if (this.newPassword && this.confirmPassword && this.currentPassword
       && !this.matchError && !this.pService.message)
      return false;
    return true
  }

  validatePassword(){ this.pService.validatePassword(this.newPassword)}
}
