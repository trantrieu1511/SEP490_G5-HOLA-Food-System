import {Component, OnInit, ViewChild} from '@angular/core';
import {Table} from "primeng/table";
import {
    iComponentBase,
    iServiceBase, mType,
    ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../../services/apiURL";
import {
    ConfirmationService,
    MessageService,
} from "primeng/api";
import { Shipper } from '../../models/shipper.model';


@Component({
  selector: 'app-shipper',
  templateUrl: './shipper.component.html',
  styleUrls: ['./shipper.component.scss']
})
export class ShipperComponent extends iComponentBase implements OnInit{
  lstShipper: Shipper[] = [];
  selectedShippers: Shipper[] = [];
  displayDialogCreate: boolean = false;
  headerDialog: string = '';
  lstNhomRole: any[] = [];
  roleModel: Shipper = new Shipper();
  loading: boolean;


  @ViewChild('dt') table: Table;

  constructor(
              private shareData: ShareData,
              public messageService: MessageService,
              private confirmationService: ConfirmationService,
              private iServiceBase: iServiceBase) {
      super(messageService);

  }

  ngOnInit() {
    //this.getAllRole(); 
    this.onTest();
    
  }

  async onTest(){
    
  }

  onInitListNhomPhanQuyen() {
      this.lstNhomRole = [
          {label: 'Quản trị viên', value: "ROLE_ADMIN"},
          {label: 'Người dùng', value: "ROLE_USER"}
      ];
  }

  // async getAllRole() {
  //     this.lstShipper = [];
  //     this.selectedShippers = [];
  //     try {
  //         this.loading = true;

  //         let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.USER, API.API_USER.SHIPPERS);
          
  //         if (response && response.length) {
  //             this.lstShipper = response;
  //         }
  //         this.loading = false;
  //     } catch (e) {
  //         console.log(e);
  //         this.loading = false;
  //     }

  // }

  // bindingDataRoleModel(): AppRole {
  //     let result = new AppRole();
  //     if (this.shareData && this.shareData.userInfo) {
          
  //         if (this.roleModel.id && this.roleModel.id > 0) {
  //             //Update
  //             result.id = this.roleModel.id;
  //             result.roleId = this.roleModel.roleId;
  //             result.roleKey = this.roleModel.roleKey;
  //             result.roleName = this.roleModel.roleName;
  //             result.roleDescribe = this.roleModel.roleDescribe;
  //             result.active = this.roleModel.active;
  //             result.lastModifiedBy = this.shareData.userInfo.userName;
  //             result.lastModifiedDate = new Date();
  //             result.createdBy = this.roleModel.createdBy;
  //             result.createdDate = this.roleModel.createdDate;

  //         } else {
  //             //Insert
  //             result.roleId = this.roleModel.roleId;
  //             result.roleKey = this.roleModel.roleKey;
  //             result.roleName = this.roleModel.roleName;
  //             result.roleDescribe = this.roleModel.roleDescribe;
  //             result.active = this.roleModel.active;
  //             result.createdBy = this.shareData.userInfo.userName;
  //             result.createdDate = new Date();
  //         }
  //     }

  //     return result;
  // }
  
  // onCreateRole() {
  //     this.headerDialog = 'Thêm mới phân quyền';

  //     this.roleModel.roleId = 'ROLE_ADMIN';
  //     this.roleModel.active = true;
  //     this.roleModel = new AppRole();

  //     this.displayDialogCreateRole = true;
  // }

  // onUpdateRole(role: AppRole) {
  //     this.headerDialog = `Cập nhật phân quyền: ${role.roleName}`;

  //     this.roleModel = role;

  //     this.displayDialogCreateRole = true;
  // }

  // onDeleteRole(role: AppRole, event) {
  //     this.confirmationService.confirm({
  //         target: event.target,
  //         message: `Bạn có chắc chắn xóa phân quyền ${role.roleName} này ?`,
  //         icon: 'pi pi-exclamation-triangle',
  //         accept: () => {
  //             //confirm action
  //             this.deleteRole(role);
  //         },
  //         reject: () => {
  //             //reject action
  //         }
  //     });
  // }

  // onDeleteListRole() {
  //     this.confirmationService.confirm({
  //         key: 'deleteList',
  //         message: 'Bạn có chắc chắn xóa những phân quyền đã chọn ?',
  //         accept: () => {
  //             //Actual logic to perform a confirmation
  //             this.deleteListRole();
  //         }
  //     });
  // }

  // onSaveRole() {
  //     let roleEnity = this.bindingDataRoleModel();

  //     if (this.validateRoleModel()) {
  //         if (roleEnity && roleEnity.id && roleEnity.id > 0) {
  //             this.updateRole(roleEnity);
  //         } else {
  //             this.createRole(roleEnity);
  //         }
  //     }
  // }

  // onCancelRole() {
  //     this.roleModel = new AppRole();

  //     this.displayDialogCreateRole = false;
  // }

  // validateRoleModel(): boolean {
  //     if (!this.roleModel.roleKey || this.roleModel.roleKey == '') {
  //         this.showMessage(mType.warn, "Thông báo", "Mã phân quyền không được để trống. Vui lòng nhập!", 'notify');
  //         return false;
  //     }

  //     if (!this.roleModel.roleName || this.roleModel.roleName == '') {
  //         this.showMessage(mType.warn, "Thông báo", "Tên phân quyền không được để trống. Vui lòng nhập!", 'notify');
  //         return false;
  //     }

  //     if (!this.roleModel.roleId || this.roleModel.roleId == '') {
  //         this.showMessage(mType.warn, "Thông báo", "Mã nhóm phân quyền không được để trống. Vui lòng chọn!", 'notify');
  //         return false;
  //     }

  //     return true;
  // }

  // async createRole(roleEnity) {
  //     try {
  //         let param = roleEnity;

  //         // let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.QTHT, API.API_QTHT.INSERT_APP_ROLE, param, true);

  //         // if (response && response.success) {
  //         //     this.showMessage(mType.success, "Thông báo", "Thêm mới phân quyền thành công!", 'notify');

  //         //     this.displayDialogCreateRole = false;

  //         //     //lấy lại danh sách All Role
  //         //     this.getAllRole();

  //         //     //Clear Role model đã tạo
  //         //     this.roleModel = new AppRole();
  //         // } else {
  //         //     this.showMessage(mType.error, "Thông báo", "Thêm mới phân quyền không thành công. Vui lòng xem lại!", 'notify');
  //         // }
  //     } catch (e) {
  //         console.log(e);
  //     }
  // }

  // async updateRole(roleEnity) {
  //     try {
  //         let param = roleEnity;

  //         // let response = await this.iServiceBase.putDataAsync(API.PHAN_HE.QTHT, API.API_QTHT.UPDATE_APP_ROLE, param, true);

  //         // if (response && response.success) {
  //         //     this.showMessage(mType.success, "Thông báo", "Cập nhật phân quyền thành công!", 'notify');

  //         //     this.displayDialogCreateRole = false;

  //         //     //lấy lại danh sách All Role
  //         //     this.getAllRole();

  //         //     //Clear Role model đã tạo
  //         //     this.roleModel = new AppRole();
  //         // } else {
  //         //     this.showMessage(mType.error, "Thông báo", "Cập nhật phân quyền không thành công. Vui lòng xem lại!", 'notify');
  //         // }
  //     } catch (e) {
  //         console.log(e);
  //     }
  // }

  // async deleteRole(roleEnity) {
  //     try {
  //         let param = roleEnity.id;

  //         // let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.QTHT, API.API_QTHT.DELETE_APP_ROLE, param, true);

  //         // if (response && response.success) {
  //         //     this.showMessage(mType.success, "Thông báo", "Xóa phân quyền thành công!", 'notify');

  //         //     //lấy lại danh sách All Role
  //         //     this.getAllRole();

  //         // } else {
  //         //     this.showMessage(mType.error, "Thông báo", "Xóa phân quyền không thành công. Vui lòng xem lại!", 'notify');
  //         // }
  //     } catch (e) {
  //         console.log(e);
  //     }
  // }

  // async deleteListRole() {
  //     try {
  //         let param = this.selectedRoles.map(o => o.id);

  //         // let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.QTHT, API.API_QTHT.DELETE_LIST_APP_ROLE, param, true);

  //         // if (response) {
  //         //     this.showMessage(mType.success, "Thông báo", "Xóa phân quyền thành công!", 'notify');

  //         //     //lấy lại danh sách All Role
  //         //     this.getAllRole();

  //         // } else {
  //         //     this.showMessage(mType.error, "Thông báo", "Xóa phân quyền không thành công. Vui lòng xem lại!", 'notify');
  //         // }
  //     } catch (e) {
  //         console.log(e);
  //     }
  // }

  // getSeverity(status: boolean) {
  //     switch (status) {
  //         case true:
  //             return 'success';
  //         case false:
  //             return 'danger';
  //     }
  // }
}
