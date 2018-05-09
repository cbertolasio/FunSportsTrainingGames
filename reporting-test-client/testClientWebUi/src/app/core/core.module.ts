import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CoreRoutingModule } from './core-routing.module';

import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { LoggerModule, NgxLoggerLevel, NGXLogger } from 'ngx-logger';

@NgModule({
  imports: [
    CommonModule,
    CoreRoutingModule,
    LoggerModule.forRoot({
      serverLoggingUrl: 'api/logs',
      level: NgxLoggerLevel.DEBUG,
      serverLogLevel: NgxLoggerLevel.OFF
    })
  ],
  declarations: [
    PageNotFoundComponent
  ],
  exports: [
    RouterModule
  ]
})

export class CoreModule {
}
