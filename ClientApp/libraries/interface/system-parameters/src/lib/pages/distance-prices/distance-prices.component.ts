import { Component, OnInit } from '@angular/core';
import { IForm } from '@iedu-ui-forms';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NzNotificationService } from 'ng-zorro-antd';
import { TRAVELINGCOSTSFIELDS } from '../../forms/traveling-costs.form';

@Component({
  selector: 'app-distance-prices',
  templateUrl: './distance-prices.component.html',
  styleUrls: []
})
export class DistancePricesComponent implements OnInit {
  index: number = 1;
  cost: any;
  costs: any;
  form: IForm;
  loading: boolean;
  visible: any;
  constructor(private service: Service, private nzNotification: NzNotificationService) { }

  ngOnInit() {
    this.getDistanceCosts();
   }

   getDistanceCosts(e?){
    this.cost = null;
    this.service.getPaginated({ pageNumber: this.index, pageSize: 8},'DistancePrices/paged-costs').subscribe(res=> {
      if(res.succeeded){
        this.costs = res;
        this.loadForm();
      }
      else this.nzNotification.error('Error', res.message, {nzAnimate: true, nzDuration: 10000 })      
    })
  }

  loadForm(cost?){
    this.form = {
      fields: [...TRAVELINGCOSTSFIELDS(cost)],
      buttons: [
        { name: 'Save Fee', validates: true, action: res => this.save(res), class: 'primary' },
        { name: 'Cancel', validates: false, action: ()=> this.toggleDrawer(false)}
      ]
    }
  }

  save(request){
    this.loading = true;
    if(!this.cost)
      this.service.postToUrl('DistancePrices', request).subscribe(res=> this.requestResult(res))
    else this.service.updateToUrl(`DistancePrices/${this.cost.id}`, {id: this.cost.id, ...request}).subscribe(res=>{
      this.requestResult(res);
    })
  }

  delete(cost){
    this.service.delete(`DistancePrices/${cost.id}`).subscribe(res=> this.requestResult(res));
  }

  requestResult(res){
    if(res.succeeded) 
      this.nzNotification.success('Success', res.message, { nzDuration: 10000, nzAnimate: true})
    else this.nzNotification.error('Error', res.message, { nzAnimate: true, nzDuration: 10000})
    this.loading = false;
    this.toggleDrawer(false)
    this.getDistanceCosts();
  }

  edit(cost){
    this.cost = cost;
    this.toggleDrawer(true, cost)
  }

  toggleDrawer(visible, city?){
    this.visible = visible;
    this.loadForm(city)
  }

}
