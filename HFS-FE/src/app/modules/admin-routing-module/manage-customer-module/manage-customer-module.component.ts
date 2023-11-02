import { Component, OnInit } from '@angular/core';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData,
  iFunction
} from 'src/app/modules/shared-module/shared-module';
import { User } from 'src/app/services/auth.service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import * as API from "../../../services/apiURL";
import { Customer } from '../models/Customer';

@Component({
  selector: 'app-manage-customer-module',
  templateUrl: './manage-customer-module.component.html',
  styleUrls: ['./manage-customer-module.component.scss']
})
export class ManageCustomerModuleComponent extends iComponentBase implements OnInit {
  lstUser: Customer[] = [];
  user:User;

  constructor( private shareData: ShareData,
    public messageService: MessageService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private _router: Router,
  ){
    super(messageService);

  }

  ngOnInit(): void {
    //  const userData = localStorage.getItem('user');
    //    this.user = JSON.parse(userData);
    //    const token=sessionStorage.getItem('JWT');
    // this.presence.createHubConnection(token);
    this.getAllCustomer();

  }

  async getAllCustomer() {
    this.lstUser = [];

    try {

   debugger;
        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.LIST_CUS,"");

        if (response && response.message === "Success") {
            this.lstUser = response.data;

        }
       ;
    } catch (e) {
        console.log(e);

    }
}
async BanCustomer(user:Customer){
  debugger;
  const param = {
    "customerId":user.customerId,
   "Ban":!user.isBanned
  };
  console.log(param)
try {

  debugger;
       let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.BAN_CUS,param);

       if (response && response.message === "Success") {
        this.getAllCustomer();
        this.showMessage(mType.success, "Notification", "Update "+user.customerId+" successfully", 'notify');
       }
      ;
   } catch (e) {
       console.log(e);
       this.showMessage(mType.error, "Notification", "Update "+user.customerId+" failure", 'notify');
   }
}
}
