import { Component, OnInit } from '@angular/core';
import { IrrigationEvent } from '../../core/services/irrigation-event';
import { IrrigationCoverageService } from '../../core/services/irrigation-coverage.service';
import { NgbDateStruct, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap/datepicker/ngb-date';
import { equal } from 'assert';
import { equalSegments } from '@angular/router/src/url_tree';
import { CoverageRange } from './coverage-range';
import { NGXLogger } from 'ngx-logger';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

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

  irrigationEvents: IrrigationEvent[];
  selectedEvent: IrrigationEvent;
  hoveredDate: NgbDateStruct;

  constructor(private coverageService: IrrigationCoverageService, private calendar: NgbCalendar,
    private logger: NGXLogger, private fb: FormBuilder) {
      this.createForm();
      this.inputForm.setValue({
        pivotId: 0,
        startDate: calendar.getToday(),
        stopDate: calendar.getNext(calendar.getToday(), 'd', 2),
        startTime: { hour: 13, minute: 30 },
        stopTime: { hour: 13, minute: 30 }
      });

    this.logger.debug('entered ctor');
  }

  createForm() {
    this.inputForm = this.fb.group({
      pivotId: ['', Validators.required],
      startDate: '',
      stopDate: '',
      startTime: '',
      stopTime: ''
    });
  }

  ngOnInit() {
  }

  onSelect(irrigationEvent: IrrigationEvent): void {
    this.selectedEvent = irrigationEvent;
    this.logger.debug('item selected: ' + JSON.stringify(irrigationEvent));
  }

  onDateSelection(date: NgbDateStruct) {
    if (!this.inputForm.value.startDate && !this.inputForm.value.stopDate) {
      this.inputForm.value.startDate = date;
    } else if (this.inputForm.value.startDate && !this.inputForm.value.stopDate && after(date, this.inputForm.value.startDate)) {
      this.inputForm.value.stopDate = date;
    } else {
      this.inputForm.value.stopDate = null;
      this.inputForm.value.startDate = date;
    }

    this.logger.debug('onDateSelection.startDate: ' + JSON.stringify(this.inputForm.value.startDate)
      + ', stopDate: ' + JSON.stringify(this.inputForm.value.stopDate));
  }

  onSubmit() {
    this.logger.debug('onSubmit:' + JSON.stringify(this.inputForm.value));
    const queryData = this.inputForm.value;
    this.coverageService.findEvents(queryData)
      .subscribe(it => this.irrigationEvents = it);
  }

  isHovered = date => this.inputForm.value.startDate && !this.inputForm.value.stopDate && this.hoveredDate
    && after(date, this.inputForm.value.startDate) && before(date, this.hoveredDate)

  isInside = date => after(date, this.inputForm.value.startDate) && before(date, this.inputForm.value.stopDate);
  isFrom = date => equals(date, this.inputForm.value.startDate);
  isTo = date => equals(date, this.inputForm.value.stopDate);
}
