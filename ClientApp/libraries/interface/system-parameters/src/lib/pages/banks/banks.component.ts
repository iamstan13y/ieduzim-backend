import { Component, OnInit } from '@angular/core';
import { Bank } from '@iedu-data-system-parameters';
import { IForm } from '@iedu-ui-forms';
import { PageResult } from '@iedu/core';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NzNotificationService } from 'ng-zorro-antd';
import { ACTIVEFIELDS, NAMEFIELDS } from '../../forms/name-model.form';

@Component({
  selector: 'app-banks',
  templateUrl: './banks.component.html',
  styleUrls: []
})
export class BanksComponent implements OnInit {
  banks: PageResult<Bank[]>;
  visible: any;
  form: IForm;
  bank: any;
  index: number = 1;
  loading: boolean = false;

  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getBanks();
   }

  getBanks(e?){
    this.bank = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 8},'Banks/paged').subscribe(res=> {
      if(res.succeeded){
        this.banks = res;
        this.loadForm();
      }
      else this.nzNotification.error('Error', res.message, {nzAnimate: true, nzDuration: 10000 })      
    })
  }

  loadForm(bank?){
    this.form = {
      fields: [...NAMEFIELDS(bank), ...ACTIVEFIELDS(bank)],
      buttons: [
        { name: 'Save Bank', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: ()=> this.toggleDrawer(false)}
      ]
    }
  }

  save(request){
    this.loading = true;
    if(!this.bank)
      this.service.postToUrl('Banks', request).subscribe(res=> this.requestResult(res))
    else this.service.updateToUrl(`Banks/${this.bank.id}`, {id: this.bank.id, ...request}).subscribe(res=>{
      this.requestResult(res);
    })
  }

  delete(bank){
    this.service.delete(`Banks/${bank.id}`).subscribe(res=> this.requestResult(res));
  }

  requestResult(res){
    if(res.succeeded) 
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true})
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000})
    this.loading = false;
    this.toggleDrawer(false)
    this.getBanks();
  }

  edit(bank){
    this.bank = bank;
    this.toggleDrawer(true, bank)
  }

  toggleDrawer(visible, bank?){
    this.visible = visible;
    this.loadForm(bank)
  }

  changeStatus(status: boolean, data) {    
    this.service.updateToUrl(`Banks/${data.id}`, data).subscribe(r => {
        this.nzNotification.success('Success', `${data.name} ${status ? 'Activated' : 'Deactivated'}`, { nzDuration: 10000, nzAnimate: true });
        this.getBanks()
      });
  }
}
