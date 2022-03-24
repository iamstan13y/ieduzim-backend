import { Component, OnInit } from '@angular/core';
import { IForm } from '@iedu-ui-forms';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NzNotificationService } from 'ng-zorro-antd';
import { ACTIVEFIELDS, NAMEFIELDS } from '../../forms/name-model.form';
import { PERIODSFIELDS } from '../../forms/payment-period.form';

@Component({
  selector: 'app-payment-periods',
  templateUrl: './payment-periods.component.html',
  styleUrls: []
})
export class PaymentPeriodsComponent implements OnInit {
  index: number = 1;
  paymentPeriod: any;
  paymentPeriods: any;
  form: IForm;
  loading: boolean;
  visible: any;
  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getPaymentPeriods();
   }

   getPaymentPeriods(e?){
    this.paymentPeriod = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 8},'PaymentPeriods/paged').subscribe(res=> {
      if(res.succeeded){
        this.paymentPeriods = res;
        this.loadForm();
      }
      else this.nzNotification.error('Error', res.message, {nzAnimate: true, nzDuration: 10000 })      
    })
  }

  loadForm(paymentPeriod?){
    this.form = {
      fields: [
        ...NAMEFIELDS(paymentPeriod),
        ...PERIODSFIELDS(paymentPeriod),
        ...ACTIVEFIELDS(paymentPeriod)
      ],
      buttons: [
        { name: 'Save Fee', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: ()=> this.toggleDrawer(false)}
      ]
    }
  }

  save(request){
    this.loading = true;
    if(!this.paymentPeriod)
      this.service.postToUrl('PaymentPeriods', request).subscribe(res=> this.requestResult(res))
    else this.service.updateToUrl(`PaymentPeriods/${this.paymentPeriod.id}`, {id: this.paymentPeriod.id, ...request}).subscribe(res=>{
      this.requestResult(res);
    })
  }

  delete(paymentPeriod){
    this.service.delete(`PaymentPeriods/${paymentPeriod.id}`).subscribe(res=> this.requestResult(res));
  }

  requestResult(res){
    if(res.succeeded) 
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true})
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000})
    this.loading = false;
    this.toggleDrawer(false)
    this.getPaymentPeriods();
  }

  edit(paymentPeriod){
    this.paymentPeriod = paymentPeriod;
    this.toggleDrawer(true, paymentPeriod)
  }

  toggleDrawer(visible, paymentPeriod?){
    this.visible = visible;
    this.loadForm(paymentPeriod)
  }

  changeStatus(status: boolean, data) {    
    this.service.updateToUrl(`PaymentPeriods/${data.id}`, data).subscribe(r => {
        this.nzNotification.success('Success', `${data.name} ${status ? 'Activated' : 'Deactivated'}`, { nzDuration: 10000, nzAnimate: true });
        this.getPaymentPeriods()
      });
  }
}
