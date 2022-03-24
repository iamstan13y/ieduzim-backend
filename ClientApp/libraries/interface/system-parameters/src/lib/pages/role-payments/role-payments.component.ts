import { Component, OnInit } from '@angular/core';
import { IForm } from '@iedu-ui-forms';
import { PageResult } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NzNotificationService } from 'ng-zorro-antd';
import { ROLEPAYMENTPERIODFIELDS } from '../../forms/role-payment-period-form';

@Component({
  selector: 'app-role-payments',
  templateUrl: './role-payments.component.html',
  styleUrls: ['./role-payments.component.css']
})
export class RolePaymentsComponent implements OnInit {
  rolePayments: PageResult<any[]>;
  visible: any;
  form: IForm;
  rolePayment: any;
  index: number = 1;
  loading: boolean = false;
  roles: any[];

  constructor(private service: Service, private nzNotification: NzNotificationService) { 
  }

  ngOnInit() {
    this.getRolePaymentPeriods();
   }

   getRolePaymentPeriods(e?){
    this.rolePayment = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 8},'RolePayments/all').subscribe(res=> {
      if(res.succeeded){
        this.rolePayments = res.data;
        this.loadForm();
      }
      else this.nzNotification.error('Error', res.message, {nzAnimate: true, nzDuration: 10000 })      
    })
  }

  loadForm(data?){
    this.form = {
      fields: ROLEPAYMENTPERIODFIELDS(data),
      buttons: [
        { name: 'Save Role Payment Period', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: ()=> this.toggleDrawer(false)}
      ]
    }
  }

  changeStatus(status: boolean, data) {    
    this.service.updateToUrl(`RolePayments/${data.id}`, data).subscribe(r => {
        this.nzNotification.success('Success', `${status ? 'Activated' : 'Deactivated'}`, { nzDuration: 10000, nzAnimate: true });
        this.getRolePaymentPeriods();
      });
  }

  save(request){
    this.loading = true;
    if(!this.rolePayment)
      this.service.postToUrl('RolePayments', request).subscribe(res=> this.requestResult(res))
    else this.service.updateToUrl(`RolePayments/${this.rolePayment.id}`, {id: this.rolePayment.id, ...request}).subscribe(res=>{
      this.requestResult(res);
    })
  }

  delete(data){
    this.service.delete(`RolePayments/${data.id}`).subscribe(res=> this.requestResult(res));
  }

  requestResult(res){
    if(res.succeeded) 
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true})
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000})
    this.loading = false;
    this.toggleDrawer(false)
    this.getRolePaymentPeriods();
  }

  edit(data){
    this.rolePayment = data;
    this.toggleDrawer(true, data);
  }

  toggleDrawer(visible, data?){
    this.visible = visible;
    this.loadForm(data)
  }
}