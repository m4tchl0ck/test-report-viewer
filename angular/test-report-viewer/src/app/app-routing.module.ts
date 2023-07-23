import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TestExecutionsListComponent } from './test-executions-list/test-executions-list.component';

const routes: Routes = [
  { path: '', component: TestExecutionsListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
