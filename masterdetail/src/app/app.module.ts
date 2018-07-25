import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { AppRoutingModule } from './/app-routing.module';
import { FarmsParentComponent } from './farms/farms-parent/farms-parent.component';
import { FarmsChildComponent } from './farms/farms-child/farms-child.component';


@NgModule({
  declarations: [
    AppComponent,
    FarmsParentComponent,
    FarmsChildComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
