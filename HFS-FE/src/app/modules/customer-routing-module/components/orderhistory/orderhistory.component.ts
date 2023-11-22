import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  Renderer2,
} from '@angular/core';
import {
  ConfirmationService,
  LazyLoadEvent,
  MenuItem,
  MessageService,
  SelectItem,
  TreeNode,
} from 'primeng/api';
import { iComponentBase, iServiceBase, mType } from 'src/app/modules/shared-module/shared-module';
import { FeedBackInput, Order, OrderAcceptInput, OrderCancelInput, OrderCancelInputValidation, OrderDetailFoodDto, OrderStatusInput } from '../../models/order.model';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import {DatePipe} from '@angular/common';
import * as API from '../../../../services/apiURL';
import { TabView, TabViewChangeEvent } from 'primeng/tabview';
import { RateInput } from '../../models/RateInput.model';
import { FileRemoveEvent, FileSelectEvent, FileUpload } from 'primeng/fileupload';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-orderhistory',
  templateUrl: './orderhistory.component.html',
  styleUrls: ['./orderhistory.component.scss'],
})
export class OrderhistoryComponent
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
  displayDialogRateOrder: boolean = false;
  orderCancelInput: OrderCancelInput = new OrderCancelInput();
  rateInput : RateInput = new RateInput();
  orderCancelValidateInput: OrderCancelInputValidation =
    new OrderCancelInputValidation();

  // visibleShipperLstDialog: boolean = false;
  // lstShipper: Shipper[] = [];
  // selectedShipper!: Shipper;
  // loadingShipperLst: boolean;
  orderPreparingSelected: Order = new Order();
  uploadedFiles: File[] = [];
  rangeDates: Date[] | undefined;
  currentDate: Date = new Date();
  isDisableCalendar: boolean = true;
  disabledIds = ['0', '1', '2', '3'];

  constructor(
    public breadcrumbService: AppBreadcrumbService,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private datePipe: DatePipe,
    public authService: AuthService
  ) {
    super(messageService);
  }



  async ngOnInit() {
    this.setDefaultDate();
    this.initTabMenuitem();
    this.getAllOrders();
    this.showCurrentPageReport = true;
  }

  setDefaultDate(){
    this.rangeDates = [];
    this.rangeDates[0] = new Date(2022, this.currentDate.getMonth(), 1);
    this.rangeDates[1] = new Date();
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
        API.PHAN_HE.ORDERCUSTOMER,
        API.API_ORDERCUSTOMER.GET_ORDER,
        this.orderParamInput
      );

      if (response && response.message === 'Success') {
        this.lstOrders = response.listOrders;
        console.log(this.lstOrders);
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  onChangeTab(activeTab: TabViewChangeEvent) {
    this.activeItem.id = activeTab.index.toString();
    this.getAllOrders();
    //this.onActiveRequest = false;
    console.log(this.activeItem.id);

    if (this.disabledIds.includes(this.activeItem.id)) {
      this.setDefaultDate();
      this.isDisableCalendar = true;
      return;
    }
    this.isDisableCalendar = false;
  }

  onCloseCalendar(event: any){
    this.getAllOrders();
  }

  onCancel(order: Order, event) {
    this.headerDialog = 'Confirm cancel Order';

    this.orderCancelInput = new OrderCancelInput();
    this.orderCancelInput.orderId = order.orderId;
    this.orderCancelValidateInput = new OrderCancelInputValidation();

    this.displayDialogCancelOrder = true;
  }

  onSaveCancel() {
    if (this.validateOrderCancelInput()) {
      document.body.style.cursor = 'wait';

      this.cancelOrder();
    }
  }

  onRate(food: OrderDetailFoodDto, event) {
    this.rateInput = new RateInput();
    this.rateInput.foodId = food.foodId;
    this.rateInput.star = 5;
    // this.orderCancelValidateInput = new OrderCancelInputValidation();

    this.displayDialogRateOrder = true;
  }

  async onSaveRate(){
    let response = await this.iServiceBase.postDataAsync(
      API.PHAN_HE.ORDERCUSTOMER,
      API.API_ORDERCUSTOMER.RATE_FOOD,
      this.rateInput,
      true
    );

    if (response && response.message === 'Success') {
      this.showMessage(
        mType.success,
        'Notification',
        'Rate food successfully',
        'notify'
      );

      this.displayDialogRateOrder = false;

      //lấy lại danh sách All
      this.getAllOrders();

      //Clear model đã tạo
      this.rateInput = new RateInput();
    } else {
      var messageError = this.iServiceBase.formatMessageError(response);
      console.log(messageError);
      this.showMessage(mType.error, response.message, messageError, 'notify');
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

  onCancelCancel() {
    this.orderCancelInput = new OrderCancelInput();

    this.displayDialogCancelOrder = true;
  }

  async cancelOrder() {
    // them status
    let response = await this.iServiceBase.postDataAsync(
      API.PHAN_HE.ORDERCUSTOMER,
      API.API_ORDERCUSTOMER.CANCEL_ORDER,
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


  // onExternal(order: Order, event: any) {
  //   this.confirmationService.confirm({
  //     target: event.target,
  //     message: `Are you sure to Accept this Order id: ${order.orderId} ?`,
  //     icon: 'pi pi-exclamation-triangle',
  //     accept: () => {
  //       // document.body.style.cursor = 'wait';
  //       // //confirm action
  //       // this.acceptOrder(order);
  //     },
  //     reject: () => {
  //       //reject action
  //     },
  //   });
  // }

  handleFileSelection(event: FileSelectEvent) {
    //console.log("select", event);

    this.uploadedFiles = event.currentFiles;

    //console.log('primeSelect',this.f_upload);
    console.log("uploadFiles", this.uploadedFiles);
  }

  handleFileRemoval(event: FileRemoveEvent) {
    console.log("remove", event.file.name);

    this.uploadedFiles = this.uploadedFiles.filter(f => f.name !== event.file.name);
    console.log("uploadFiles", this.uploadedFiles);
  }

  handleAllFilesClear(event: Event) {
    //console.log("clear", event);

    this.uploadedFiles = [];
    console.log("uploadFiles", this.uploadedFiles);
  }

  async createFeedback(rate: RateInput) {
    debugger
    rate.images=this.uploadedFiles;
    const param = new FormData();

    this.uploadedFiles.forEach(file => {
      param.append('images', file, file.name);
    });

    Object.keys(rate).forEach(function (key) {
      param.append(key, rate[key]);
    });

    const response = await this.iServiceBase
      .getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_ORDER.FEEDBACK, param);
    //console.log(response);
    if (response && response.message === "Success") {
      this.showMessage(mType.success, "Notification", "New FeedBack added successfully", 'notify');

      this.displayDialogRateOrder = false;

          //lấy lại danh sách All
          this.getAllOrders();

      //Clear model đã tạo
      this.rateInput=new RateInput();
    //  this.foodModel = new Food();
      //clear file upload too =))
      this.uploadedFiles = [];
      this.displayDialogRateOrder = false;
    } else {
      var messageError = this.iServiceBase.formatMessageError(response);
      console.log(messageError);
      this.showMessage(mType.error, response.message, messageError, 'notify');
    }
  }
}

