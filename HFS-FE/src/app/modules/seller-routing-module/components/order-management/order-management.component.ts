import {
  AfterViewInit,
  Component,
  ElementRef,
  HostListener,
  OnInit,
  Renderer2,
} from '@angular/core';
import {
  Order,
  OrderAcceptInput,
  OrderCancelInput,
  OrderCancelInputValidation,
  OrderExternalInput,
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
import { DataRealTimeService } from 'src/app/services/SignalR/data-real-time.service';
import { TranslateService } from '@ngx-translate/core';
declare var google: any;
@Component({
  selector: 'order-seller',
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

  orderHistoryInput: Order = new Order();
  displayDialogHistory: boolean = false;

  visibleShipperLstDialog: boolean = false;
  lstShipper: Shipper[] = [];
  selectedShipper!: Shipper;
  loadingShipperLst: boolean;
  orderPreparingSelected: Order = new Order();

  rangeDates: Date[] | undefined;
  currentDate: Date = new Date();
  isDisableCalendar: boolean = true;
  disabledIds = ['0', '1', '2', '3'];
  displayMap : boolean = false;
  label1: string;
  label2: string;
  label3: string;
  label4: string;
  label5: string;
  label6: string;
  label7: string;

  visibleMapBox: boolean = false;

  constructor(
    public breadcrumbService: AppBreadcrumbService,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private datePipe: DatePipe,
    private signalRService: DataRealTimeService,
    public translate: TranslateService
  ) {
    super(messageService);
    this.rangeDates = [];
    this.rangeDates[0] = this.rangeDates[1] = new Date();

    this.items = [
      { label:  "Requested", id: '0'},
      { label:  "Preparing", id: '1'},
      { label:  "Wait Shipper", id: '2'},
      { label:  "Shipping", id: '3'},
      { label:  "InCompleted", id: '4'},
      { label:  "Completed", id: '5'},
      { label:  "Cancel", id: '6'},
    ];

    this.activeItem = this.items[0];

    
  }

  ngOnInit() {
    this.initTabMenuitem();
    
    this.getAllOrders();
    
    this.connectSignalR();
    
    this.showCurrentPageReport = true;
  }

  async connectSignalR() {
    this.lstOrders = [];
    this.signalRService.startConnection();
    const res = await this.signalRService.addTransferDataListener(
      'orderSellerRealTime',
      API.PHAN_HE.ORDER,
      API.API_ORDER.GET_ORDER_BY_STATUS,
      this.orderParamInput,
      false
    );
    if (res && res.message === 'Success') {
      this.lstOrders = res.orders;
    }
  }

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any): void {
    this.signalRService.stopConnection();
  }

  ngAfterViewInit() {
    // const tabmenuitem = this.elementRef.nativeElement.querySelector('.p-tabmenuitem');
    // const tabmenu_ink_bar = this.elementRef.nativeElement.querySelector('.p-tabmenu-ink-bar');
    // this.renderer.setStyle(tabmenu_ink_bar, 'width', tabmenuitem.offsetWidth);
    //console.log('Width:', width);
  }

  initTabMenuitem() {
    this.translate.get('orderSellerScreen').subscribe( (text: any) => {
      this.label1 = text.request;
      this.label2 = text.preparing;
      this.label3 = text.waitShipper;
      this.label4 = text.shipping;
      this.label5 = text.completed;
      this.label6 = text.incompleted;
      this.label7 = text.cancel;

      this.items = [
        { label:  this.label1, id: '0'},
        { label:  this.label2, id: '1'},
        { label:  this.label3, id: '2'},
        { label:  this.label4, id: '3'},
        { label:  this.label5, id: '4'},
        { label:  this.label6, id: '5'},
        { label:  this.label7, id: '6'},
      ];

      this.activeItem = this.items[0];
    });
    

    
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
        this.activeItem != undefined ? parseInt(this.activeItem.id) : 0;

      let response = await this.iServiceBase.postDataAsync(
        API.PHAN_HE.ORDER,
        API.API_ORDER.GET_ORDER_BY_STATUS,
        this.orderParamInput
      );

      if (response && response.message === 'Success') {
        this.lstOrders = response.orders;
        console.log("day", this.lstOrders[0].orderId);
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
    let fromDate = this.datePipe.transform(this.rangeDates[0], "yyyy-MM-dd");
    let endDate = this.datePipe.transform(this.rangeDates[1], "yyyy-MM-dd");
    if(!endDate){
      this.rangeDates[1] = this.rangeDates[0];
      endDate = fromDate;
    }

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
         document.body.style.cursor = 'wait';
         //confirm action
        this.commissionExternalShipper(order);
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

  
  async commissionExternalShipper(order: Order) {
    const param: OrderExternalInput = new OrderExternalInput(
      order.orderId
    );
    let response = await this.iServiceBase.postDataAsync(
      API.PHAN_HE.ORDER,
      API.API_ORDER.EXTERNAL_ORDER,
      param,
      true
    );

    if (response && response.message === 'Success') {
      this.showMessage(
        mType.success,
        'Notification',
        `Commission external shipper to orderId: ${order.orderId} successfully`,
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

  onHistory(order: Order, event) {
    this.headerDialog = 'Order History';

    this.orderHistoryInput = new Order();
    this.orderHistoryInput = order;

    this.displayDialogHistory = true;
  }

  onClickMap(order: Order){
    this.visibleMapBox = true;
    this.onOpenMap(order.shipAddress)
  }
  
  onOpenMap(address : string){
    
    this.displayMap = true;
    //this.geocodeAddress(address);
  }

  async geocodeAddress(address : string) {
    const param = {
      "address": address
    }
    const response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_USER.MAP, param);
    const location = response.results[0].geometry.location;
    this.initMap(location.lat, location.lng);

  }
  locationResult: any;
  getLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          const latitude = position.coords.latitude;
          const longitude = position.coords.longitude;

          this.locationResult = `Latitude: ${latitude}, Longitude: ${longitude}`;
        },
        (error) => {
          this.locationResult = `Error getting location: ${error.message}`;
        }
      );
    } else {
      this.locationResult = 'Geolocation is not supported by this browser.';
    }
  }
  initMap(latitude: number, longitude: number) {
    
    const map = new google.maps.Map(document.getElementById('map'), {
      center: { lat: latitude, lng: longitude },
      zoom: 12
    });

    const marker = new google.maps.Marker({
      position: { lat: latitude, lng: longitude },
      map: map,
      title: 'Specific Address'
    });
    /// chỉ đường
    const directionsService = new google.maps.DirectionsService();
    const directionsRenderer = new google.maps.DirectionsRenderer({ map: map });

    // Lấy vị trí hiện tại của người dùng
    navigator.geolocation.getCurrentPosition(
      (position) => {
        const userLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);

        // Đặt các tham số để lấy chỉ đường từ vị trí hiện tại đến điểm cần đến
        const request = {
          origin: userLocation,
          destination: { lat: latitude, lng: longitude },
          travelMode: google.maps.TravelMode.DRIVING
        };

        // Gửi yêu cầu chỉ đường
        directionsService.route(request, (response, status) => {
          if (status === google.maps.DirectionsStatus.OK) {
            // Hiển thị chỉ đường trên bản đồ
            directionsRenderer.setDirections(response);
          } else {
            console.error('Error fetching directions:', status);
          }
        });
      },
      (error) => {
        console.error('Error getting user location:', error);
      }
    );
  }

  getSeverity(status: string) {
    switch (status) {
      case 'External':
        return 'success';
      case 'Internal':
        return 'secondary';
      default:
        return 'error';
    }
  }
}
