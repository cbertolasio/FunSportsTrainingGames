import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { IrrigationEvent } from './irrigation-event';
import { catchError, map, tap } from 'rxjs/operators';
import { RequestOptions } from '@angular/http';

const options = {headers: new HttpHeaders({'Content-Type': 'application/json' })};

@Injectable()
export class IrrigationCoverageService {
  findEventsUri = 'api/findEvents';
  irrigationEventsUri = 'api/irrigationEvents';

  findEvents(queryData: any): Observable<IrrigationEvent[]> {
    const data = {};

    return this.http.post<IrrigationEvent[]>(this.findEventsUri, data, options)
      .pipe(
        tap(irrigationEvents => this.log(`fetched irrigation events via post`)),
        catchError(this.handleError('findEvents', []))
      );
  }

  get(): Observable<IrrigationEvent[]> {
    return this.http.get<IrrigationEvent[]>(this.irrigationEventsUri)
      .pipe(
        tap(irrigationEvents => this.log(`fetched irrigationEvents`)),
        catchError(this.handleError('getIrrigationEvents', []))
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
