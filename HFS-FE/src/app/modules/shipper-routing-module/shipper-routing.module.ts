import { NgModule } from '@angular/core';
import { CommonModule, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared-module/shared-module.module';
import { AppComponent } from 'src/app/app.component';

const routes: Routes = [
  
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedModule,
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
export class ShipperRoutingModule { }