import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FarmsParentComponent } from './farms/farms-parent/farms-parent.component';

const routes: Routes = [
  { path: 'farms', component: FarmsParentComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
