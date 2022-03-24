import { Component, OnInit } from '@angular/core';
import { IForm } from '@iedu-ui-forms';
import { PageResult } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NzNotificationService } from 'ng-zorro-antd';
import { CODEEFIELDS } from '../../forms/currency.form';
import { ACTIVEFIELDS, NAMEFIELDS } from '../../forms/name-model.form';

@Component({
  selector: 'app-currencies',
  templateUrl: './currencies.component.html',
  styleUrls: []
})
export class CurrenciesComponent implements OnInit {
  currencies: PageResult<any[]>;
  visible: any;
  form: IForm;
  currency: any;
  index: number = 1;
  loading: boolean = false;

  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getCurrencies();
   }

  getCurrencies(e?){
    this.currency = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 8},'Currency/paged').subscribe(res=> {
      if(res.succeeded){
        this.currencies = res;
        this.loadForm();
      }
      else this.nzNotification.error('Error', res.message, {nzAnimate: true, nzDuration: 10000 })      
    })
  }

  loadForm(currency?){
    this.form = {
      fields: [...NAMEFIELDS(currency), ...CODEEFIELDS(currency), ...ACTIVEFIELDS(currency)],
      buttons: [
        { name: 'Save Currency', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: ()=> this.toggleDrawer(false)}
      ]
    }
  }

  save(request){
    this.loading = true;
    if(!this.currency)
      this.service.postToUrl('Currency', request).subscribe(res=> this.requestResult(res))
    else this.service.updateToUrl(`Currency/${this.currency.id}`, {id: this.currency.id, ...request}).subscribe(res=>{
      this.requestResult(res);
    })
  }

  delete(level){
    this.service.delete(`Currency/${level.id}`).subscribe(res=> this.requestResult(res));
  }

  requestResult(res){
    if(res.succeeded) 
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true})
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000})
    this.loading = false;
    this.toggleDrawer(false)
    this.getCurrencies();
  }

  edit(currency){
    this.currency = currency;
    this.toggleDrawer(true, currency)
  }

  toggleDrawer(visible, currency?){
    this.visible = visible;
    this.loadForm(currency)
  }

  changeStatus(status: boolean, data) {    
    this.service.updateToUrl(`Currency/${data.id}`, data).subscribe(r => {
        this.nzNotification.success('Success', `${data.name} ${status ? 'Activated' : 'Deactivated'}`, { nzDuration: 10000, nzAnimate: true });
        this.getCurrencies()
      });
  }
}
