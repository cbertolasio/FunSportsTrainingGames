import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';

import { IrrigationCoverageComponent } from './irrigation-coverage.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NGXLogger, LoggerModule, NgxLoggerLevel, NGXLoggerMock, NGXLoggerHttpService, NGXLoggerHttpServiceMock } from 'ngx-logger';
import { IrrigationCoverageService } from '../../core/services/irrigation-coverage.service';
import { CoverageRange } from './coverage-range';
import { of } from 'rxjs/observable/of';
import { IrrigationEvent } from '../../core/services/irrigation-event';
import { Observable } from 'rxjs/Observable';
import { DebugElement } from '@angular/core';
import { AppComponent } from '../../app.component';
import { Http } from '@angular/http';
import { element } from 'protractor';

describe('IrrigationCoverageComponent', () => {
  let component: IrrigationCoverageComponent;
  let fixture: ComponentFixture<IrrigationCoverageComponent>;

  const testEvents = [{ journalId: 1 }, { journalId: 2 }];
  let coverageService: IrrigationCoverageService;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IrrigationCoverageComponent ],
      imports: [
        ReactiveFormsModule,
        NgbModule.forRoot(),
        LoggerModule.forRoot({
          serverLoggingUrl: 'api/logs',
          level: NgxLoggerLevel.OFF,
          serverLogLevel: NgxLoggerLevel.OFF
        })
      ],
      providers: [
        IrrigationCoverageService,
        {provide: Http, useValue: {}},
        {provide: NGXLogger, useValue: NGXLoggerMock}
      ]
    })
    .compileComponents();

  }));

  beforeEach(inject([IrrigationCoverageService], s => {
    coverageService = s;
    fixture = TestBed.createComponent(IrrigationCoverageComponent);
    component = fixture.componentInstance;
  }));

  afterEach(() => {
    coverageService = null;
    component = null;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it ('.onInit does not load data', () => {
    component.ngOnInit();

    expect(component.irrigationEvents).toBeUndefined('undefined');
  });

  it ('.onSubmit() should call coverageService.findEvents()', () => {
    const findEventsSpy = spyOn(coverageService, 'findEvents').and.returnValue(of(testEvents));

    component.onSubmit();

    expect(findEventsSpy).toHaveBeenCalled();
  });

  it ('.onSubmit() should set irrigationEvents', () => {
    const findEventsSpy = spyOn(coverageService, 'findEvents').and.returnValue(of(testEvents));

    component.onSubmit();

    expect(component.irrigationEvents.events.length).toEqual(testEvents.length);
    expect(component.irrigationEvents.events[0].journalId).toEqual(testEvents[0].journalId);
  });
});
