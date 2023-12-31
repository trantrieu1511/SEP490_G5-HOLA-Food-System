import {Table} from "primeng/table";
import {AppBreadcrumbService} from "../../../../app-systems/app-breadcrumb/app.breadcrumb.service";
import { AfterViewInit, Component, ElementRef, OnInit, Renderer2,ViewChild } from '@angular/core';
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
import { OrderDaoOutputDto } from "../../models/order-of-shipper.model";
import { AuthService, User } from "src/app/services/auth.service";
import { OrderProgress } from "../../models/order-progress-shipper.model";
import { Post } from "src/app/modules/seller-routing-module/models/post.model";
import { FileRemoveEvent, FileSelectEvent } from "primeng/fileupload";

@Component({
  selector: 'app-shipper',
  templateUrl: './shipper.component.html',
  styleUrls: ['./shipper.component.scss']
})

export class ShipperComponent extends iComponentBase implements OnInit {
    items: MenuItem[] | undefined;

    displayDialogConfirm: boolean = false;

    headerDialog: string = '';

    postModel: Post = new Post();
    
    userId : string ;

    activeItem: MenuItem | undefined;
  
    products: any[] = [];
  
    onActiveRequest: boolean = true;
  
    loading: boolean;
  
    showCurrentPageReport: boolean;
  
    lstOrderOfShipper: OrderDaoOutputDto[];

    selectedOrderId : number;

    note : string;

    confirmModel: OrderProgress = new OrderProgress();
    
    selectedtype:number;

    uploadedFiles: File[] = [];

    contentDialog: string;
    visibleContentDialog: boolean = false;

    postImageDialog: OrderProgress = new OrderProgress();
    visibleImageDialog: boolean = false;

    constructor(private elementRef: ElementRef, private renderer: Renderer2,public messageService: MessageService,
        private confirmationService: ConfirmationService,
        private iServiceBase: iServiceBase,private authService: AuthService) {
        super(messageService);
    }
  
    ngOnInit(): void {
      this.items = [
        { label: 'Requested', id: '0'},
        { label: 'Shipping', id: '1'},    
      ];
  
      this.activeItem = this.items[0];
  
  
      this.showCurrentPageReport = true;

      this.getAllOrder();
      
      
    }

    onChangeTab(activeTab: MenuItem){
        this.activeItem = activeTab;        
        console.log(this.activeItem.id);
        this.getAllOrder();
      }

    async onUpdateOrder(order:OrderDaoOutputDto){
      this.userId = sessionStorage.getItem('userId');
      const param = new FormData();
      param.append('orderId', order.orderId.toString());
      param.append('status', "3");
      param.append('notes', "dang ship nha");
      let response =  await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.SHIPPER, API.API_SHIPPER.CHANGE_STATUS,param);
      if (response && response.message === "Success") {
        this.lstOrderOfShipper = response.orderList;
        this.calculatorTotalOrder();
    }
    }

    async getAllOrder() {
      this.lstOrderOfShipper = [];
      try {
        this.loading = true;
        this.userId = sessionStorage.getItem('userId');  

        const param = {
            "shipperId":parseInt(this.userId),
            
            "Status" : this.activeItem.id == "0" ? true : false,
        };

        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.SHIPPER, API.API_SHIPPER.GET_All, param);
        if (response && response.message === "Success") {
            this.lstOrderOfShipper = response.orders;
            console.log(response);
            this.calculatorTotalOrder();
        }
          
        this.loading = false;
      } catch (e) {
        console.log(e);
        this.loading = false;
      }
    }
    Complete(orderId : number,type:number){
  
        this.headerDialog = 'Confirm';        

        this.selectedOrderId = orderId;
        this.selectedtype = type;
        this.note = "";

        this.uploadedFiles = [];
  
        this.displayDialogConfirm = true;
    
    }
    InComplete(orderId : number,type:number){
  
      this.headerDialog = 'Confirm';        

      this.selectedOrderId = orderId;
      this.selectedtype = type;
      this.note = "";

      this.uploadedFiles = [];

      this.displayDialogConfirm = true;
  
  }
    onCancel(){
      this.displayDialogConfirm = false;
    }
    async Save(){
      try {
        //let param = postEnity;
        const param = new FormData();
        param.append('orderId', this.selectedOrderId.toString());
        param.append('note', this.note);
        param.append('status',this.selectedtype == 0? "4" : "5");
        this.uploadedFiles.forEach(file => {
          param.append('image', file, file.name);
        });
  
        //console.log(param);
        const response = await this.iServiceBase
          .postDataAsync(API.PHAN_HE.SHIPPER, API.API_SHIPPER.CHANGE_STATUS, param);
        
          if(response && response.message === "Success"){
          this.showMessage(mType.success, "Notification", "Confirm successfully", 'notify');
  
          this.displayDialogConfirm = false;
  
          //lấy lại danh sách All 
          this.getAllOrder();
  
          //Clear model đã tạo
          
          //clear file upload too =))
          this.uploadedFiles = [];
        } 
      } catch (e) {
        console.log(e);
        this.showMessage(mType.error, "Notification", "Confirm failed", 'notify');
      }
    }
    calculatorTotalOrder(){
      if(this.lstOrderOfShipper.length > 0){
        this.lstOrderOfShipper.forEach( value => {
          let amount = 0;

          if(value.orderDetails.length == 1) {
            value.total = value.orderDetails[0].unitPrice * value.orderDetails[0].quantity;
            return;
          }

          value.orderDetails.forEach( value => {
            amount += value.unitPrice * value.quantity;
          });

          value.total = amount;
        });
      }
    }
    handleFileSelection(event :  FileSelectEvent) {
      
      this.uploadedFiles = event.currentFiles;
  
    }
  
    formatSize(size: number): string {
      // Format file size as needed
      // Example: Convert bytes to KB
      return (size / 1024).toFixed(2) + ' KB';
    }
  
    handleFileRemoval(event :  FileRemoveEvent){
      //console.log("remove", event.file.name);
  
      this.uploadedFiles = this.uploadedFiles.filter(f => f.name !== event.file.name);
      //console.log("uploadFiles", this.uploadedFiles);
    }
  
    handleAllFilesClear(event :  Event){
      //console.log("clear", event);
  
      this.uploadedFiles = [];
      //console.log("uploadFiles", this.uploadedFiles);
    }
  
    
}


