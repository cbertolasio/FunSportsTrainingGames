import { Component, OnInit } from '@angular/core';
import { IrrigationEvent } from '../../core/services/irrigation-event';
import { IrrigationCoverageService } from '../../core/services/irrigation-coverage.service';
import { NgbDateStruct, NgbCalendar, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap/datepicker/ngb-date';
import { equal } from 'assert';
import { equalSegments } from '@angular/router/src/url_tree';
import { CoverageRange } from './coverage-range';
import { NGXLogger } from 'ngx-logger';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { IrrigationEventResponse } from '../../core/services/irrigation-events-response';
import { IrrigationEventSummary } from '../../core/services/irrigation-event-summary';

const equals = (one: NgbDateStruct, two: NgbDateStruct) =>
  one && two && two.year === one.year && two.month === one.month && two.day === one.day;

const before = (one: NgbDateStruct, two: NgbDateStruct) =>
  !one || !two ? false : one.year === two.year ? one.month === two.month ? one.day === two.day ? false :
    one.day < two.day : one.month < two.month : one.year < two.year;

const after = (one: NgbDateStruct, two: NgbDateStruct) =>
  !one || !two ? false : one.year === two.year ? one.month === two.month ? one.day === two.day
    ? false : one.day > two.day : one.month > two.month : one.year > two.year;

@Component({
  selector: 'app-irrigation-coverage',
  templateUrl: './irrigation-coverage.component.html',
  styleUrls: ['./irrigation-coverage.component.scss'],
  providers: [NGXLogger]
})
export class IrrigationCoverageComponent implements OnInit {
  inputForm: FormGroup;
  irrigationEvents: IrrigationEventResponse;
  selectedEvent: IrrigationEvent;
  selectedSummaryItem: IrrigationEventSummary;
  hoveredDate: NgbDateStruct;
  currentPage: number;
  collectionSize: number;
  pageSize: number;
  startDate: NgbDateStruct;
  stopDate: NgbDateStruct;
  loading: boolean;
  hasData: boolean;

  constructor(private coverageService: IrrigationCoverageService, private calendar: NgbCalendar,
    private logger: NGXLogger, private fb: FormBuilder, private ngbDateParser: NgbDateParserFormatter) {
      this.startDate = calendar.getToday();
      this.stopDate = calendar.getNext(calendar.getToday(), 'd', 2);

      this.createForm();
      this.inputForm.setValue({
        pivotId: 0,
        startTime: { hour: 13, minute: 30 },
        stopTime: { hour: 13, minute: 30 },
        startBearing: 1,
        stopBearing: 360
      });

    this.logger.debug('entered ctor');
    this.currentPage = 1;
    this.pageSize = 10;
    this.loading = false;
    this.hasData = false;
  }

  createForm() {
    this.inputForm = this.fb.group({
      pivotId: ['', Validators.required],
      startTime: '',
      stopTime: '',
      startBearing: [1, [ Validators.required, Validators.min(0), Validators.max(361)]],
      stopBearing: [360, [ Validators.required, Validators.min(0), Validators.max(361)]]
    });
  }

  ngOnInit() {
    this.irrigationEvents = new IrrigationEventResponse;
  }

  onSelect(irrigationEvent: IrrigationEvent): void {
    this.selectedEvent = irrigationEvent;
    this.logger.debug('item selected: ' + JSON.stringify(irrigationEvent));
  }

  onSelecteSummaryItem(summaryItem: IrrigationEventSummary): void {
    this.selectedSummaryItem = summaryItem;
    this.logger.debug('summary item selected: ' + JSON.stringify(summaryItem));
  }

  onDateSelection(date: NgbDateStruct) {
    if (!this.startDate && !this.stopDate) {
      this.startDate = date;
    } else if (this.startDate && !this.stopDate && after(date, this.startDate)) {
      this.stopDate = date;
    } else {
      this.stopDate = null;
      this.startDate = date;
    }

    this.logger.debug('onDateSelection.startDate: ' + JSON.stringify(this.startDate)
      + ', stopDate: ' + JSON.stringify(this.stopDate));
  }

  onSubmit() {
    this.loading = true;
    this.logger.debug('onSubmit:' + JSON.stringify(this.inputForm.value));
    const queryData = this.inputForm.value;
    queryData.startDate = this.ngbDateParser.format(this.startDate);
    queryData.stopDate = this.ngbDateParser.format(this.stopDate);

    this.coverageService.findEvents(queryData)
      .subscribe(it => {
        this.irrigationEvents = it;
        this.collectionSize = this.irrigationEvents.events.length;

        this.logger.debug('component.startDate: ' + JSON.stringify(this.startDate)
        + ', component.stopDate: ' + JSON.stringify(this.stopDate));
        this.loading = false;
        this.hasData = (this.collectionSize > 0);
      }, () => {
        this.loading = false;
        this.hasData = false;
      });

    this.currentPage = 1;
  }

  pageChanged(pageNumber: number) {
    this.logger.debug('current page ' + pageNumber);
  }

  isHovered = date => this.startDate && !this.stopDate && this.hoveredDate
    && after(date, this.startDate) && before(date, this.hoveredDate)

  isInside = date => after(date, this.startDate) && before(date, this.stopDate);
  isFrom = date => equals(date, this.startDate);
  isTo = date => equals(date, this.stopDate);
}
