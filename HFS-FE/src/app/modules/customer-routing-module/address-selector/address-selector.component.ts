import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
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
import { DropdownChangeEvent } from 'primeng/dropdown';
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
  addressDetail: string = '';
  provinceSelected: any;
  districtSelected: any;
  wardSelected: any;

  @Output() provinceOutput = new EventEmitter<string>();
  @Output() districtOutput = new EventEmitter<string>();
  @Output() wardOutput = new EventEmitter<string>();
  @Output() addressOutput = new EventEmitter<string>();

  timeOutInput: any = null;

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
    
    const param={
      "url" :api
    }
    if(target!=='province'){
      const response = await  this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, target,param);
      //console.log(response);
      this.renderData(response, target);
    }else{
      const response = await  this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, target,"");
      //console.log(response);
      this.renderData(response, target);
    }



  }

  renderData(data: any, target: string) {
    //console.log(data)
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

  onProvinceChange(event: any) {

    //const selectedProvinceCode = (document.getElementById('province') as HTMLSelectElement).value;
    
    const selectedProvinceCode = this.provinceSelected.code
    
    

    this.provinceOutput.emit(this.provinceSelected.name);
    
    this.callAPI('p/' + selectedProvinceCode + '?depth=2', 'district');
    this.printResult();
  }

  onDistrictChange(event: any) {
    //const selectedDistrictCode = (document.getElementById('district') as HTMLSelectElement).value;
    
    const selectedDistrictCode = this.districtSelected.code
    
    this.districtOutput.emit(this.districtSelected.name);

    this.callAPI('d/' + selectedDistrictCode + '?depth=2', 'ward');
    this.printResult();
  }

  onWardChange(event: any) {

    this.wardOutput.emit(this.wardSelected.name);
    this.printResult();
  }

  printResult() {
    // const selectedProvinceText = (document.getElementById('province') as HTMLSelectElement).options[
    //   (document.getElementById('province') as HTMLSelectElement).selectedIndex
    // ].text;

    // const selectedDistrictText = (document.getElementById('district') as HTMLSelectElement).options[
    //   (document.getElementById('district') as HTMLSelectElement).selectedIndex
    // ].text;

    // const selectedWardText = (document.getElementById('ward') as HTMLSelectElement).options[
    //   (document.getElementById('ward') as HTMLSelectElement).selectedIndex
    // ].text;

    if (this.districtSelected && this.provinceSelected && this.wardSelected ) {
      this.selectedResult = `${this.provinceSelected.name} | ${this.districtSelected.name} | ${this.wardSelected.name}`;
    }
  }

  onAddressDetailChange(event: any){
    clearTimeout(this.timeOutInput);

    this.timeOutInput = setTimeout(() => {
     
      this.addressOutput.emit(event.target.value)
     
    }, 1000);
  }
}
