import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ResetPassword } from '@iedu-data-accounts';
import { NzNotificationService } from 'ng-zorro-antd';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { AuthService, PasswordService } from 'libraries/core/src/lib/guards';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['../external-forms.component.css']
})
export class ResetPasswordComponent implements OnInit {

  resetPassword: any = {};
  view: boolean;
  vw: boolean;
  viewConfirmation: boolean;
  matchError: boolean;
  message: string;
  @Output() toggle = new EventEmitter<any>();

  constructor(private service: Service, private notification: NzNotificationService, private router: Router, public pService: PasswordService, private authService: AuthService) {
     }

  ngOnInit() {
  }

  toggleView() {
    this.view = !this.view;
  }

  toggleViewConfirmation() {
    this.viewConfirmation = !this.viewConfirmation;
  }

  comparePassword() {
    this.resetPassword.newPassword != this.resetPassword.newPasswordConfirm ?
      this.matchError = true : this.matchError = false;
  }

  reset() {
    this.resetPassword.userId = this.authService.tokenPayload["UserId"];
    this.service.postToUrl('Recovery/change', this.resetPassword).subscribe(res => {
      if (res.succeeded){
        this.notification.success('Success',res.message, { nzDuration: 10000, nzAnimate: true})
        this.router.navigateByUrl(`account/login`)
      }
      else this.notification.error( 'Error',res.message, { nzAnimate: true, nzDuration: 10000})
    })
  }

  validate() {
    if (this.resetPassword.newPassword && this.resetPassword.newPasswordConfirm && this.resetPassword.currentPassword
       && !this.matchError && !this.pService.message)
      return false;
    return true
  }

  validatePassword(){ this.pService.validatePassword(this.resetPassword.newPassword)}
}
