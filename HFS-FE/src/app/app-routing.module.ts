import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppCustomerLayoutComponent } from './layout/customer/app.customer.component';
import { AppManageLayoutComponent } from './layout/manage/app.manage.component';

const routes: Routes = [
  // {
  //   path: 'login',
  //   redirectTo: '/login',
  //   pathMatch: 'full'
  // },
  // {
  //     path: 'login',
  //     component: LoginComponent
  // }
  // ,
  {
    path: '', component: AppCustomerLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./modules/customer-routing-module/customer-routing-module.module').then(m => m.CustomerRoutingModuleModule),
      }
    ]
  },
  {
    path: 'manage', component: AppManageLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./modules/manage-routing-module/manage-routing-module.module').then(m => m.ManageRoutingModuleModule),
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {scrollPositionRestoration: 'enabled', useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
