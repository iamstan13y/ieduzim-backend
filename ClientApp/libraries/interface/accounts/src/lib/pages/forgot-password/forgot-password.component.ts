import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NzNotificationService } from 'ng-zorro-antd';
import { Service } from 'libraries/data/accounts/src/lib/services/service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['../external-forms.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  email: string;

  constructor(private service: Service, private notification: NzNotificationService, private router: Router) { }

  ngOnInit() {
  }

  forgotPasswordRequest(){
    this.service.postToUrl('Recovery/forgot',  {email: this.email} ).subscribe(res=>{
        if(res.succeeded) 
          this.notification.success('Success', res.message, { nzAnimate: true, nzDuration: 10000})
        else this.notification.error('Error',res.message, { nzAnimate: true, nzDuration: 10000})
        this.router.navigate(['/account/login']);
    })
  }
}
