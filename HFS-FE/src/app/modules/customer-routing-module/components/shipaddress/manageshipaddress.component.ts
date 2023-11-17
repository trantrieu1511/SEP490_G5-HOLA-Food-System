import { Component, OnInit, ViewChild } from '@angular/core';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../../services/apiURL";
import {
  ConfirmationService,
  LazyLoadEvent,
  MenuItem,
  MessageService,
  SelectItem,
  TreeNode
} from "primeng/api";
import { FileSelectEvent, FileUploadEvent } from 'primeng/fileupload';
import { Router } from '@angular/router';

@Component({
  selector: 'app-manageshipaddress',
  templateUrl: './manageshipaddress.component.html',
  styleUrls: ['./manageshipaddress.component.scss']
})
export class ManageshipaddressComponent extends iComponentBase implements OnInit {
  // ----------- Use-for-binding variables ------------
  listShipAddress: any[] = [];

  // ----------- UI component variables -----------
  isVisibleAddModal: boolean = false;
  isVisibleEditModal: boolean = false;

  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    public router: Router,
    // private appCustomerTopBarComponent: AppCustomerTopBarComponent
  ) {
    super(messageService);
  }

  ngOnInit(): void {
    this.getAllShipAddress();
  }

  async getAllShipAddress() {
    try {
      

    } catch (e) {
      console.log(e);
    }
  }

}
