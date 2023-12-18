import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import {
    iComponentBase,
    iServiceBase, mType,
    ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../../services/apiURL";
import {
    ConfirmationService,
    LazyLoadEvent,
    MenuItem,
    MessageService,
    SelectItem,
    TreeNode
} from "primeng/api";
import { FileSelectEvent, FileUploadEvent } from 'primeng/fileupload';
import { Router } from '@angular/router';
import { AppCustomerTopBarComponent } from 'src/app/app-systems/app-topbar/customer/app.topbar-cus.component';
import { AuthService } from 'src/app/services/auth.service';
import { DashboardPostModStatistic } from '../../models/dashboard-postmod.model';
import { DatePipe } from '@angular/common';
import { ColorLineChart } from 'src/app/utils/colorLineChart';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-dashboard-postmod',
    templateUrl: './dashboard-postmod.component.html',
    styleUrls: ['./dashboard-postmod.component.scss']
})
export class DashboardPostmodComponent extends iComponentBase implements OnInit {

    myData: any;
    systemData: any;
    options: any;
    allTimeStatistics: DashboardPostModStatistic = new DashboardPostModStatistic();
    myStatistics: DashboardPostModStatistic = new DashboardPostModStatistic();
    // thisMonthStatistics: DashboardPostModStatistic = new DashboardPostModStatistic();

    systemCalendarRangeDates: Date[] | undefined;
    myCalendarRangeDates: Date[] | undefined;
    currentDate: Date = new Date();
    isDisplaySystemChart: boolean = false;
    isDisplayMyChart: boolean = false;
    Unbannote:string;
    Bannote:string;

    constructor(
        private datePipe: DatePipe,
        private shareData: ShareData,
        public messageService: MessageService,
        private confirmationService: ConfirmationService,
        private iServiceBase: iServiceBase,
        public router: Router,
        public authService: AuthService,
        public breadcrumbService: AppBreadcrumbService,
        public translate: TranslateService
    ) {
        super(messageService, breadcrumbService);

        this.breadcrumbService.setItems([
            {label: 'HFSBusiness'},
            {label: 'Dashboard', routerLink: ['/HFSBusiness/postmoderator/dashboard']}
        ]);

        this.translate.get('adminPostManagementScreen').subscribe( (text: any) => {
            this.Unbannote = text.Unbannote; 
            this.Bannote = text.Bannote; 
          });

        this.systemCalendarRangeDates = [];
        this.systemCalendarRangeDates[0] = this.systemCalendarRangeDates[1] = new Date();
        this.myCalendarRangeDates = [];
        this.myCalendarRangeDates[0] = this.myCalendarRangeDates[1] = new Date();
    }

    async ngOnInit() {
        await this.getSystemData();
        await this.getMyData();
        this.configChart();
        await this.getAllTimeStatistics();
        // await this.getThisMonthStatistics();
    }

    // async getThisMonthStatistics() {
    //     try {
    //         let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.DASHBOARD, API.API_DASHBOARD.DASHBOARD_POSTMOD_THISMONTHSTATISTICS);

    //         if (response && response.message === "Success") {
    //             this.thisMonthStatistics = response.statistics;
    //             console.log(this.thisMonthStatistics);
    //         } else {
    //             console.log(response);
    //         }
    //     } catch (e) {
    //         console.log(e);
    //     }
    // }

    async getAllTimeStatistics() {
        try {
            let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.DASHBOARD, API.API_DASHBOARD.DASHBOARD_POSTMOD_ALLTIMESTATISTICS);

            if (response && response.message === "Success") {
                this.allTimeStatistics = response.statistics;
                this.myStatistics = response.myStatistics;
                console.log(this.allTimeStatistics);
                console.log(this.myStatistics);
            } else {
                console.log(response);
            }
        } catch (e) {
            console.log(e);
        }
    }

    configChart() {
        const documentStyle = getComputedStyle(document.documentElement);
        const textColor = documentStyle.getPropertyValue('--text-color');
        const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
        const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

        

        this.options = {
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

    async getSystemData() {
        let param: {
            dateFrom: string,
            dateEnd: string
        } = {
            dateFrom: "",
            dateEnd: ""
        };

        if (this.systemCalendarRangeDates) {
            param.dateFrom = this.datePipe.transform(this.systemCalendarRangeDates[0], "yyyy-MM-dd");
            param.dateEnd = this.datePipe.transform(this.systemCalendarRangeDates[1], "yyyy-MM-dd");
        } else {
            param.dateFrom = param.dateEnd = this.datePipe.transform(new Date(), "yyyy-MM-dd");
        }

        let response = await this.iServiceBase.getDataWithParamsAsync(
            API.PHAN_HE.DASHBOARD,
            API.API_DASHBOARD.DASHBOARD_POSTMOD_SYSTEMSTATISTICS,
            param,
            false
        );

        if (response && response.success) {
            this.systemData = response;
            if (this.systemData.datasets) {

                const documentStyle = getComputedStyle(document.documentElement);
                this.systemData.datasets = this.systemData.datasets.map((element, index) => {
                    element.borderColor = documentStyle.getPropertyValue(ColorLineChart[index]);
                    element.fill = false;
                    element.tension = 0, 4;
                    return element;
                });
            }

            console.log(response);
            console.log("data", this.systemData);
        }
    }

    async getMyData() {
        let param: {
            dateFrom: string,
            dateEnd: string
        } = {
            dateFrom: "",
            dateEnd: ""
        };

        if (this.myCalendarRangeDates) {
            param.dateFrom = this.datePipe.transform(this.myCalendarRangeDates[0], "yyyy-MM-dd");
            param.dateEnd = this.datePipe.transform(this.myCalendarRangeDates[1], "yyyy-MM-dd");
        } else {
            param.dateFrom = param.dateEnd = this.datePipe.transform(new Date(), "yyyy-MM-dd");
        }

        let response = await this.iServiceBase.getDataWithParamsAsync(
            API.PHAN_HE.DASHBOARD,
            API.API_DASHBOARD.DASHBOARD_POSTMOD_MYSTATISTICS,
            param,
            false
        );

        if (response && response.success) {
            this.myData = response;
            if (this.myData.datasets) {

                const documentStyle = getComputedStyle(document.documentElement);
                this.myData.datasets = this.myData.datasets.map((element, index) => {
                    element.borderColor = documentStyle.getPropertyValue(ColorLineChart[index]);
                    element.fill = false;
                    element.tension = 0, 4;
                    return element;
                });
            }

            console.log(response);
            console.log("data", this.myData);
        }
    }

    onCloseCalendar(calendarType: number, event: any) {

        if (calendarType == 1) { // system data calendar
            let fromDate = this.datePipe.transform(this.systemCalendarRangeDates[0], "yyyy-MM-dd");
            let endDate = this.datePipe.transform(this.systemCalendarRangeDates[1], "yyyy-MM-dd");
            if (!endDate) {
                this.systemCalendarRangeDates[1] = this.systemCalendarRangeDates[0];
                endDate = fromDate;
            }
            this.getSystemData();
            this.isDisplaySystemChart = fromDate != endDate ? true : false;
        }
        if (calendarType == 2) { // my data calendar
            let fromDate = this.datePipe.transform(this.myCalendarRangeDates[0], "yyyy-MM-dd");
            let endDate = this.datePipe.transform(this.myCalendarRangeDates[1], "yyyy-MM-dd");
            if (!endDate) {
                this.myCalendarRangeDates[1] = this.myCalendarRangeDates[0];
                endDate = fromDate;
            }
            this.getMyData();
            this.isDisplayMyChart = fromDate != endDate ? true : false;
        }
    }

}
