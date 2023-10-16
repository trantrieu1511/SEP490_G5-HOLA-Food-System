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

@Component({
  selector: 'app-shipper',
  templateUrl: './shipper.component.html',
  styleUrls: ['./shipper.component.scss']
})

export class ShipperComponent implements OnInit {
    items: MenuItem[] | undefined;
  
    activeItem: MenuItem | undefined;
  
    products: any[] = [];
  
    onActiveRequest: boolean = true;
  
    loading: boolean;
  
    showCurrentPageReport: boolean;
  
    constructor(private elementRef: ElementRef, private renderer: Renderer2) { }
  
    ngOnInit(): void {
      this.items = [
        { label: 'Requested', id: '0'},
        { label: 'Shipping', id: '1'},    
      ];
  
      this.activeItem = this.items[0];
  
  
      this.showCurrentPageReport = true;
    //   this.products = [{
    //       id: '1000',
    //       code: 'f230fh0g3',
    //       name: 'Bamboo Watch',
    //       description: 'Product Description',
    //       image: 'bamboo-watch.jpg',
    //       price: 65,
    //       category: 'Accessories',
    //       quantity: 24,
    //       inventoryStatus: 'INSTOCK',
    //       rating: 4,
    //       orders: [
    //           {
    //               id: '1000-0',
    //               productCode: 'f230fh0g3',
    //               date: '2020-09-13',
    //               amount: 65,
    //               quantity: 1,
    //               customer: 'David James',
    //               status: 'PENDING'
    //           },
    //       ]
    //   },];
    }
    onChangeTab(activeTab: MenuItem){
        this.activeItem = activeTab;        
        console.log(this.activeItem.id);
      }
}


