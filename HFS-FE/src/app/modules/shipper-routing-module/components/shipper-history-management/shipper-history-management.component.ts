import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AuthService } from 'src/app/services/auth.service';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../../services/apiURL";
import { OrderDaoOutputDto, OrderDetailDtoOutput } from '../../models/order-of-shipper.model';
@Component({
  selector: 'app-shipper-history-management',
  templateUrl: './shipper-history-management.component.html',
  styleUrls: ['./shipper-history-management.component.scss']
})
export class ShipperHistoryManagementComponent extends iComponentBase implements OnInit {

  userId : string ;

  lstOrderHistory: OrderDaoOutputDto[];

  loading: boolean;

  showCurrentPageReport: boolean;

  headerDialog: string = '';

  displayDialogConfirm: boolean = false;

  orderDetails: OrderDetailDtoOutput[];   



  constructor(private elementRef: ElementRef, private renderer: Renderer2,public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,private authService: AuthService) {
    super(messageService);
  }

  ngOnInit(): void {
    this.getAllOrder();
  }

  async getAllOrder(){
    this.userId = sessionStorage.getItem('userId'); 
    const param = {
      "shipperId":this.userId,

    };
    let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.SHIPPER, API.API_SHIPPER.HISTORY,param);
        if (response && response.message === "Success") {
            this.lstOrderHistory = response.orders;
            console.log(response);
            this.calculatorTotalOrder();
        }
          
        this.loading = false;
      } catch (e) {
        console.log(e);
        this.loading = false;
      }
    
      calculatorTotalOrder(){
        if(this.lstOrderHistory.length > 0){
          this.lstOrderHistory.forEach( value => {
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

  Detail(orderId : number){
    this.orderDetails = this.lstOrderHistory.filter( x => x.orderId == orderId)[0].orderDetails;
    this.headerDialog = 'Detail';        
    this.displayDialogConfirm = true;
    
  }
}

