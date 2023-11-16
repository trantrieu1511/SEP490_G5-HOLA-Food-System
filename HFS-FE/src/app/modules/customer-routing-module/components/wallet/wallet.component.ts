import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { iComponentBase, iServiceBase } from 'src/app/modules/shared-module/shared-module';
import { PaymentInput } from '../../models/PaymentInput.model';
import * as API from '../../../../services/apiURL';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-wallet',
  templateUrl: './wallet.component.html',
  styleUrls: ['./wallet.component.scss']
})
export class WalletComponent 
extends iComponentBase
implements OnInit{

  value : number = 0;
  DepositDialog = false
  paymentInput : PaymentInput = new PaymentInput();
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
    
  }

  OnSaveDeposit(){
    this.DepositDialog = false;
    this.onRedirectUrl();
  }

  onOpenDeposit(){
    this.DepositDialog = true;
  }
  
  async  onRedirectUrl(){
    try {
      this.paymentInput.value = this.value
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.PAYMENT, API.API_PAYMENT.GET_URL, this.paymentInput);
      debugger
      if (response) {
        window.location.href = response.text;
      }
      else{
          //this.router.navigate([response]);
      }
  } catch (e) {
      console.log(e);
  }
  }
}
