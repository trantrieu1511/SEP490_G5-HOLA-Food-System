import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { iComponentBase, iServiceBase, mType } from 'src/app/modules/shared-module/shared-module';
import * as API from '../../../../services/apiURL';
@Component({
  selector: 'app-paymentverify',
  templateUrl: './paymentverify.component.html',
  styleUrls: ['./paymentverify.component.scss']
})
export class PaymentverifySellerComponent extends iComponentBase
  implements OnInit {
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
  tranId: string;
  value: number;
  bankcode: string;
  status: boolean
  resultText: string

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.queryParams = params;
      console.log(this.queryParams);
    })
    this.onPaymentVerify();
  }


  async onPaymentVerify() {
    try {
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.PAYMENT, API.API_PAYMENT.VERIFY, this.queryParams);
      if (response.success) {
        console.log(response)
        this.tranId = response.vnPayTranID
        this.value = response.value
        this.bankcode = response.bankCode
        this.status = response.success
        this.resultText = "Payment success!"
        this.showMessage(mType.success, response.message, response.message, 'notify');
      }
      else {
        this.status = response.success
        this.resultText = "Payment Fail!"
        var messageError = this.iServiceBase.formatMessageError(response);
        this.showMessage(mType.error, response.message, messageError, 'notify');
      }
    } catch (e) {
      console.log(e);
    }
  }

  onBackToWallet() {
    this.router.navigate(['/HFSBusiness/seller/wallet']);
  }
}
