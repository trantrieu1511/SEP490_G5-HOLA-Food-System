import { Component, OnInit } from '@angular/core';
import {
    iComponentBase,
    iServiceBase, mType,
    ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../services/apiURL";
import { MenuDataService } from 'src/app/services/menu-data.service';
import { RoleNames } from '../../utils/roleName';
import { AuthService } from 'src/app/services/auth.service';


@Component({
  selector: 'app-menu',
  template: `
    <ul class="layout-menu">
      <li app-menuitem *ngFor="let item of model; let i = index;" [item]="item" [index]="i"
          [root]="true"></li>
    </ul>
  `,
})
export class AppMenuComponent implements OnInit {

  model: any[];

  constructor(
    private menuService: MenuDataService,
    private authService: AuthService
  ) {
    this.model = [];
  }

  ngOnInit(): void {
    // check role here
    // switch case and put data for model
    // each role has own model :
    // model được hiểu là menu bên trái nha


    //this.getRole();
    //var role = sessionStorage.getItem("role");
    var role = this.authService.getUserInfor().role;
    switch (RoleNames[role]) {
        case null:
            
            break;
        case "Seller":
            this.model = this.menuService.menusSeller();
            break;
        case "Admin":
            this.model = this.menuService.menusAdmin();
            break;
        case "Shipper":
            this.model = this.menuService.menusShipper();
            break;
        case "PostModerator":
            this.model = this.menuService.menusPostModerator();
            break;
        case "MenuModerator":
            this.model = this.menuService.menusMenuModerator();
            break;
        case "Accountant":
            this.model = this.menuService.menuAccountant();
            break;    
        default:
            this.model = [];
            break;
    }
  }

  // async getRole(){


  //   var param = {
  //       email: "string@gmail.com",
  //       password: "123"
  //   }

  //   var res = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.TEST, API.API_TEST.SIGNIN, param);
  //   console.log("res", res);
  //   sessionStorage.setItem("token", res.token);

  //   var token = await this.jwtHelper.tokenGetter();
  //   const decodedToken = this.jwtHelper.decodeToken(token);
  //   console.log("token", decodedToken);

  //   const name = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
  //   const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

  //   console.log('Name:', name);
  //   console.log('Role:', role);
  // }

}
