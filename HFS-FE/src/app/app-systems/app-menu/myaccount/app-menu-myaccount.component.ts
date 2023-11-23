import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData
} from 'src/app/modules/shared-module/shared-module';
import { AuthService } from 'src/app/services/auth.service';
import { MenuDataService } from 'src/app/services/menu-data.service';


@Component({
  selector: 'app-menu-myaccount',
  templateUrl: 'app-menu-myaccount.component.html',
  styleUrls: ['../../../profile/manageprofile.component.scss']
  // styleUrls: ['app-menu-myaccount.component.scss']
})
export class AppMenuMyaccountComponent implements OnInit {

  model: any[];
  currentRoute: string = '';

  constructor(
    private iServiceBase: iServiceBase,
    private jwtHelper: JwtHelperService,
    private menuService: MenuDataService,
    public router: Router,
    public authService: AuthService
  ) {
    this.model = [];
  }

  ngOnInit(): void {
    this.GetCurrentRoute();
  }
  
  GetCurrentRoute() { // Get current route for class sidebar-option-selected of html template
    // ;
    // console.log(window.location.href);
    const currentRoute = window.location.href.toString();
    if(currentRoute.includes('profile')){
      this.currentRoute = 'profile';
    }
    if(currentRoute.includes('orderhistory')){
      this.currentRoute = 'orderhistory';
    }
    if(currentRoute.includes('shipaddress')){
      this.currentRoute = 'shipaddress';
    }
    if(currentRoute.includes('postreport')){
      this.currentRoute = 'postreport';
    }
    if(currentRoute.includes('menureport')){
      this.currentRoute = 'menureport';
    }
    if(currentRoute.includes('wallet')){
      this.currentRoute = 'wallet';
    }
    // this.currentRoute = window.location.href.includes('profile') == true ? 'profile' : '';
    // this.currentRoute = window.location.href.includes('orderhistory') == true ? 'orderhistory' : '';
    // this.currentRoute = window.location.href.includes('shipaddress') == true ? 'shipaddress' : '';
    // this.currentRoute = window.location.href.includes('postreport') == true ? 'postreport' : '';
    // this.currentRoute = window.location.href.includes('menureport') == true ? 'menureport' : '';
    // this.currentRoute = window.location.href.includes('wallet') == true ? 'wallet' : '';
    // const searchString: string = "parent";
  }
}
