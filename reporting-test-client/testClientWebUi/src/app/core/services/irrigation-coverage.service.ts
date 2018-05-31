import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { IrrigationEvent } from './irrigation-event';
import { catchError, map, tap } from 'rxjs/operators';
import { RequestOptions } from '@angular/http';
import { environment } from '../../../environments/environment';
import { IrrigationEventResponse } from './irrigation-events-response';

const options = {headers: new HttpHeaders({'Content-Type': 'application/json' })};

@Injectable()
export class IrrigationCoverageService {
  baseUri = environment.baseUrl;
  findEventsUri = this.baseUri + 'api/findEvents/';
  irrigationEventsUri = this.baseUri + 'api/irrigationEvents';

  findEvents(queryData: any): Observable<IrrigationEventResponse> {
    const pivotId: number = queryData.pivotId;
    const data = {
      startDate: queryData.startDate,
      stopDate: queryData.stopDate,
      startBearing: queryData.startBearing,
      stopBearing: queryData.stopBearing,
      startAt: queryData.startTime,
      stopAt: queryData.stopTime
    };
    const uri = `${this.irrigationEventsUri}/${pivotId}`;
    return this.http.post<IrrigationEventResponse>(uri, data, options)
      .pipe(
        tap(irrigationEvents => this.log(`fetched irrigation events via post`)),
        catchError(this.handleError('findEvents', new IrrigationEventResponse))
      );
  }

  constructor( private http: HttpClient ) { }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  private log(message: string) {
    // tslint:disable-next-line:no-console
    console.log('IrrigationCoverageService: ' + message);
  }
}
