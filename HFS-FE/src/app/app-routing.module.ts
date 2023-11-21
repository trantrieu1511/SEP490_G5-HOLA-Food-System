import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppCustomerLayoutComponent } from './layout/customer/app.customer.component';
import { AppManageLayoutComponent } from './layout/manage/app.manage.component';
import { AppNotfoundComponent } from './pages/app.notfound.component';
import { AppErrorComponent } from './pages/app.error.component';
import { AppAccessdeniedComponent } from './pages/app.accessdenied.component';
import { authGuard } from "./services/Guard/auth.guard";
import { LoginComponent } from './login/login.component';
import { ChatComponent } from './modules/chat/chat.component';
import { RegisterComponent } from './register/register.component';
import { ConfirmemailComponent } from './confirmemail/confirmemail.component';
import { ForgotComponent } from './forgot/forgot.component';
import { LoginNonCustomerComponent } from './login-non-customer/login-non-customer.component';
import { SellerListComponent } from './seller-list/seller-list.component';
import {
  DetailNotificationComponent,
  ManageFoodComponent,
  ManageNotificationComponent,
  ManageOrderComponent,
  ManagePostComponent
} from './modules/business-routing-module/business-routing-mudule';
import { ManageprofileComponent } from './profile/manageprofile.component';
import { FooddetailComponent, 
  HomepageComponent, 
  NewFeedModuleComponent, 
  SearchComponent, 
  ShopdetailComponent 
} from './modules/customer-routing-module/customer-routing-module';

const routes: Routes = [
  // {
  //   path: 'login',
  //   redirectTo: '/login',
  //   pathMatch: 'full'
  // },
  {
    path: 'login',
    component: LoginComponent
  }
  ,
  {
    path: 'login-2',
    component: LoginNonCustomerComponent
  }
  ,
  {
    path: 'confirm',
    component: ConfirmemailComponent
  }
  ,
  {
    path: 'forgot',
    component: ForgotComponent
  }
  ,
  {
    path: 'register',
    component: RegisterComponent
  }
  ,
  {
    path: 'chat',
    component: ChatComponent
  }
  ,
  {
    path: 'listseller',
    component: SellerListComponent
  }
  ,
  // {
  //   path: 'profile',
  //   component: ManageprofileComponent
  // },

  // {
  //   path: '', component: AppCustomerLayoutComponent,
  //   children: [
  //     {
  //       path: '',
  //       loadChildren: () => import('./modules/customer-routing-module/customer-routing.module').then(m => m.CustomerRoutingModule),
  //     },
  //     {
  //       path: 'notify-management',
  //       component: ManageNotificationComponent,
  //     },
  //     {
  //       path: 'notify-management/detail/:id',
  //       component: DetailNotificationComponent,
  //     },
  //   ]
  // },
  {
    path: '',
    redirectTo: 'homepage',
    pathMatch: 'full'
  },
  {
    path: '',
    component: AppManageLayoutComponent,
    children: [
      // customer & guest role
      {path: 'homepage', component: HomepageComponent},
      {path: "shopdetail", component: ShopdetailComponent},
      {path: "fooddetail", component: FooddetailComponent},
      {path: "newfeed", component : NewFeedModuleComponent},
      {path: "search", component : SearchComponent},
      {
        path: '',
        canActivate: [authGuard],
        data: { requiredRole: ['Customer'] },
        loadChildren: () => import('./modules/customer-routing-module/customer-routing.module').then(m => m.CustomerRoutingModule),
      },
      {
        path: 'profile',
        canActivate: [authGuard],
        data: { requiredRole: ['Customer'] },
        component: ManageprofileComponent
      },
      {
        path: 'notify-management',
        canActivate: [authGuard],
        data: { requiredRole: ['Customer'] },
        component: ManageNotificationComponent,
      },
      {
        path: 'notify-management/detail/:id',
        canActivate: [authGuard],
        data: { requiredRole: ['Customer'] },
        component: DetailNotificationComponent,
      },


      // vip pro role
      {
        path: 'HFSBusiness', 
        canActivate: [authGuard],
        data: { requiredRole: ['Admin', 'Shipper', 'Seller', 'PostModerator', 'MenuModerator'] },
        children: [
          {
            path: 'profile',
            component: ManageprofileComponent
          },
          {
            path: 'post-management',
            canActivate: [authGuard],
            data: { requiredRole: ['Seller', 'PostModerator'] },
            component: ManagePostComponent
          },
          {
            path: 'notify-management',
            component: ManageNotificationComponent,
          },
          {
            path: 'notify-management/detail/:id',
            component: DetailNotificationComponent,
          },
          {
            path: 'menu-management',
            canActivate: [authGuard],
            data: { requiredRole: ['Seller', 'MenuModerator'] },
            component: ManageFoodComponent,
          },
          {
            path: 'order-management',
            canActivate: [authGuard],
            data: { requiredRole: ['Seller', 'Shipper'] },
            component: ManageOrderComponent,
          },
          {
            path: 'shipper',
            canActivate: [authGuard],
            data: { requiredRole: 'Shipper' },
            loadChildren: () => import('./modules/shipper-routing-module/shipper-routing.module').then(m => m.ShipperRoutingModule),
          },
          {
            path: 'seller',
            canActivate: [authGuard],
            data: { requiredRole: ['Seller', 'Shipper'] },
            loadChildren: () => import('./modules/seller-routing-module/seller-routing.module').then(m => m.SellerRoutingModule),
          },
          {
            path: 'admin',
            canActivate: [authGuard],
            data: { requiredRole: 'Admin' },
            loadChildren: () => import('./modules/admin-routing-module/admin-routing.module').then(m => m.AdminRoutingModule),
          },
          {
            path: 'postmoderator',
            loadChildren: () => import('./modules/postmoderator-routing-module/postmoderator-routing.module').then(m => m.PostmoderatorRoutingModule),
          },
          {
            path: 'menumoderator',
            loadChildren: () => import('./modules/menumoderator-routing-module/menumoderator-routing.module').then(m => m.MenumoderatorRoutingModule),
          }
        ]
      }
      
    ]
  },
  { path: 'error', component: AppErrorComponent },
  { path: 'accessdeny', component: AppAccessdeniedComponent },
  { path: 'notfound', component: AppNotfoundComponent },
  {path: '**', redirectTo: '/notfound'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled', useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
