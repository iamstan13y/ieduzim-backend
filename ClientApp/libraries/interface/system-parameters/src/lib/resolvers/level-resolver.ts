import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Service } from 'libraries/data/accounts/src/lib/services/service';
import { NameModel } from 'libraries/data/system-parameters/src/lib/models/name-model';

@Injectable()
export class LevelResolver implements Resolve<any> {
  constructor(private service: Service) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    Observable<any>
    | Promise<any>
    | NameModel {
    const id = route.params['id'];
    return this.service.get(`Level/${id}`);
  }
}