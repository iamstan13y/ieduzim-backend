import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NzNotificationService } from 'ng-zorro-antd';
import { Login } from '@iedu-data-accounts';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { AuthService } from 'libraries/core/src/lib/guards';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../external-forms.component.css']
})
export class LoginComponent implements OnInit {
  view: boolean;
  login: Login | any = {};
  returnUrl: any;

  constructor(private service: Service, private notification: NzNotificationService, private authService: AuthService,
    private route: ActivatedRoute) { }

  ngOnInit() { 
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '';
  }

  toggleView() {
    this.view = !this.view;
  }

  validate() {
    if (this.login.password && this.login.username)
      return false;
    return true
  }

  loginRequest() {
    this.service.postToUrl('Auth', this.login).subscribe(res => {
      if (res.succeeded) {
        this.authService.setToken(res.data.token);
        location.href = this.returnUrl;
      }
      else this.notification.error('Error', res.message, { nzDuration: 10000, nzAnimate: true })
    })
  }
}
