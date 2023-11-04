import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  Renderer2,
} from '@angular/core';
import {
  Order,
  OrderAcceptInput,
  OrderCancelInput,
  OrderCancelInputValidation,
  OrderInternalInput,
  OrderStatusInput,
} from '../../models/order.model';
import { Table } from 'primeng/table';
import { AppBreadcrumbService } from '../../../../app-systems/app-breadcrumb/app.breadcrumb.service';
import {
  Food,
  Category,
  FoodInput,
  FoodDisplayHideInputDto,
  FoodInputValidation,
} from '../../models/food.model';
import {
  iComponentBase,
  iServiceBase,
  mType,
  ShareData,
  iFunction,
} from 'src/app/modules/shared-module/shared-module';
import * as API from '../../../../services/apiURL';
import {
  ConfirmationService,
  LazyLoadEvent,
  MenuItem,
  MessageService,
  SelectItem,
  TreeNode,
} from 'primeng/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Shipper } from '../../models/shipper.model';
import {DatePipe} from '@angular/common';

@Component({
  selector: 'app-order-management',
  templateUrl: './order-management.component.html',
  styleUrls: ['./order-management.component.scss'],
})
export class OrderManagementComponent
  extends iComponentBase
  implements OnInit, AfterViewInit
{
  items: MenuItem[] | undefined;

  activeItem: MenuItem | undefined;

  lstOrders: Order[] = [];

  loading: boolean;

  showCurrentPageReport: boolean;

  orderParamInput: OrderStatusInput = new OrderStatusInput();

  headerDialog: string;
  displayDialogCancelOrder: boolean = false;
  orderCancelInput: OrderCancelInput = new OrderCancelInput();
  orderCancelValidateInput: OrderCancelInputValidation =
    new OrderCancelInputValidation();

  visibleShipperLstDialog: boolean = false;
  lstShipper: Shipper[] = [];
  selectedShipper!: Shipper;
  loadingShipperLst: boolean;
  orderPreparingSelected: Order = new Order();

  rangeDates: Date[] | undefined;
  currentDate: Date = new Date();
  isDisableCalendar: boolean = true;
  disabledIds = ['0', '1', '2', '3'];

  constructor(
    public breadcrumbService: AppBreadcrumbService,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private datePipe: DatePipe
  ) {
    super(messageService);
    this.rangeDates = [];
    this.rangeDates[0] = this.rangeDates[1] = new Date();
  }

  async ngOnInit() {
    this.initTabMenuitem();
    this.getAllOrders();
    
    this.showCurrentPageReport = true;
  }

  ngAfterViewInit() {
    // const tabmenuitem = this.elementRef.nativeElement.querySelector('.p-tabmenuitem');
    // const tabmenu_ink_bar = this.elementRef.nativeElement.querySelector('.p-tabmenu-ink-bar');
    // this.renderer.setStyle(tabmenu_ink_bar, 'width', tabmenuitem.offsetWidth);
    //console.log('Width:', width);
  }

  initTabMenuitem() {
    this.items = [
      { label: 'Requested', id: '0' },
      { label: 'Preparing', id: '1' },
      { label: 'Wait Shipper', id: '2' },
      { label: 'Shipping', id: '3' },
      { label: 'Completed', id: '4' },
      { label: 'InCompleted', id: '5' },
      { label: 'Cancel', id: '6' },
    ];

    this.activeItem = this.items[0];
  }

  async getAllOrders() {
    this.lstOrders = [];
    try {
      this.loading = true;
      if(this.rangeDates){
        this.orderParamInput.dateFrom = this.datePipe.transform(this.rangeDates[0], "yyyy-MM-dd");
        this.orderParamInput.dateEnd = this.datePipe.transform(this.rangeDates[1], "yyyy-MM-dd");
      }else{
        this.orderParamInput.dateFrom = this.orderParamInput.dateEnd = this.datePipe.transform(new Date(), "yyyy-MM-dd");
      }

      this.orderParamInput.status =
        this.activeItem.id != undefined ? parseInt(this.activeItem.id) : 0;

      let response = await this.iServiceBase.postDataAsync(
        API.PHAN_HE.ORDER,
        API.API_ORDER.GET_ORDER_BY_STATUS,
        this.orderParamInput
      );

      if (response && response.message === 'Success') {
        this.lstOrders = response.orders;
        console.log(this.lstOrders);
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  onChangeTab(activeTab: MenuItem) {
    this.activeItem = activeTab;
    this.getAllOrders();
    //this.onActiveRequest = false;
    //console.log(this.activeItem.id);

    if (this.disabledIds.includes(this.activeItem.id)) {
      this.rangeDates = [];
      this.rangeDates[0] = this.rangeDates[1] = new Date();
      this.isDisableCalendar = true;
      return;
    }
    this.isDisableCalendar = false;
  }

  onCloseCalendar(event: any){
    this.getAllOrders();
  }

  onAccept(order: Order, event: any) {
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure to Accept this Order id: ${order.orderId} ?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        document.body.style.cursor = 'wait';
        //confirm action
        this.acceptOrder(order);
      },
      reject: () => {
        //reject action
      },
    });
  }

  onCancel(order: Order, event) {
    this.headerDialog = 'Confirm deny Order';

    this.orderCancelInput = new OrderCancelInput();
    this.orderCancelInput.orderId = order.orderId;
    this.orderCancelValidateInput = new OrderCancelInputValidation();

    this.displayDialogCancelOrder = true;
  }

  onSaveDeny() {
    if (this.validateOrderCancelInput()) {
      document.body.style.cursor = 'wait';

      this.cancelOrder();
    }
  }

  validateOrderCancelInput(): boolean {
    console.log(this.orderCancelInput);
    this.orderCancelValidateInput = new OrderCancelInputValidation();
    var check = true;
    if (!this.orderCancelInput.note || this.orderCancelInput.note == '') {
      this.orderCancelValidateInput.isNoteValid = false;
      this.orderCancelValidateInput.noteMessage = 'Please enter a reason';
      check = false;
    }

    return check;
  }

  onCancelDeny() {
    this.orderCancelInput = new OrderCancelInput();

    this.displayDialogCancelOrder = true;
  }

  async cancelOrder() {
    // them status
    this.orderCancelInput.status = 6;
    let response = await this.iServiceBase.postDataAsync(
      API.PHAN_HE.ORDER,
      API.API_ORDER.CANCEL_ORDER,
      this.orderCancelInput,
      true
    );

    if (response && response.message === 'Success') {
      this.showMessage(
        mType.success,
        'Notification',
        'Cancel Order successfully',
        'notify'
      );

      this.displayDialogCancelOrder = false;

      //lấy lại danh sách All
      this.getAllOrders();

      //Clear model đã tạo
      this.orderCancelInput = new OrderCancelInput();
    } else {
      var messageError = this.iServiceBase.formatMessageError(response);
      console.log(messageError);
      this.showMessage(mType.error, response.message, messageError, 'notify');
    }
  }

  async acceptOrder(order: Order) {
    // them status
    const param: OrderAcceptInput = new OrderAcceptInput(order.orderId, 1);
    let response = await this.iServiceBase.postDataAsync(
      API.PHAN_HE.ORDER,
      API.API_ORDER.ACCEPT_ORDER,
      param,
      true
    );

    if (response && response.message === 'Success') {
      this.showMessage(
        mType.success,
        'Notification',
        'Accept Order successfully',
        'notify'
      );

      //lấy lại danh sách All
      this.getAllOrders();
    } else {
      var messageError = this.iServiceBase.formatMessageError(response);
      console.log(messageError);
      this.showMessage(mType.error, response.message, messageError, 'notify');
    }
  }

  onInternal(order: Order): void {
    this.orderPreparingSelected = Object.assign({}, order);

    this.visibleShipperLstDialog = true;

    this.selectedShipper = new Shipper();

    this.getAllShipper();
  }

  onExternal(order: Order, event: any) {
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure to Accept this Order id: ${order.orderId} ?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        // document.body.style.cursor = 'wait';
        // //confirm action
        // this.acceptOrder(order);
      },
      reject: () => {
        //reject action
      },
    });
  }

  async getAllShipper() {
    this.lstShipper = [];

    this.loadingShipperLst = true;

    let response = await this.iServiceBase.getDataAsync(
      API.PHAN_HE.USER,
      API.API_USER.GET_SHIPPERS_AVAILABLE
    );

    if (response && response.message === 'Success') {
      this.lstShipper = response.shippers;
    } else {
      var messageError = this.iServiceBase.formatMessageError(response);
      this.showMessage(mType.error, response.message, messageError, 'notify');
    }
    this.loadingShipperLst = false;
  }

  onChoose() {
    if (!this.selectedShipper) {
      this.showMessage(
        mType.error,
        'Error',
        'Please select shipper row want to choose',
        'notify'
      );
      return;
    }
    document.body.style.cursor = 'wait';
    this.commissionInternalShipper();
  }

  onCancelChoose() {
    this.visibleShipperLstDialog = false;
    this.selectedShipper = new Shipper();
  }

  async commissionInternalShipper() {
    const orderId = this.orderPreparingSelected.orderId;
    const shipperId = this.selectedShipper.shipperId;

    const param: OrderInternalInput = new OrderInternalInput(
      orderId,
      2,
      shipperId
    );
    let response = await this.iServiceBase.postDataAsync(
      API.PHAN_HE.ORDER,
      API.API_ORDER.INTERNAL_ORDER,
      param,
      true
    );

    if (response && response.message === 'Success') {
      this.showMessage(
        mType.success,
        'Notification',
        `Commission shipperId: ${shipperId} to orderId: ${orderId} successfully`,
        'notify'
      );

      //lấy lại danh sách All
      this.getAllOrders();

      this.visibleShipperLstDialog = false;
    } else {
      var messageError = this.iServiceBase.formatMessageError(response);
      console.log(messageError);
      this.showMessage(mType.error, response.message, messageError, 'notify');
    }
  }
}
