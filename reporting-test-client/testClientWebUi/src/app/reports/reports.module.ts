import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IrrigationCoverageComponent } from './irrigation-coverage/irrigation-coverage.component';
import { ReportsRoutingModule } from './reports-routing.module';
import { RouterModule } from '@angular/router';
import { environment } from '../../environments/environment';
import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';
import { InMemoryIrrigationEventsDataService } from '../core/services/in-memory-irrigation-events-data.service';
import { IrrigationCoverageService } from '../core/services/irrigation-coverage.service';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule, NgbTimepicker } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { CoverageRange } from './irrigation-coverage/coverage-range';
import { MomentModule } from 'ngx-moment';

@NgModule({
  imports: [
    ReactiveFormsModule,
    NgbModule,
    CommonModule,
    ReportsRoutingModule,
    HttpClientModule,
    environment.production ? [] : HttpClientInMemoryWebApiModule.forRoot(
      InMemoryIrrigationEventsDataService, { dataEncapsulation: false }
    ),
    MomentModule
  ],
  declarations: [
    IrrigationCoverageComponent
  ],
  providers: [
    IrrigationCoverageService
  ],
  exports: [
    ReactiveFormsModule
  ]
})
export class ReportsModule { }
