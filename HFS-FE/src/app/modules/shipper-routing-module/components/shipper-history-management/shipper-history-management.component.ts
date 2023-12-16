import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AuthService } from 'src/app/services/auth.service';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../../services/apiURL";
import { OrderDaoOutputDto, OrderDetailDtoOutput, OrderProgressDtoOutput } from '../../models/order-of-shipper.model';
import { TranslateService } from '@ngx-translate/core';
import { DatePipe } from '@angular/common';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';

@Component({
  selector: 'app-shipper-history-management',
  templateUrl: './shipper-history-management.component.html',
  styleUrls: ['./shipper-history-management.component.scss']
})
export class ShipperHistoryManagementComponent extends iComponentBase implements OnInit {

  userId: string;

  lstOrderHistory: OrderDaoOutputDto[];

  loading: boolean;

  showCurrentPageReport: boolean;

  headerDialog: string = '';

  displayDialogConfirm: boolean = false;

  orderDetails: OrderDetailDtoOutput[];
  orderProgresses: OrderProgressDtoOutput[];

  rangeDates: Date[] | undefined;
  currentDate: Date = new Date();
  disabledIds = ['0', '1', '2', '3'];

  constructor(private elementRef: ElementRef, private renderer: Renderer2, public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private datePipe: DatePipe,
    private iServiceBase: iServiceBase, private authService: AuthService, public translate: TranslateService,public breadcrumbService: AppBreadcrumbService) {
    super(messageService, breadcrumbService);

    this.breadcrumbService.setItems([
      {label: 'HFSBusiness'},
      {label: 'History Management', routerLink: ['/HFSBusiness/shipper/history']}
    ]);
    this.rangeDates = [];
    this.rangeDates[0] = this.rangeDates[1] = new Date();
  }

  ngOnInit(): void {
    this.userId = this.authService.getUserInfor().userId;
    this.getAllOrder();
  }

  async getAllOrder() {
    // this.userId = sessionStorage.getItem('userId'); 
    let dateFrom;
    let dateEnd;
    if(this.rangeDates){
      dateFrom = this.datePipe.transform(this.rangeDates[0], "yyyy-MM-dd");
      dateEnd = this.datePipe.transform(this.rangeDates[1], "yyyy-MM-dd");
    }else{
      dateFrom = dateEnd = this.datePipe.transform(new Date(), "yyyy-MM-dd");
    }

    const param = {
      dateFrom: dateFrom,
      dateEnd: dateEnd
    };
    let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.SHIPPER, API.API_SHIPPER.HISTORY, param);
    if (response && response.success) {
      
      
      this.lstOrderHistory = response.orders ? response.orders : [];
      console.log('his nef', this.lstOrderHistory);
      this.calculatorTotalOrder();
    }

    this.loading = false;
  } catch(e) {
    console.log(e);
    this.loading = false;
  }

  calculatorTotalOrder() {
    if (this.lstOrderHistory.length > 0) {
      this.lstOrderHistory.forEach(value => {
        let amount = 0;

        if (value.orderDetails.length == 1) {
          value.total = value.orderDetails[0].unitPrice * value.orderDetails[0].quantity;
          return;
        }

        value.orderDetails.forEach(value => {
          amount += value.unitPrice * value.quantity;
        });

        value.total = amount;
      });
    }
  }

  Detail(orderId: number) {
    const orderFiltered = this.lstOrderHistory.filter(x => x.orderId == orderId)[0];

    this.orderDetails = orderFiltered.orderDetails;
    this.orderProgresses = orderFiltered.orderProgresses;
    this.headerDialog = 'Detail';
    this.displayDialogConfirm = true;

  }

  onCloseCalendar(event: any) {
    let fromDate = this.datePipe.transform(this.rangeDates[0], "yyyy-MM-dd");
    let endDate = this.datePipe.transform(this.rangeDates[1], "yyyy-MM-dd");
    if (!endDate) {
      this.rangeDates[1] = this.rangeDates[0];
      endDate = fromDate;
    }
    this.getAllOrder();
  }
}

