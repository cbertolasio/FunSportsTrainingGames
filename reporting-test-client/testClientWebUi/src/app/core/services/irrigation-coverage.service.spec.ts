import { TestBed, inject } from '@angular/core/testing';

import { IrrigationCoverageService } from './irrigation-coverage.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpClientModule, HttpEvent, HttpEventType } from '@angular/common/http';

const mockEvents = [
  { journalId: 1 },
  { journalId: 2 }
];

describe('IrrigationCoverageService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [IrrigationCoverageService],
      imports: [
        HttpClientTestingModule
      ]
    });
  });

  it('should be created', inject([IrrigationCoverageService], (service: IrrigationCoverageService) => {
    expect(service).toBeTruthy();
  }));

  it('should get events',
    inject([HttpTestingController, IrrigationCoverageService],
      (httpMock: HttpTestingController, dataService: IrrigationCoverageService) => {
        dataService.get().subscribe(data => {
          expect(data.length).toEqual(mockEvents.length);
        });

        const req = httpMock.expectOne(dataService.irrigationEventsUri);
        expect(req.request.method).toEqual('GET');

        req.flush(mockEvents);
      }
    )
  );

  it('should get events via post',
    inject([HttpTestingController, IrrigationCoverageService],
      (httpMock: HttpTestingController, service: IrrigationCoverageService) => {
        const input = {};
        service.findEvents(input).subscribe(data => {
          expect(data.length).toEqual(mockEvents.length);
        });

        const req = httpMock.expectOne(service.findEventsUri);
        expect(req.request.method).toEqual('POST');

        req.flush(mockEvents);
      })
  );

  afterEach(inject([HttpTestingController], (httpMock: HttpTestingController) => {
    httpMock.verify();
  }));
});
