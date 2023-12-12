import { Component, OnInit } from '@angular/core';

import {
  iComponentBase,
  iServiceBase, mType,
  ShareData,
  iFunction
} from 'src/app/modules/shared-module/shared-module';
import { User } from 'src/app/services/auth.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import * as API from "../../../services/apiURL";
import { AccountantsOutput, MenuModerator, MenuModeratorOutput } from '../models/menuModerator';
@Component({
  selector: 'app-manage-accountant-module',
  templateUrl: './manage-accountant-module.component.html',
  styleUrls: ['./manage-accountant-module.component.scss']
})
export class ManageAccountantModuleComponent extends iComponentBase implements OnInit{
  lstUser: AccountantsOutput[] = [];
  user:User;
  displayDialogAdd: boolean = false;
  headerDialog: string = '';
  menuM:MenuModerator=new MenuModerator();
  constructor( private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
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
    this.getAllMM();

  }

  async getAllMM() {
    this.lstUser = [];

    try {


        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.LIST_AC,"");

        if (response && response.message === "Success") {
            this.lstUser = response.data;

        }
       ;
    } catch (e) {
        console.log(e);

    }
}
async BanModerator(user:AccountantsOutput,event){
  if(user.isBanned===false){
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure to Closed Account id: ${user.accountantId} ?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        //confirm action
        this.BanModeratorMM(user);
      },
      reject: () => {
        //reject action
      },
    });
  }else{
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure to Unlock id: ${user.accountantId} ?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        //confirm action
        this.BanModeratorMM(user);
      },
      reject: () => {
        //reject action
      },
    });
  }


}
async BanModeratorMM(user:AccountantsOutput){

  const param = {
    "accountantId":user.accountantId,
   "isBanned":!user.isBanned
  };
  console.log(param)
try {


       let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.BAN_AC,param);

       if (response && response.message === "Success") {
        this.getAllMM();
        this.showMessage(mType.success, "Notification", "Update"+user.accountantId+" successfully", 'notify');
       }
      ;
   } catch (e) {
       console.log(e);
       this.showMessage(mType.error, "Notification", e, 'notify');
   }
}
onCreateMM() {
  this.headerDialog = 'Add New Accountant';

  this.menuM = new MenuModerator();

  ;
  this.displayDialogAdd = true;
  this.getAllMM();
}
  async onSaveMM() {

console.log(this.menuM);

let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.ADD_AC,this.menuM);
try{
if (response && response.message === "Success") {
 this.getAllMM();
 this.showMessage(mType.success, "Notification", "Add "+this.menuM.email+" successfully", 'notify');
 this.displayDialogAdd = false;
}else{
  this.showMessage(mType.error, "Notification", response.message , 'notify');
}
;
} catch (e) {
console.log(e);
this.showMessage(mType.error, "Notification", e, 'notify');
}
}

onCancelMM() {
  this.menuM = new MenuModerator();


  this.displayDialogAdd = false;
}
UpdateModerator(user:MenuModerator){
  this.headerDialog = `Edit MenuModerator ID: ${user.modId}`;
  ;
  this.menuM = user;
  console.log(this.menuM.birthDate)
  this.menuM.birthDate = new Date(this.menuM.birthDate)
  this.displayDialogAdd = true;
}
UpdateModeratorSucess(){

}
}
