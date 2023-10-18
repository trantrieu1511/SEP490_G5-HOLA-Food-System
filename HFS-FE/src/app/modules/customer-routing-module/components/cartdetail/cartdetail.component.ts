import {Component, OnInit, ViewChild} from '@angular/core';
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
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from 'src/app/services/data.service';
import { CartItem } from '../../models/CartItem.model';
import { CheckboxModule } from 'primeng/checkbox';

@Component({
  selector: 'app-cartdetail',
  templateUrl: './cartdetail.component.html',
  styleUrls: ['./cartdetail.component.scss']
})

export class CartdetailComponent extends iComponentBase implements OnInit{

  loading: boolean;
  items : CartItem[]
  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private route: ActivatedRoute,
    private router: Router,
    private dataService: DataService
  ){
    super(messageService);	
  }

      ngOnInit(){
        this.getCartItem();
      }

      async getCartItem(){
        try {
          this.loading = true;
          let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.CART, API.API_CART.CART_DETAIL, null);
          if (response && response.message === "Success") {
              this.items = response.listItem
              this.calculate();
          }
          else{
            this.router.navigate(['/login']);
          }
    
          this.loading = false;
      } catch (e) {
          console.log(e);
          this.loading = false;
      }
      }

      calculate(){
        this.items.forEach(element => {
                element.totalPrice = element.unitPrice * element.amount
              });
      }
}
