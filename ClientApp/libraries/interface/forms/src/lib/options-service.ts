import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { PageRequest, RequestService } from '@iedu/core';
import { IField, Options } from './model';

@Injectable()
export class OptionsService {
  constructor(private httpClient: HttpClient) { }

  private getDefaults(pageRequest: PageRequest): PageRequest {
    if (!pageRequest) return null;
    pageRequest.pageSize = pageRequest.pageSize;
    pageRequest.pageNumber = pageRequest.pageNumber;
    return pageRequest;
  }

  getData(option: Options): Observable<any> {
    let api = option;
    api.pageRequest = this.getDefaults(api.pageRequest);
    let service = new RequestService(this.httpClient);
    if (api.pageRequest){
      api.pageRequest.pageSize = api.pageRequest.pageSize || 1000;
      return service.getPaginated(api.pageRequest, api.url).pipe(map(result => {
        api.pageRequest.last = result.pageNumber == result.totalPages ? true : false;
        return { pageRequest: api.pageRequest, pageResult: result };
      }));
    }
    else return service.get(api.url) .pipe(map(result=> {
        return { pageRequest: api.pageRequest, pageResult: result };
    }))   
  }
}
