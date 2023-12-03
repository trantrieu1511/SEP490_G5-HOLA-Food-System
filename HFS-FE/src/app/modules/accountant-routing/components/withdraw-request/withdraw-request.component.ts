import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { iComponentBase, iServiceBase, mType } from 'src/app/modules/shared-module/shared-module';
import * as API from '../../../../services/apiURL';
import { ActivatedRoute, Router } from '@angular/router';
import { TabViewChangeEvent } from 'primeng/tabview';
import { async } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { GetTransactionHistoryInput } from 'src/app/modules/customer-routing-module/models/GetTransactionHistoryInput.model';

@Component({
  selector: 'app-withdraw-request',
  templateUrl: './withdraw-request.component.html',
  styleUrls: ['./withdraw-request.component.scss']
})
export class WithdrawRequestComponent
  extends iComponentBase
  implements OnInit {
  transactionHistory: any
  transactionHistoryInput: GetTransactionHistoryInput = new GetTransactionHistoryInput()
  rangeDates: Date[] | undefined;
  currentDate: Date = new Date();
  displayTransaction: any
  tabs: any = [
    { label: 'All', id: '0' },
    { label: 'Waiting', id: '1' },
    { label: 'Success', id: '2' },
    { label: 'Cancel', id: '3' },
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
    this.setDefaultDate();
    this.onGetHistory();
  }
  setDefaultDate() {
    this.rangeDates = [];
    this.rangeDates[0] = new Date(this.currentDate.getFullYear(), this.currentDate.getMonth(), 1);
    this.rangeDates[1] = new Date();
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
    }
  }

  onCloseCalendar(event: any) {
    this.onGetHistory();
  } 
}
