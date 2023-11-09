import { NgModule } from '@angular/core';
import { CommonModule, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { SharedModule } from '../shared-module/shared-module.module';
import { ComponentModule } from '../components-module/component.modules';
import { FoodManagementComponent } from './components/food-management/food-management.component';
import { FoodreportManagementComponent } from './components/foodreport-management/foodreport-management.component';

const routes: Routes = [
  {path: 'food-management', component: FoodManagementComponent},
  {path: 'foodreport-management', component: FoodreportManagementComponent}
]

@NgModule({
  declarations: [
    FoodManagementComponent,
    FoodreportManagementComponent
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
    {provide: LocationStrategy, useClass: HashLocationStrategy}
  ],
  bootstrap: [AppComponent]
})
export class MenumoderatorRoutingModule { }
