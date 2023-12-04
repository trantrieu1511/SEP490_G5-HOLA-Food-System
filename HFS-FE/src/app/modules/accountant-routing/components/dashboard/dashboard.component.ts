import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { iComponentBase, iServiceBase, mType } from 'src/app/modules/shared-module/shared-module';
import * as API from '../../../../services/apiURL';
import { ActivatedRoute, Router } from '@angular/router';
import { TabViewChangeEvent } from 'primeng/tabview';
import { async } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { GetTransactionHistoryInput } from 'src/app/modules/customer-routing-module/models/GetTransactionHistoryInput.model';
import { UpdateStatusWithdrawInput } from '../../models/UpdateStatusWithdrawInput.model';
import { DashboardSeller } from 'src/app/modules/seller-routing-module/models/dashboard-seller.model';
import { ColorLineChart } from 'src/app/utils/colorLineChart';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent
  extends iComponentBase
  implements OnInit {
  rangeDates: Date[] | undefined;
  currentDate: Date = new Date();
  dataline: any;
  optionsline: any;
  options: any;
  datatron: any;
  datatotal: any;
  optionstron: any;
  isDisplayChart: boolean = false;
  data: any;
  input: any;

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

  async ngOnInit() {
    this.setDefaultDate();
    await this.getAllData();
    this.bangtron();
    this.bangline();
  }

  setDefaultDate() {
    this.rangeDates = [];
    this.rangeDates[0] = new Date(this.currentDate.getFullYear(), this.currentDate.getMonth(), 1);
    this.rangeDates[1] = new Date();
  }

  async onCloseCalendar(event: any) {
    let fromDate = this.datePipe.transform(this.rangeDates[0], "yyyy-MM-dd");
    let endDate = this.datePipe.transform(this.rangeDates[1], "yyyy-MM-dd");
    this.isDisplayChart = fromDate != endDate ? true : false;
    await this.getAllData();
    this.bangtron();
    this.bangline();
  }

  async getAllData() {
    let dateList: string[] = [];
    let param: {
      dateFrom: string,
      dateEnd: string,
      dates: string[]
    } = {
      dateFrom: "",
      dateEnd: "",
      dates: []
    };
    if (this.rangeDates) {
      param.dateFrom = this.datePipe.transform(this.rangeDates[0], "yyyy-MM-dd");
      param.dateEnd = this.datePipe.transform(this.rangeDates[1], "yyyy-MM-dd");
    } else {
      param.dateFrom = param.dateEnd = this.datePipe.transform(new Date(), "yyyy-MM-dd");
    }
    for (let currentDate = new Date(param.dateFrom); currentDate <= new Date(param.dateEnd); currentDate.setDate(currentDate.getDate() + 1)) {

      let formattedDate = this.datePipe.transform(currentDate, 'yyyy-MM-dd');
      dateList.push(formattedDate);
    }
    param.dates = dateList;
    this.input = param;

    let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.WALLET, API.API_WALLET.DASH_BOARD, param);

    if (response && response.success) {
      this.data = response;
    }
  }

  async bangline() {
    debugger
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
    const surfaceBorder = documentStyle.getPropertyValue('--surface-border');
    
    this.dataline = {
      labels: this.input.dates,
      datasets: [
        {
          label: 'Money',
          data: this.data.moneyOuts,
          fill: false,
          tension: 0.4,
          borderColor: documentStyle.getPropertyValue('--blue-500')
        }
      ]
    };

    this.optionsline = {
      maintainAspectRatio: false,
      aspectRatio: 0.6,
      plugins: {
        legend: {
          labels: {
            color: textColor
          }
        }
      },
      scales: {
        x: {
          ticks: {
            color: textColorSecondary
          },
          grid: {
            color: surfaceBorder
          }
        },
        y: {
          ticks: {
            color: textColorSecondary
          },
          grid: {
            color: surfaceBorder
          }
        }
      }
    };
  }

  bangtron() {
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');

    this.datatron = {
      labels: ['Waiting', 'Success', 'Reject'],
      datasets: [
        {
          data: [this.data.totalWaitingRequest, this.data.totalSuccessRequest, this.data.totalRejectRequest],
          backgroundColor: [documentStyle.getPropertyValue('--blue-500'), documentStyle.getPropertyValue('--yellow-500'), documentStyle.getPropertyValue('--green-500')],
          hoverBackgroundColor: [documentStyle.getPropertyValue('--blue-400'), documentStyle.getPropertyValue('--yellow-400'), documentStyle.getPropertyValue('--green-400')]
        }
      ]
    };

    this.options = {
      plugins: {
        legend: {
          labels: {
            usePointStyle: true,
            color: textColor
          }
        }
      }
    };
  }
}
