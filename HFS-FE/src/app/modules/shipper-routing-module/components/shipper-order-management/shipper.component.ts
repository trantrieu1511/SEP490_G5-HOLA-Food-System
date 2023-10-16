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

@Component({
  selector: 'app-shipper',
  templateUrl: './shipper.component.html',
  styleUrls: ['./shipper.component.scss']
})

export class ShipperComponent extends iComponentBase implements OnInit {
    items: MenuItem[] | undefined;
  
    activeItem: MenuItem | undefined;
  
    products: any[] = [];
  
    onActiveRequest: boolean = true;
  
    loading: boolean;
  
    showCurrentPageReport: boolean;
  
    lstOrderOfShipper: OrderDaoOutputDto[];




    constructor(private elementRef: ElementRef, private renderer: Renderer2,public messageService: MessageService,
        private confirmationService: ConfirmationService,
        private iServiceBase: iServiceBase,) {
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
      }

    async getAllOrder() {
      this.lstOrderOfShipper = [];
      try {
        this.loading = true;

        const param = {
            "shipperId": 1
        };

        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.SHIPPER, API.API_SHIPPER.GET_All, param);
        if (response && response.message === "Success") {
            this.lstOrderOfShipper = response.orderList;
            this.calculatorTotalOrder();
        }
          
        this.loading = false;
      } catch (e) {
        console.log(e);
        this.loading = false;
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
}


