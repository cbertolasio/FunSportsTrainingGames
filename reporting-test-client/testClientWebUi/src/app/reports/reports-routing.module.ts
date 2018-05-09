import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IrrigationCoverageComponent } from './irrigation-coverage/irrigation-coverage.component';

const routes: Routes = [
  { path: 'irrigationcoverage', component: IrrigationCoverageComponent}
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ReportsRoutingModule { }
