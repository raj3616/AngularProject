import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddskillComponent } from './addskill/addskill.component';

const routes: Routes = [
  {
    path:"addskill",
    component:AddskillComponent
  },
  {
    component:sh
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
