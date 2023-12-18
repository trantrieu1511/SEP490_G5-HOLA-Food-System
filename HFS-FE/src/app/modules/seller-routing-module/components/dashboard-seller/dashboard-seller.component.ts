import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  iComponentBase,
  iServiceBase,
  mType,
} from 'src/app/modules/shared-module/shared-module';
import * as API from '../../../../services/apiURL';
import { MessageService } from 'primeng/api';
import { DashboardSeller } from '../../models/dashboard-seller.model';
import { ColorLineChart } from 'src/app/utils/colorLineChart';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'dashboard-seller',
  templateUrl: './dashboard-seller.component.html',
  styleUrls: ['./dashboard-seller.component.scss']
})
export class DashboardSellerComponent extends iComponentBase implements OnInit{
  dataDashboard: DashboardSeller = new DashboardSeller();
  dataDashboardDisplay: any;

  dataDashboardPie: any;

  options: any;
  optionsPie: any;

  rangeDates: Date[] | undefined;
  currentDate: Date = new Date();
  isDisplayChart: boolean = false;

  chooseDisplayLine: any = 0;

  Order:string;
  Amount:string;
  Sold:string;

  constructor(
    private datePipe: DatePipe,
    public messageService: MessageService,
    private iServiceBase: iServiceBase,
    public breadcrumbService: AppBreadcrumbService,
    public translate: TranslateService
  ){
    super(messageService, breadcrumbService);
    this.rangeDates = [];
    this.rangeDates[0] = this.rangeDates[1] = new Date();

    this.breadcrumbService.setItems([
      {label: 'HFSBusiness'},
      {label: 'Dashboard', routerLink: ['/HFSBusiness/seller/dashboard']}
    ]);

    this.translate.get('CusAdminScreen').subscribe( (text: any) => {
      this.Order = text.Order; 
      this.Amount = text.Amount;  
      this.Sold = text.Sold;   
    });
  }


  ngOnInit() {
    this.getAllData();

    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
    const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

    // this.data = {
    //   labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
    //   datasets: [
    //     {
    //       label: 'First Dataset',
    //       data: [65, 59, 80, 81, 56, 55, 40],
    //       fill: false,
    //       borderColor: documentStyle.getPropertyValue('--blue-400'),
    //       tension: 0.4
    //     },
    //     {
    //       label: 'Second Dataset',
    //       data: [28, 48, 40, 19, 86, 27, 90],
    //       fill: false,
    //       borderColor: documentStyle.getPropertyValue('--yellow-400'),
    //       //--yellow-400 --pink-400
    //       tension: 0.4
    //     }
    //   ]
    // }

    this.options = {
        maintainAspectRatio: false,
        aspectRatio: 1.2,
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
                    color: surfaceBorder,
                    drawBorder: false
                }
            },
            y: {
                ticks: {
                    color: textColorSecondary
                },
                grid: {
                    color: surfaceBorder,
                    drawBorder: false
                }
            }
        }
    };

    this.optionsPie = {
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

  async getAllData(){
    let param: {
      dateFrom: string,
      dateEnd: string
    } = {
      dateFrom: "",
      dateEnd: ""
    };

    if(this.rangeDates){
      param.dateFrom = this.datePipe.transform(this.rangeDates[0], "yyyy-MM-dd");
      param.dateEnd = this.datePipe.transform(this.rangeDates[1], "yyyy-MM-dd");
    }else{
      param.dateFrom = param.dateEnd = this.datePipe.transform(new Date(), "yyyy-MM-dd");
    }

    let response = await this.iServiceBase.getDataWithParamsAsync(
      API.PHAN_HE.DASHBOARD,
      API.API_DASHBOARD.DASHBOARD_SELLER,
      param,
      false
    );

    if (response && response.success) {
      this.dataDashboard = response;
      if(this.dataDashboard.datasets){

        const documentStyle = getComputedStyle(document.documentElement);
        this.dataDashboard.datasets = this.dataDashboard.datasets.map((element, index) => {
          element.borderColor = documentStyle.getPropertyValue(ColorLineChart[index]);
          element.fill = false;
          element.tension = 0,4;
          return element;
        });

        this.dataDashboardDisplay = {
          labels: this.dataDashboard.labels,
          datasets: [this.dataDashboard.datasets[this.chooseDisplayLine]] 
        };

        //this.mapToPie(documentStyle);

      }

    }
  }

  onCloseCalendar(event: any){
    let fromDate = this.datePipe.transform(this.rangeDates[0], "yyyy-MM-dd");
    let endDate = this.datePipe.transform(this.rangeDates[1], "yyyy-MM-dd");
    if(!endDate){
      this.rangeDates[1] = this.rangeDates[0];
      endDate = fromDate;
    }
    this.isDisplayChart = fromDate != endDate ? true : false;
    this.getAllData();
  }

  mapToPie(documentStyle: any){
  
    // this.dataDashboard.datasets.forEach((element, index) => {
    //   var temp = {
    //     data: element.data,
    //     backgroundColor: [documentStyle.getPropertyValue('--blue-500'), documentStyle.getPropertyValue('--yellow-500'), documentStyle.getPropertyValue('--green-500')],
    //     hoverBackgroundColor: [documentStyle.getPropertyValue('--blue-400'), documentStyle.getPropertyValue('--yellow-400'), documentStyle.getPropertyValue('--green-400')]
    //   }
    //   return temp;
    // })

    this.dataDashboardPie = {
      
      labels: [this.Order, this.Amount, this.Sold],
      datasets: [
          {
              data: [this.dataDashboard.orderCount, this.dataDashboard.amountCount, this.dataDashboard.soldCount],
              backgroundColor: [documentStyle.getPropertyValue('--blue-500'), documentStyle.getPropertyValue('--yellow-500'), documentStyle.getPropertyValue('--green-500')],
              hoverBackgroundColor: [documentStyle.getPropertyValue('--blue-400'), documentStyle.getPropertyValue('--yellow-400'), documentStyle.getPropertyValue('--green-400')]
          }
      ]
    };
  }

  chooseLine(type: number){
    this.chooseDisplayLine = type;

    switch (type) {
      case 0:
          this.dataDashboardDisplay = {
            labels: this.dataDashboard.labels,
            datasets: [this.dataDashboard.datasets[0]] 
          };
        break;
      case 1:
        this.dataDashboardDisplay = {
          labels: this.dataDashboard.labels,
          datasets: [this.dataDashboard.datasets[1]] 
        };
        break;
      case 2:
        this.dataDashboardDisplay = {
          labels: this.dataDashboard.labels,
          datasets: [this.dataDashboard.datasets[2]] 
        };
        break;
      default:
        break;
    }
  }
}
