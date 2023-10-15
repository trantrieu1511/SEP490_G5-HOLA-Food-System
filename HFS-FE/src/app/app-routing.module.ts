import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppCustomerLayoutComponent } from './layout/customer/app.customer.component';
import { AppManageLayoutComponent } from './layout/manage/app.manage.component';
import {AppNotfoundComponent} from './pages/app.notfound.component';
import {AppErrorComponent} from './pages/app.error.component';
import {AppAccessdeniedComponent} from './pages/app.accessdenied.component';
import {authGuard} from "./services/Guard/auth.guard";

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
        loadChildren: () => import('./modules/customer-routing-module/customer-routing.module').then(m => m.CustomerRoutingModule),
      }
    ]
  },
  {
    path: 'HFSBusiness', component: AppManageLayoutComponent,
    children: [
      {
        path: 'manage',
        loadChildren: () => import('./modules/manage-routing-module/manage-routing-module.module').then(m => m.ManageRoutingModuleModule),
      },
      {
        path: 'shipper',
        loadChildren: () => import('./modules/shipper-routing-module/shipper-routing.module').then(m => m.ShipperRoutingModule),
      },
      {
        path: 'seller',
        // canActivate: [authGuard],
        // data: { requiredRole: 'Seller' },
        loadChildren: () => import('./modules/seller-routing-module/seller-routing.module').then(m => m.SellerRoutingModule),
      },
      {
        path: 'admin',
        loadChildren: () => import('./modules/admin-routing-module/admin-routing.module').then(m => m.AdminRoutingModule),
      }
    ]
  },
  {path: 'error', component: AppErrorComponent},
  {path: 'accessdeny', component: AppAccessdeniedComponent},
  {path: 'notfound', component: AppNotfoundComponent},
  {path: '**', redirectTo: '/notfound'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {scrollPositionRestoration: 'enabled', useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
