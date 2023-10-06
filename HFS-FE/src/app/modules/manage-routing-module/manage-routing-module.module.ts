import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import { DashboardComponent } from './components/test-component/dashboard.component';
import {HashLocationStrategy, LocationStrategy} from '@angular/common';
import {AppComponent} from '../../app.component';
import { SharedModule } from '../shared-module/shared-module.module';
import { ComponentModule } from '../components-module/component.modules';
import { QuanTriPhanQuyenComponent } from './components/test1-component/quan-tri-phan-quyen.component';


const routes: Routes = [
  {path: '', component: DashboardComponent},
  {path: 'users', component: QuanTriPhanQuyenComponent},
]

@NgModule({
  declarations: [
    DashboardComponent,
    QuanTriPhanQuyenComponent
  ],
  imports: [
    ComponentModule,
    SharedModule,
    CommonModule,
    RouterModule.forChild(routes),
  ],
  exports: [

  ],
  providers: [
      {provide: LocationStrategy, useClass: HashLocationStrategy}
  ],
  entryComponents: [],
  bootstrap: [AppComponent]
})

export class ManageRoutingModuleModule { }
