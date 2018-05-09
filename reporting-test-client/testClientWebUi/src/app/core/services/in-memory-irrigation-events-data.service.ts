import { Injectable } from '@angular/core';
import { InMemoryDbService, RequestInfo, STATUS, getStatusText} from 'angular-in-memory-web-api';
import { IrrigationEvent } from './irrigation-event';

import { of } from 'rxjs/observable/of';
import { delay } from 'rxjs/operators';
import { ResponseOptions, ResponseOptionsArgs } from '@angular/http';

const findEvents = [
  { id: 1, journalId: 11, bearing: 180, pump: true, rotation: 'forward', substance: 'water' },
  { id: 2, journalId: 12, bearing: 185, pump: true, rotation: 'forward', substance: 'water' },
  { id: 3, journalId: 13, bearing: 190, pump: true, rotation: 'forward', substance: 'water' },
  { id: 4, journalId: 14, bearing: 195, pump: true, rotation: 'forward', substance: 'water' },
  { id: 5, journalId: 15, bearing: 200, pump: true, rotation: 'forward', substance: 'water' }
];

@Injectable()
export class InMemoryIrrigationEventsDataService implements InMemoryDbService {

  post(reqInfo: RequestInfo) {
    const collectionName = reqInfo.collectionName;
    if (collectionName === 'findEvents') {
      return this.postFindEventsResponse(reqInfo);
    }
  }

  private postFindEventsResponse(reqInfo: RequestInfo) {
    return reqInfo.utils.createResponse$(() => {
      console.log('http post override');
      const collection = findEvents.slice();
      const dataEncapsulation = reqInfo.utils.getConfig().dataEncapsulation;
      const id = reqInfo.id;

      // tslint:disable-next-line:triple-equals
      const data = id == undefined ? collection : reqInfo.utils.findById(collection, id);

      const options: ResponseOptions = data ? new ResponseOptions({
        body: dataEncapsulation ? { data } : data,
        status: STATUS.OK
      }) : new ResponseOptions({
        body: {error: `'IrrigationEvents' with id='${id}' not found`},
        status: STATUS.NOT_FOUND
      });
      return this.finishOptions(options, reqInfo);
    });
  }

  private finishOptions(options: ResponseOptions, {headers, url}: RequestInfo) {
    options.url = url;
    return options;
  }

  createDb(reqInfo?: RequestInfo) {
    const irrigationEvents = [
      { journalId: 1, bearing: 180, pump: true, rotation: 'forward', substance: 'water' },
      { journalId: 2, bearing: 185, pump: true, rotation: 'forward', substance: 'water' },
      { journalId: 3, bearing: 190, pump: true, rotation: 'forward', substance: 'water' },
      { journalId: 4, bearing: 195, pump: true, rotation: 'forward', substance: 'water' },
      { journalId: 5, bearing: 200, pump: true, rotation: 'forward', substance: 'water' }
    ];

    let returnType = 'object';
    if (reqInfo) {
      const body = reqInfo.utils.getJsonBody(reqInfo.req) || {};
      if (body.clear === true) {
        irrigationEvents.length = 0;
      }

      returnType = body.returnType || 'object';
    }

    const db = { irrigationEvents: irrigationEvents};

    switch (returnType) {
      case ('observable'):
          return of(db).pipe(delay(10));
      case ('promise'):
        return new Promise(resolve => {
          setTimeout(() => resolve(db), 10);
        });
      default:
          return db;
    }
  }

  constructor() { }

}
