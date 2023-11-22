import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { iComponentBase, iServiceBase } from 'src/app/modules/shared-module/shared-module';
import { PaymentInput } from '../../models/PaymentInput.model';
import * as API from '../../../../services/apiURL';
import { ActivatedRoute, Router } from '@angular/router';
import { TabViewChangeEvent } from 'primeng/tabview';
import { async } from 'rxjs';
import { GetTransactionHistoryInput } from '../../models/GetTransactionHistoryInput.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-wallet',
  templateUrl: './wallet.component.html',
  styleUrls: ['./wallet.component.scss']
})
export class WalletComponent
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
    public authService: AuthService,
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
    this.rangeDates[0] = new Date(2022, this.currentDate.getMonth(), 1);
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
        this.displayTransaction = this.transactionHistory.filter(x => x.transactionType === "Deposit");
        break;
      case 5:
        this.displayTransaction = this.transactionHistory.filter(x => x.transactionType != "Deposit");
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

  onCloseCalendar(event: any) {
    this.onGetHistory();
  }
}
