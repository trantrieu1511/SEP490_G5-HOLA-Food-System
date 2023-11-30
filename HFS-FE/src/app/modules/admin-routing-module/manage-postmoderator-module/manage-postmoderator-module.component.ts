import { Component, OnInit } from '@angular/core';
import { Customer } from '../models/Customer';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData,
  iFunction
} from 'src/app/modules/shared-module/shared-module';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import * as API from "../../../services/apiURL";
import { User } from 'src/app/services/auth.service';
import { PostModerator, PostModeratorOutput } from '../models/PostModerator';

@Component({
  selector: 'app-manage-postmoderator-module',
  templateUrl: './manage-postmoderator-module.component.html',
  styleUrls: ['./manage-postmoderator-module.component.scss']
})
export class ManagePostmoderatorModuleComponent extends iComponentBase implements OnInit {
  lstUser: PostModeratorOutput[] = [];
  user:User;
  displayDialogAdd: boolean = false;
  headerDialog: string = '';
  postM:PostModerator=new PostModerator();
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
    this.getAllCustomer();

  }

  async getAllCustomer() {
    this.lstUser = [];

    try {


        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.LIST_PM,"");

        if (response && response.message === "Success") {
            this.lstUser = response.data;

        }
       ;
    } catch (e) {
        console.log(e);

    }
}
async BanModerator(user:PostModeratorOutput,event){
  if(user.isBanned===false){
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure to Closed Account id: ${user.modId} ?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        //confirm action
        this.BanModeratorPM(user);
      },
      reject: () => {
        //reject action
      },
    });
  }else{
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure to Unlock id: ${user.modId} ?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        //confirm action
        this.BanModeratorPM(user);
      },
      reject: () => {
        //reject action
      },
    });
  }


}
async BanModeratorPM(user:PostModeratorOutput){

  const param = {
    "modId":user.modId,
   "isBanned":!user.isBanned
  };
  console.log(param)
try {


       let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.BAN_PM,param);

       if (response && response.message === "Success") {
        this.getAllCustomer();
        this.showMessage(mType.success, "Notification", "Update "+user.modId+" successfully", 'notify');
       }
      ;
   } catch (e) {
       console.log(e);
       this.showMessage(mType.error, "Notification", "Update "+user.modId+" failure", 'notify');
   }
}
onCreatePostM() {
  this.headerDialog = 'Add New PostModerator';

  this.postM = new PostModerator();


  this.displayDialogAdd = true;
}
  async onSavePostM() {

console.log(this.postM);
console.log(this.postM);
let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.ADD_PM,this.postM);
try{
if (response && response.message === "Success") {
 this.getAllCustomer();
 this.showMessage(mType.success, "Notification", "Add "+this.postM.email+" successfully", 'notify');
 this.displayDialogAdd = false;
 this.postM = new PostModerator();

}else{
  this.showMessage(mType.error, "Notification", response.message, 'notify');
}
;
} catch (e) {
console.log(e);
this.showMessage(mType.error, "Notification", "Add "+this.postM.email+" failure", 'notify');
}
}

onCancelPostM() {
  this.postM = new PostModerator();


  this.displayDialogAdd = false;
}

}
