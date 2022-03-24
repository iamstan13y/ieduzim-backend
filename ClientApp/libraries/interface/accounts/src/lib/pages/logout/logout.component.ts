import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { AuthService } from 'libraries/core/src/lib/guards';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: []
})
export class LogoutComponent implements OnInit {

  constructor(private service: Service, private authService: AuthService) { }

  ngOnInit() {
    this.service.postToUrl('Auth/logout', null)
      .subscribe(res=> {
        if(res.succeeded){
          window.location.href = '/'
          this.authService.clearToken()
        }
      });
  }

}
