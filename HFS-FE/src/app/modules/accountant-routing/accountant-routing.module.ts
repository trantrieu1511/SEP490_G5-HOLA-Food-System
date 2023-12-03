import { NgModule } from '@angular/core';
import { CommonModule, HashLocationStrategy, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { SharedModule } from '../shared-module/shared-module.module';
import { ComponentModule } from '../components-module/component.modules';
import { WithdrawRequestComponent } from './components/withdraw-request/withdraw-request.component';

const routes: Routes = [
  {path: 'withdraw-request', component: WithdrawRequestComponent},
]

@NgModule({
  declarations: [
    WithdrawRequestComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    ComponentModule,
    RouterModule.forChild(routes),
  ],
  exports: [
  ],
  providers: [
    {provide: LocationStrategy, useClass: PathLocationStrategy}
  ],
  bootstrap: [AppComponent]
})
export class AccountantRoutingModule { }