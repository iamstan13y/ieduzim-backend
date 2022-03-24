import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RequestService } from '@iedu/core';

@Injectable()
export class Service extends RequestService<any> {
  constructor(httpClient: HttpClient) {
    super(httpClient)
  }
}