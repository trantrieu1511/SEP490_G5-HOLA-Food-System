import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../services/apiURL";
import {
  ConfirmationService,
  LazyLoadEvent,
  MenuItem,
  MessageService,
  SelectItem,
  TreeNode
} from "primeng/api";
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-address-selector',
  templateUrl: './address-selector.component.html',
  styleUrls: ['./address-selector.component.scss']
})
export class AddressSelectorComponent extends iComponentBase implements OnInit {
  host = 'https://provinces.open-api.vn/api/';
  provinces: any[] = [];
  districts: any[] = [];
  wards: any[] = [];
  selectedResult: string = '';

  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    public router: Router,
    public authService: AuthService
    // private appCustomerTopBarComponent: AppCustomerTopBarComponent
  ) {
    super(messageService);
  }

  ngOnInit() {
    this.callAPI("", 'province');
  }

  async callAPI(api: string, target: string) {
    debugger
    const param={
      "url" :api
    }
    if(target!=='province'){
      const response = await  this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, target,param);
     console.log(response);
        this.renderData(response, target);
    }else{
      const response = await  this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, target,"");
      console.log(response);
         this.renderData(response, target);
 }



  }

  renderData(data: any, target: string) {
    switch (target) {
      case 'province':
        this.provinces = data;
        break;
      case 'district':
        this.districts = data['districts'];
        break;
      case 'ward':
        this.wards = data['wards'];
        break;
      default:
        break;
    }
  }

  onProvinceChange() {
debugger;
    const selectedProvinceCode = (document.getElementById('province') as HTMLSelectElement).value;

    this.callAPI('p/' + selectedProvinceCode + '?depth=2', 'district');
    this.printResult();
  }

  onDistrictChange() {
    const selectedDistrictCode = (document.getElementById('district') as HTMLSelectElement).value;
    this.callAPI('d/' + selectedDistrictCode + '?depth=2', 'ward');
    this.printResult();
  }

  onWardChange() {
    this.printResult();
  }

  printResult() {
    const selectedProvinceText = (document.getElementById('province') as HTMLSelectElement).options[
      (document.getElementById('province') as HTMLSelectElement).selectedIndex
    ].text;

    const selectedDistrictText = (document.getElementById('district') as HTMLSelectElement).options[
      (document.getElementById('district') as HTMLSelectElement).selectedIndex
    ].text;

    const selectedWardText = (document.getElementById('ward') as HTMLSelectElement).options[
      (document.getElementById('ward') as HTMLSelectElement).selectedIndex
    ].text;

    if (selectedDistrictText !== '' && selectedProvinceText !== '' && selectedWardText !== '') {
      this.selectedResult = `${selectedProvinceText} | ${selectedDistrictText} | ${selectedWardText}`;
    }
  }
}
