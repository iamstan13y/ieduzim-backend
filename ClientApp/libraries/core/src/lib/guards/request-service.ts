import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable, empty } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { PageRequest, REQUESTPARAMS, PageResult } from '../models';

export class RequestService<T> {
  loader: boolean;
  
  public get(url?: string): Observable<any> {
    if(!url) url = '';
    return this.defaultHttpClient.get(`api/${url}`)
      .pipe(catchError(() => empty()));
  }

  public getPaginated(request: PageRequest, url):Observable<any>{
    if(!url) url = '';
    url+=`${REQUESTPARAMS(request)}`;
    return this.get(url)
  }

  public getById(id: number | string): Observable<T> {
    return this.get(`/${id}`);
  }

  public post(body: T): Observable<any> {
    return this.postToUrl('', body);
  }

  public postToUrl(url: string, body: any): Observable<any> {
    return this.defaultHttpClient.post(`api/${url}`, body, {
      observe: 'response'
    }).pipe(map((response: HttpResponse<any>) => response.body));
  }

  public update(body: T): Observable<any> {
    return this.updateToUrl('', body);
  }

  public updateToUrl(url: string, body: any): Observable<any> {
    return this.defaultHttpClient.put(`api/${url}`, body, {
      observe: 'response'
    }).pipe(map((response: HttpResponse<any>) => response.body));
  }

  public delete(url: string): Observable<any> {
    return this.defaultHttpClient.delete(`api/${url}`, {
      observe: 'response'
    }).pipe(map((response: HttpResponse<any>) => response.body));
  }

  public search(parameter: any): Observable<T> {
    return this.get(`?${parameter}`);
  }

  constructor(public defaultHttpClient: HttpClient, public url?: string) { 
  }
}
