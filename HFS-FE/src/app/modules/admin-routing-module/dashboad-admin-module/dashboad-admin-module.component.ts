import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  iComponentBase,
  iServiceBase,
  mType,
} from 'src/app/modules/shared-module/shared-module';
import * as API from '../../../services/apiURL';
import { MessageService } from 'primeng/api';

import { ColorLineChart } from 'src/app/utils/colorLineChart';
import { DashboardSeller } from '../../seller-routing-module/models/dashboard-seller.model';
@Component({
  selector: 'app-dashboad-admin-module',
  templateUrl: './dashboad-admin-module.component.html',
  styleUrls: ['./dashboad-admin-module.component.scss']
})
export class DashboadAdminModuleComponent extends iComponentBase implements OnInit {
  data: DashboardSeller;

  options: any;
  datatron: any;

  optionstron: any;
  rangeDates: Date[] | undefined;
  currentDate: Date = new Date();
  isDisplayChart: boolean = false;

  constructor(
    private datePipe: DatePipe,
    public messageService: MessageService,
    private iServiceBase: iServiceBase,
  ){
    super(messageService);
    this.rangeDates = [];
    this.rangeDates[0] = this.rangeDates[1] = new Date();
  }


  ngOnInit() {
    this.getAllData();
this.bangtron();
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
      this.data = response;
      if(this.data.datasets){
        const documentStyle = getComputedStyle(document.documentElement);
        this.data.datasets = this.data.datasets.map((element, index) => {
          element.borderColor = documentStyle.getPropertyValue(ColorLineChart[index]);
          element.fill = false;
          element.tension = 0,4;
          return element;
        });
      }

      console.log(response);
      console.log("data", this.data);
    }
  }

  onCloseCalendar(event: any){
    let fromDate = this.datePipe.transform(this.rangeDates[0], "yyyy-MM-dd");
    let endDate = this.datePipe.transform(this.rangeDates[1], "yyyy-MM-dd");
    this.isDisplayChart = fromDate != endDate ? true : false;
    this.getAllData();
  }

  async bangtron(){
    const documentStyle = getComputedStyle(document.documentElement);
        const textColor = documentStyle.getPropertyValue('--text-color');
        let dataFromApi=[];
        debugger
        try {
          dataFromApi = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_USER.DASHBOAD_ADMIN,"");
         ;
      } catch (e) {
          console.log(e);

      }
        this.datatron = {
            labels: dataFromApi.map(item => item.actor),
            datasets: [
                {
                    data: dataFromApi.map(item => item.total),
                    backgroundColor: ['blue', 'orange', 'green'],
                   hoverBackgroundColor: ['lightblue', 'lightyellow', 'lightgreen']
                }
            ]
        };

        this.optionstron = {
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