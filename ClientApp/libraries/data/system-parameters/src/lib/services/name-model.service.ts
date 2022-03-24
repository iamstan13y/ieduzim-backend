import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RequestService } from '@iedu/core';
import { NameModel } from '../models/name-model';

@Injectable()
export class NameModelService extends RequestService<NameModel> {
  constructor(httpClient: HttpClient) {
    super(httpClient)
  }
}