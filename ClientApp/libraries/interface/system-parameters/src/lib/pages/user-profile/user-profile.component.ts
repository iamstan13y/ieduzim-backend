import { Component, OnInit } from '@angular/core';
import { FieldType, IForm } from '@iedu-ui-forms';
import { AuthService } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NzNotificationService } from 'ng-zorro-antd';
import { EMAILFIELD, PHONENUMBERFIELD} from '../../forms/profile.form';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  user: any = {};
  visible: boolean;
  title: string;
  form: IForm;
  reset: boolean = false;

  constructor(private auth: AuthService,private service: Service, private notification: NzNotificationService) { 
    service.get(`Users/${auth.tokenPayload["UserId"]}`).subscribe(res=> {
      if(res.succeeded)
        this.user = {...res.data, role: auth.tokenPayload["Role"]}
    })
    
  }

  ngOnInit() {
  }

  updateDetails(details){
    this.title = details.title;
    this.form = {
      fields: this.title == 'Email' ? EMAILFIELD(details) : PHONENUMBERFIELD(details),
      buttons: [
        { name: 'Save', action: res=> this.saveDetails(res), class: 'primary', validates: true },
        { name: 'Cancel', action: ()=> this.toggleDrawer(false), validates: false }
      ]
    }
    this.toggleDrawer(true)
   
  }

  saveDetails(details){
      var userDetails = { ...details, id: this.auth.tokenPayload["UserId"]}
    this.service.updateToUrl('Users', userDetails).subscribe(res=> {
      if(res.succeeded){
          this.notification.success('','Successfully updated!', {nzAnimate: true, nzDuration: 10000 })
          this.user = {...res.data, role: this.auth.tokenPayload["Role"]}
      }
      else this.notification.warning('Oops', res.message, {nzDuration: 10000, nzAnimate: true})
      this.toggleDrawer(false)
    })
  }

  toggleDrawer(visible){ this.visible = visible}

  toggleReset(visible){ this.reset = visible}

}
