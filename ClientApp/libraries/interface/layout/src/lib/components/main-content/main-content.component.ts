import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '@iedu/core';

@Component({
  selector: 'app-main-content',
  templateUrl: './main-content.component.html',
  styleUrls: ['./main-content.component.css']
})
export class MainContentComponent implements OnInit {
  user: any;
  isCollapsed: boolean = false;
  route: any;

  constructor(authService: AuthService) { 
    this.route = location.pathname;
    if(authService.isAuthenticated())
      this.user = authService.tokenPayload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'].toUpperCase();
      
  }

  ngOnInit() {
  }

  toggle($event){
    this.isCollapsed = $event;
  }

}
