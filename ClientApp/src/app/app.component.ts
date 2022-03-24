import { Component } from '@angular/core';
import { AuthService } from '@iedu/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {
  title = 'IEduZim';

  constructor(private authService: AuthService) { }
  ngOnInit() {
    if (this.authService.isLoggedIn()){
      var s = new Date().getTime();
      setTimeout(()=> {
        this.authService.clearToken();
        window.location.href = `${window.location.origin}/account/login?returnUrl=${window.location.pathname}`;     
      }, (this.authService.tokenPayload.exp*1000)-s
      )
    }
  }
}
