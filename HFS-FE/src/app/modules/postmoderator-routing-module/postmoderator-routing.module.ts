import { NgModule } from '@angular/core';
import { CommonModule, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { SharedModule } from '../shared-module/shared-module.module';
import { ComponentModule } from '../components-module/component.modules';
import { DisplayPostComponent } from './components/display-post/display-post.component';
import { PostreportManagementComponent } from './components/postreport-management/postreport-management.component';

const routes: Routes = [
  {path: 'post-management', component: DisplayPostComponent},
  {path: 'postreport-management', component: PostreportManagementComponent},
]

@NgModule({
  declarations: [
    DisplayPostComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ComponentModule,
    RouterModule.forChild(routes),
  ],
  exports: [
    DisplayPostComponent
  ],
  providers: [
    {provide: LocationStrategy, useClass: HashLocationStrategy}
  ],
  bootstrap: [AppComponent]
})
export class PostmoderatorRoutingModule { }
