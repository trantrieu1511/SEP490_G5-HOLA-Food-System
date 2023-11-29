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
import { firstValueFrom } from 'rxjs';
import { FileSelectEvent, FileUploadEvent } from 'primeng/fileupload';
import { Router } from '@angular/router';
import { CreateNewShipAddressInputValidation, ShipAddress, UpdateShipAddressInputValidation } from '../../models/ShipAddress.modal';
import { AuthService } from 'src/app/services/auth.service';
import { HttpClient } from '@angular/common/http';

class ProvinceDistrictWard {
  name: string = null;
  code: string = null;
}

@Component({
  selector: 'app-manageshipaddress',
  templateUrl: './manageshipaddress.component.html',
  styleUrls: ['./manageshipaddress.component.scss']
})
export class ManageshipaddressComponent extends iComponentBase implements OnInit {
  // ----------- Use-for-binding variables ------------
  listShipAddress: ShipAddress[] = [];
  provinces: ProvinceDistrictWard[] = [];
  districts: ProvinceDistrictWard[] = [];
  wards: ProvinceDistrictWard[] = [];
  selectedProvince: ProvinceDistrictWard = new ProvinceDistrictWard();
  selectedDistrict: ProvinceDistrictWard = new ProvinceDistrictWard();
  selectedWard: ProvinceDistrictWard = new ProvinceDistrictWard();
  shipAddress: ShipAddress = new ShipAddress();
  createNewShipAddressValidation: CreateNewShipAddressInputValidation = new CreateNewShipAddressInputValidation();
  updateShipAddressValidation: UpdateShipAddressInputValidation = new UpdateShipAddressInputValidation();

  // ----------- UI component variables -----------
  isVisibleCreateModal: boolean = false;
  isVisibleUpdateModal: boolean = false;
  isVisibleDeleteModal: boolean = false;
  isVisibleSetDefaultAddressModal: boolean = false;
  loading: boolean;
  // host = "https://provinces.open-api.vn/api/";

  constructor(
    public httpClient: HttpClient,
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

  async ngOnInit(): Promise<void> {
    await this.getAllShipAddress();
    this.callAPI("", 'province');
    // await this.getCities();
  }

  async callAPI(api: string, target: string) {
    // debugger;
    const param = {
      "url": api
    }
    if (target !== 'province') {
      const response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, target, param);
      if (target === 'district') {
        this.districts = response.districts;
      }
      if (target === 'ward') {
        this.wards = response.wards;
      }

      console.log(response);
      // this.renderData(response, target);
    } else {
      const response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, target, "");
      this.provinces = response;
      console.log(this.provinces);
      //  this.renderData(response, target);
    }
  }

  onProvinceChange(fromCreateModal: boolean) {
    // debugger;
    // const selectedProvinceCode = (document.getElementById('province') as HTMLSelectElement).value;
    // if (this.selectedProvince != null || this.selectedProvince) {

    // this.validateCreateNewShipAddress();
    this.districts = [];
    this.selectedDistrict = null;
    this.wards = [];
    this.selectedWard = null;
    if (fromCreateModal) {
      this.validateCreateNewShipAddress();
    } else {
      this.validateUpdateNewShipAddress();
    }
    console.log(this.selectedProvince);
    this.callAPI('p/' + this.selectedProvince.code + '?depth=2', 'district');
    // this.printResult();
    // }
  }

  onDistrictChange(fromCreateModal: boolean) {
    // const selectedDistrictCode = (document.getElementById('district') as HTMLSelectElement).value;
    // if (this.selectedDistrict != null || this.selectedDistrict) {

    // this.validateCreateNewShipAddress();
    this.wards = [];
    this.selectedWard = null;
    if (fromCreateModal) {
      this.validateCreateNewShipAddress();
    } else {
      this.validateUpdateNewShipAddress();
    }
    console.log(this.selectedDistrict);
    this.callAPI('d/' + this.selectedDistrict.code + '?depth=2', 'ward');
    // this.printResult();
    // }
  }

  onWardChange(fromCreateModal: boolean) {
    if (fromCreateModal) {
      this.validateCreateNewShipAddress();
    } else {
      this.validateUpdateNewShipAddress();
    }
    // this.printResult();
  }


  // async getCities() {
  //   try {
  //     const response = await this.iServiceBase.getAllProvincesAsync();

  //     // console.log(response);
  //     if (response) {
  //       this.listCities = response.data;
  //       console.log(this.listCities);
  //     }
  //     this.loading = false;
  //   } catch (e) {
  //     console.log(e);
  //     this.loading = false;
  //   }
  // }
  // getOptionsRequest(ignoreLoading?: boolean, responseType?: string) {
  //   const options: any = {};
  //   if (ignoreLoading != undefined && ignoreLoading) {
  //     // this.loadingService.setIgnoreLoading();
  //     options.reportProgress = true;
  //   }
  //   if (responseType != undefined && responseType) {
  //     // this.loadingService.setIgnoreLoading();
  //     options.responseType = responseType;
  //   }
  //   return options;
  // }

  async getAllShipAddress() {
    this.listShipAddress = [];
    try {
      this.loading = true;

      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.CUSTOMER, API.API_SHIPADDRESS.GETALLSHIPADDRESS);

      if (response && response.message === "Success") {
        this.listShipAddress = response.shipAddresses;
        console.log(this.listShipAddress);
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  openCreateDialog() {
    this.shipAddress = new ShipAddress();

    this.createNewShipAddressValidation = new CreateNewShipAddressInputValidation();

    // this.provinces = [];
    this.callAPI("", 'province');
    this.districts = [];
    this.wards = [];
    // this.selectedProvince = new ProvinceDistrictWard();
    // this.selectedDistrict = new ProvinceDistrictWard();
    // this.selectedWard = new ProvinceDistrictWard();
    this.selectedProvince = null;
    this.selectedDistrict = null;
    this.selectedWard = null;

    this.isVisibleCreateModal = true;
  }

  validateCreateNewShipAddress() {
    // debugger;
    var check = true;
    this.createNewShipAddressValidation = new CreateNewShipAddressInputValidation();
    if (!this.shipAddress.detailAddressInfo || this.shipAddress.detailAddressInfo == '') {
      this.createNewShipAddressValidation.isValidDetailAddressInfo = false;
      this.createNewShipAddressValidation.detailAddressInfoValidationMessage = "Detail address information can not empty";
      check = false;
    } else {
      if (this.shipAddress.detailAddressInfo.length < 5) {
        this.createNewShipAddressValidation.isValidDetailAddressInfo = false;
        this.createNewShipAddressValidation.detailAddressInfoValidationMessage = "Detail address can not be too short. Min length: 5";
        check = false;
      }
    }

    if (!this.selectedProvince || this.selectedProvince == null) {
      this.createNewShipAddressValidation.isValidProvince = false;
      this.createNewShipAddressValidation.provinceValidationMessage = "Provice information can not empty";
      check = false;
    }
    if (!this.selectedDistrict || this.selectedDistrict == null) {
      this.createNewShipAddressValidation.isValidDistrict = false;
      this.createNewShipAddressValidation.districtValidationMessage = "District information can not empty";
      check = false;
    }
    if (!this.selectedWard || this.selectedWard == null) {
      this.createNewShipAddressValidation.isValidWard = false;
      this.createNewShipAddressValidation.wardValidationMessage = "Ward information can not empty";
      check = false;
    }

    console.log(this.createNewShipAddressValidation);
    return check;
  }

  async createNewShipAddress() {
    if (this.validateCreateNewShipAddress()) {
      try {
        this.loading = true;

        let param = {
          addressInfo: this.shipAddress.detailAddressInfo + '. ' + this.selectedWard.name + ', ' + this.selectedDistrict.name + ', ' + this.selectedProvince.name
        }

        let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.CUSTOMER, API.API_SHIPADDRESS.CREATENEWSHIPADDRESS, param);

        if (response && response.message === "Success") {
          this.showMessage(mType.success, 'Notification', 'Create new ship address successfully', 'notify');
          console.log('Create new ship address successfully.');
        } else {
          this.showMessage(mType.error, 'Notification', 'Create new ship address failed', 'notify');
          console.log('Create new ship address failed.');
        }

        // Clear model
        this.shipAddress = new ShipAddress();

        // Close modal
        this.isVisibleCreateModal = false;

        // Reload all data
        this.getAllShipAddress();

        this.loading = false;
      } catch (e) {
        this.showMessage(mType.error, 'Notification', "There's error happened in the server. Please contact admin for help.", 'notify');
        console.log(e);

        // Clear model
        this.shipAddress = new ShipAddress();

        // Close modal
        this.isVisibleCreateModal = false;

        // Reload all data
        this.getAllShipAddress();

        this.loading = false;
      }
    }
  }

  async openUpdateDialog(shipaddress: ShipAddress) {
    this.shipAddress = new ShipAddress();

    this.updateShipAddressValidation = new UpdateShipAddressInputValidation();

    // this.provinces = [];
    // this.districts = [];
    // this.wards = [];
    // this.selectedProvince = new ProvinceDistrictWard();
    // this.selectedDistrict = new ProvinceDistrictWard();
    // this.selectedWard = new ProvinceDistrictWard();

    this.shipAddress = Object.assign({}, shipaddress);
    this.shipAddress.detailAddressInfo = this.shipAddress.addressInfo.split('. ')[0];

    // debugger
    // get province
    await this.callAPI("", 'province');
    this.provinces.forEach(element => {
      if (element.name == this.shipAddress.addressInfo.split('. ')[1].split(', ')[2]) {
        this.selectedProvince = element;
      }
    });
    // this.selectedProvince.name = this.shipAddress.addressInfo.split('. ')[0];

    // get district
    await this.callAPI('p/' + this.selectedProvince.code + '?depth=2', 'district');
    this.districts.forEach(element => {
      if (element.name == this.shipAddress.addressInfo.split('. ')[1].split(', ')[1]) {
        this.selectedDistrict = element;
      }
    });
    // this.selectedDistrict.name = this.shipAddress.addressInfo.split('. ')[1];

    // get ward
    await this.callAPI('d/' + this.selectedDistrict.code + '?depth=2', 'ward');
    this.wards.forEach(element => {
      if (element.name == this.shipAddress.addressInfo.split('. ')[1].split(', ')[0]) {
        this.selectedWard = element;
      }
    });
    // this.selectedWard.name = this.shipAddress.addressInfo.split('. ')[2];

    this.isVisibleUpdateModal = true;
  }

  validateUpdateNewShipAddress() {
    // var check = true;
    // this.updateShipAddressValidation = new UpdateShipAddressInputValidation();
    // if (!this.shipAddress.detailAddressInfo || this.shipAddress.detailAddressInfo == '') {
    //   this.updateShipAddressValidation.isValidDetailAddressInfo = false;
    //   this.updateShipAddressValidation.detailAddressInfoValidationMessage = "Address information can not empty";
    //   check = false;
    // } else {
    //   if (this.shipAddress.detailAddressInfo.length < 5) {
    //     this.updateShipAddressValidation.isValidDetailAddressInfo = false;
    //     this.updateShipAddressValidation.detailAddressInfoValidationMessage = "Address can not be too short. Min length: 5";
    //     check = false;
    //   }
    // }
    // console.log(this.updateShipAddressValidation);
    // return check;
    var check = true;
    this.updateShipAddressValidation = new UpdateShipAddressInputValidation();
    if (!this.shipAddress.detailAddressInfo || this.shipAddress.detailAddressInfo == '') {
      this.updateShipAddressValidation.isValidDetailAddressInfo = false;
      this.updateShipAddressValidation.detailAddressInfoValidationMessage = "Detail address information can not empty";
      check = false;
    } else {
      if (this.shipAddress.detailAddressInfo.length < 5) {
        this.updateShipAddressValidation.isValidDetailAddressInfo = false;
        this.updateShipAddressValidation.detailAddressInfoValidationMessage = "Detail address can not be too short. Min length: 5";
        check = false;
      }
    }

    if (!this.selectedProvince || this.selectedProvince == null) {
      this.updateShipAddressValidation.isValidProvince = false;
      this.updateShipAddressValidation.provinceValidationMessage = "Provice information can not empty";
      check = false;
    }
    if (!this.selectedDistrict || this.selectedDistrict == null) {
      this.updateShipAddressValidation.isValidDistrict = false;
      this.updateShipAddressValidation.districtValidationMessage = "District information can not empty";
      check = false;
    }
    if (!this.selectedWard || this.selectedWard == null) {
      this.updateShipAddressValidation.isValidWard = false;
      this.updateShipAddressValidation.wardValidationMessage = "Ward information can not empty";
      check = false;
    }

    console.log(this.updateShipAddressValidation);
    return check;
  }

  async updateShipAddress() {
    if (this.validateUpdateNewShipAddress()) {
      try {
        this.loading = true;

        let param = {
          addressId: this.shipAddress.addressId,
          addressInfo: this.shipAddress.detailAddressInfo + '. ' + this.selectedWard.name + ', ' + this.selectedDistrict.name + ', ' + this.selectedProvince.name
        }

        let response = await this.iServiceBase.putDataAsync(API.PHAN_HE.CUSTOMER, API.API_SHIPADDRESS.UPDATESHIPADDRESS, param);

        if (response && response.message === "Success") {
          this.showMessage(mType.success, 'Notification', 'Update ship address successfully', 'notify');
          console.log('Update ship address successfully.');
        } else {
          this.showMessage(mType.error, 'Notification', 'Update ship address failed', 'notify');
          console.log('Update ship address failed.');
        }

        // Clear model
        this.shipAddress = new ShipAddress();

        // Close modal
        this.isVisibleUpdateModal = false;

        // Reload all data
        this.getAllShipAddress();

        this.loading = false;
      } catch (e) {
        this.showMessage(mType.error, 'Notification', "There's error happened in the server. Please contact admin for help.", 'notify');
        console.log(e);

        // Clear model
        this.shipAddress = new ShipAddress();

        // Close modal
        this.isVisibleUpdateModal = false;

        // Reload all data
        this.getAllShipAddress();

        this.loading = false;
      }
    }
  }

  openDeleteDialog(addressId: number) {
    this.shipAddress = new ShipAddress();

    this.shipAddress.addressId = addressId;

    this.isVisibleDeleteModal = true;
  }

  async deleteShipAddress() {
    try {
      this.loading = true;

      let param = {
        addressId: this.shipAddress.addressId
      }

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.CUSTOMER, API.API_SHIPADDRESS.DELETESHIPADDRESS, param);

      if (response && response.message === "Success") {
        this.showMessage(mType.success, 'Notification', 'Delete ship address successfully', 'notify');
        console.log('Delete ship address successfully.');
      } else {
        this.showMessage(mType.error, 'Notification', 'Delete ship address failed', 'notify');
        console.log('Delete ship address failed.');
      }

      // Clear model
      this.shipAddress = new ShipAddress();

      // Close modal
      this.isVisibleDeleteModal = false;

      // Reload all data
      this.getAllShipAddress();

      this.loading = false;
    } catch (e) {
      this.showMessage(mType.error, 'Notification', "There's error happened in the server. Please contact admin for help.", 'notify');
      console.log(e);

      // Clear model
      this.shipAddress = new ShipAddress();

      // Close modal
      this.isVisibleDeleteModal = false;

      // Reload all data
      this.getAllShipAddress();

      this.loading = false;
    }
  }

  openSetDefaultDialog(addressId: number) {
    this.shipAddress = new ShipAddress();

    this.shipAddress.addressId = addressId;

    this.isVisibleSetDefaultAddressModal = true;
  }

  async setDefaultShipAddress() {
    try {
      this.loading = true;

      let param = {
        addressId: this.shipAddress.addressId
      }

      let response = await this.iServiceBase.putDataAsync(API.PHAN_HE.CUSTOMER, API.API_SHIPADDRESS.SETDEFAULTSHIPADDRESS, param);

      if (response && response.message === "Success") {
        this.showMessage(mType.success, 'Notification', 'Set default to the ship address successfully', 'notify');
        console.log('Set default to the ship address successfully.');
      } else {
        this.showMessage(mType.error, 'Notification', 'Set default to the ship address failed', 'notify');
        console.log('Set default to the ship address failed.');
      }

      // Clear model
      this.shipAddress = new ShipAddress();

      // Close modal
      this.isVisibleSetDefaultAddressModal = false;

      // Reload all data
      this.getAllShipAddress();

      this.loading = false;
    } catch (e) {
      this.showMessage(mType.error, 'Notification', "There's error happened in the server. Please contact admin for help.", 'notify');
      console.log(e);

      // Clear model
      this.shipAddress = new ShipAddress();

      // Close modal
      this.isVisibleSetDefaultAddressModal = false;

      // Reload all data
      this.getAllShipAddress();

      this.loading = false;
    }
  }

  // renderData = (array, select) => {
  //   let row = ' <option disable value="">Chọn</option>';
  //   array.forEach(element => {
  //     row += `<option data-id="${element.code}" value="${element.name}">${element.name}</option>`
  //   });
  //   document.querySelector("#" + select).innerHTML = row
  // }

}
