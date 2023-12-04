import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { iComponentBase, iServiceBase, mType } from 'src/app/modules/shared-module/shared-module';
import * as API from '../../../../services/apiURL';
import { ActivatedRoute, Router } from '@angular/router';
import { TabViewChangeEvent } from 'primeng/tabview';
import { async } from 'rxjs';
import { GetTransactionHistoryInput } from '../../models/GetTransactionHistoryInput.model';
import { PaymentInput } from '../../models/PaymentInput.model';
import { VerifyCodeInput } from 'src/app/modules/customer-routing-module/models/VerifyCodeInput.model';
import { CreateWithDrawRequestInput } from '../../models/CreateWithDrawRequestInput.model';

@Component({
  selector: 'app-wallet',
  templateUrl: './wallet.component.html',
  styleUrls: ['./wallet.component.scss']
})
export class WalletComponentSeller
  extends iComponentBase
  implements OnInit {
  transactionHistory: any
  transactionHistoryInput: GetTransactionHistoryInput = new GetTransactionHistoryInput()
  rangeDates: Date[] | undefined;
  currentDate: Date = new Date();
  value: number = 0;
  DepositDialog = false
  balance: number = 0
  paymentInput: PaymentInput = new PaymentInput();
  displayTransaction: any
  activeTabIndex: number = 0;
  sellerId : string;
  TransferDialog : boolean = false;
  tranferInput : VerifyCodeInput = new VerifyCodeInput();
  withDrawRequestDialog : boolean = false
  withDrawRequestInput : CreateWithDrawRequestInput = new CreateWithDrawRequestInput();
  tabs: any = [
    { label: 'All', id: '0' },
    { label: 'Waiting', id: '1' },
    { label: 'Success', id: '2' },
    { label: 'Cancel', id: '3' },
    { label: 'Money In', id: '4' },
    { label: 'Money Out', id: '5' },
  ];
  constructor(
    public breadcrumbService: AppBreadcrumbService,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private datePipe: DatePipe,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    super(messageService);
  }

  ngOnInit(): void {
    this.onGetBalance();
    this.setDefaultDate();
    this.onGetHistory();
  }
  setDefaultDate() {
    this.rangeDates = [];
    this.rangeDates[0] = new Date(this.currentDate.getFullYear(), this.currentDate.getMonth(), 1);
    this.rangeDates[1] = new Date();
  }
  OnSaveDeposit() {
    this.DepositDialog = false;
    this.onRedirectUrl();
  }

  onOpenDeposit() {
    this.DepositDialog = true;
  }

  async onRedirectUrl() {
    try {
      this.paymentInput.value = this.value
      this.paymentInput.transactionType = 0;
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.PAYMENT, API.API_PAYMENT.GET_URL, this.paymentInput);
      if (response) {
        window.location.href = response.text;
      }
      else {
        //this.router.navigate([response]);
      }
    } catch (e) {
      console.log(e);
    }
  }

  async onGetBalance() {
    try {
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.WALLET, API.API_WALLET.GET_BAlANCE, null);
      if (response.success) {
        this.balance = response.balance
        this.sellerId = response.userId
      }
      else {
        //this.router.navigate([response]);
      }
    } catch (e) {
      console.log(e);
    }
  }

  onChangeTab(activeTab: TabViewChangeEvent) {
    switch (activeTab.index) {
      case 0:
        this.displayTransaction = this.transactionHistory;
        break;
      case 1:
        this.displayTransaction = this.transactionHistory.filter(x => x.status === 0);
        break;
      case 2:
        this.displayTransaction = this.transactionHistory.filter(x => x.status === 1);
        break;
      case 3:
        this.displayTransaction = this.transactionHistory.filter(x => x.status === 2);
        break;
      case 4:
        this.displayTransaction = this.transactionHistory
        .filter(x => x.transactionType === "OrderPaid" 
        || x.transactionType === "Deposit"
        || (x.transactionType === "Transfer" && x.recieverId === this.sellerId));
        break;
      case 5:
        this.displayTransaction = this.transactionHistory
        .filter(x => (x.transactionType === "Transfer" && x.senderId === this.sellerId)
        || x.transactionType === "Withdraw");
        break;
    }

  }

  async onGetHistory() {
    try {
      this.transactionHistoryInput.dateFrom = this.datePipe.transform(this.rangeDates[0], "yyyy-MM-dd");
      this.transactionHistoryInput.dateTo = this.datePipe.transform(this.rangeDates[1], "yyyy-MM-dd");
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.WALLET, API.API_WALLET.HISTORY, this.transactionHistoryInput);
      if (response.success) {
        this.transactionHistory = response.listTransactions
        this.displayTransaction = this.transactionHistory
      }
      else {
        //this.router.navigate([response]);
      }
    } catch (e) {
      console.log(e);
    }
  }

  async onSendCode() {
    try {
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.WALLET, API.API_WALLET.SEND_CODE, null);
      if (response.success) {
        this.showMessage(mType.success, "Notification", "Please check your code in your email!", 'notify');
      }
      else {
        this.showMessage(mType.warn, "Notification", response.message, 'notify');
      }
    } catch (e) {
      console.log(e);
    }
  }

  async OnSaveTransfer() {
    try {
      if (this.tranferInput.code.trim().length === 0){
        this.showMessage(mType.warn, "Notification", "Code is required! Send code and check your mail!", 'notify');
        return
      }

      if (this.tranferInput.value === 0){
        this.showMessage(mType.warn, "Notification", "Value must > 0", 'notify');
        return
      }

      if (this.tranferInput.value > this.balance){
        this.showMessage(mType.warn, "Notification", "Your balance not enough!", 'notify');
        return
      }

      if (this.tranferInput.recievierId.trim().length === 0){
        this.showMessage(mType.warn, "Notification", "RecievierId is required!", 'notify');
        return
      }

      
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.WALLET, API.API_WALLET.VERIFY, this.tranferInput);
      if (response.success) {
        this.showMessage(mType.success, "Notification", "Transfer success!", 'notify');
        this.TransferDialog = false;
        this.onGetHistory();
        this.onGetBalance();
      }
      else {
        this.showMessage(mType.warn, "Notification", response.message, 'notify');
      }
    } catch (e) {
      console.log(e);
    }
  }

  async onSendRequest() {
    try {
      debugger
      if (this.withDrawRequestInput.code.trim().length === 0){
        this.showMessage(mType.warn, "Notification", "Code is required! Send code and check your mail!", 'notify');
        return
      }

      if (this.withDrawRequestInput.value < 100000 ){
        this.showMessage(mType.warn, "Notification", "Value must > 100000", 'notify');
        return
      }

      if (this.withDrawRequestInput.value > this.balance){
        this.showMessage(mType.warn, "Notification", "Your balance not enough!", 'notify');
        return
      }
     
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.WALLET, API.API_WALLET.WITHDRAW, this.withDrawRequestInput);
      if (response.success) {
        this.showMessage(mType.success, "Notification", "Create Withdraw request success! Wait Accountant!", 'notify');
        this.withDrawRequestDialog = false;
        this.onGetHistory();
        this.onGetBalance();
      }
      else {
        this.showMessage(mType.warn, "Notification", response.message, 'notify');
      }
    } catch (e) {
      console.log(e);
    }
  }


  onCloseCalendar(event: any) {
    this.onGetHistory();
  }

  onOpenTransfer() {
    this.TransferDialog = true;
  }

  openWithDrawRequest() {
    this.withDrawRequestDialog = true;
  }
}
