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

@Component({
    selector: 'app-dashboard-postmod',
    templateUrl: './dashboard-postmod.component.html',
    styleUrls: ['./dashboard-postmod.component.scss']
})
export class DashboardPostmodComponent extends iComponentBase implements OnInit {

    data: any;
    options: any;
    allTimeStatistics: DashboardPostModStatistic = new DashboardPostModStatistic();
    thisMonthStatistics: DashboardPostModStatistic = new DashboardPostModStatistic();

    constructor(
        private shareData: ShareData,
        public messageService: MessageService,
        private confirmationService: ConfirmationService,
        private iServiceBase: iServiceBase,
        public router: Router,
        public authService: AuthService,
    ) {
        super(messageService);
    }

    async ngOnInit() {
        this.configChart();
        await this.getAllTimeStatistics();
        await this.getThisMonthStatistics();
    }
    
    async getThisMonthStatistics() {
        try {
            let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.DASHBOARD, API.API_DASHBOARD.DASHBOARD_POSTMOD_THISMONTHSTATISTICS);

            if (response && response.message === "Success") {
                this.thisMonthStatistics = response.statistics;
                console.log(this.thisMonthStatistics);
            } else {
                console.log(response);
            }
        } catch (e) {
            console.log(e);
        }
    }

    async getAllTimeStatistics() {
        try {
            let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.DASHBOARD, API.API_DASHBOARD.DASHBOARD_POSTMOD_ALLTIMESTATISTICS);

            if (response && response.message === "Success") {
                this.allTimeStatistics = response.statistics;
                console.log(this.allTimeStatistics);
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

        this.data = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
            datasets: [
                {
                    label: 'First Dataset',
                    data: [65, 59, 80, 81, 56, 55, 40],
                    fill: false,
                    borderColor: documentStyle.getPropertyValue('--blue-500'),
                    tension: 0.4
                },
                {
                    label: 'Second Dataset',
                    data: [28, 48, 40, 19, 86, 27, 90],
                    fill: false,
                    borderColor: documentStyle.getPropertyValue('--pink-500'),
                    tension: 0.4
                }
            ]
        };

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

}
