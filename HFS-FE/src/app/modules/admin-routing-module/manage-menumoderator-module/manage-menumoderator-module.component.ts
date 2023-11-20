import { Component, OnInit } from '@angular/core';
import { MenuModerator, MenuModeratorOutput } from '../models/menuModerator';
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
@Component({
  selector: 'app-manage-menumoderator-module',
  templateUrl: './manage-menumoderator-module.component.html',
  styleUrls: ['./manage-menumoderator-module.component.scss']
})
export class ManageMenumoderatorModuleComponent extends iComponentBase implements OnInit {
  lstUser: MenuModeratorOutput[] = [];
  user:User;
  displayDialogAdd: boolean = false;
  headerDialog: string = '';
  menuM:MenuModerator=new MenuModerator();
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
        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.LIST_MM,"");

        if (response && response.message === "Success") {
            this.lstUser = response.data;

        }
       ;
    } catch (e) {
        console.log(e);

    }
}
async BanModerator(user:MenuModeratorOutput){
  debugger;
  const param = {
    "modId":user.modId,
   "isBanned":!user.isBanned
  };
  console.log(param)
try {

  debugger;
       let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.BAN_MM,param);

       if (response && response.message === "Success") {
        this.getAllCustomer();
        this.showMessage(mType.success, "Notification", "Ban "+user.modId+" successfully", 'notify');
       }
      ;
   } catch (e) {
       console.log(e);
       this.showMessage(mType.error, "Notification", e, 'notify');
   }
}
onCreatePostM() {
  this.headerDialog = 'Add New PostModerator';

  this.menuM = new MenuModerator();


  this.displayDialogAdd = true;
}
  async onSavePostM() {

console.log(this.menuM);

let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.ADD_MM,this.menuM);
try{
if (response && response.message === "Success") {
 this.getAllCustomer();
 this.showMessage(mType.success, "Notification", "Add "+this.menuM.email+" successfully", 'notify');
}else{
  this.showMessage(mType.error, "Notification", response.message , 'notify');
}
;
} catch (e) {
console.log(e);
this.showMessage(mType.error, "Notification", e, 'notify');
}
}

onCancelPostM() {
  this.menuM = new MenuModerator();


  this.displayDialogAdd = false;
}
UpdateModerator(user:MenuModerator){
  this.headerDialog = `Edit MenuModerator ID: ${user.modId}`;
  this.menuM = user;
  console.log(this.menuM.birthDate)
  this.menuM.birthDate = new Date(this.menuM.birthDate)

  this.displayDialogAdd = true;
}
UpdateModeratorSucess(){

}
}