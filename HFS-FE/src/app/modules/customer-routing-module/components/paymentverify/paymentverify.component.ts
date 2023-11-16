import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { iComponentBase, iServiceBase } from 'src/app/modules/shared-module/shared-module';

@Component({
  selector: 'app-paymentverify',
  templateUrl: './paymentverify.component.html',
  styleUrls: ['./paymentverify.component.scss']
})
export class PaymentverifyComponent extends iComponentBase
implements OnInit{
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

  queryParams: any;

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.queryParams = params;
      console.log(this.queryParams);
  })
}
}
