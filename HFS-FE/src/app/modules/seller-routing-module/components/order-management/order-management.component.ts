import { AfterViewInit, Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-order-management',
  templateUrl: './order-management.component.html',
  styleUrls: ['./order-management.component.scss']
})
export class OrderManagementComponent implements OnInit, AfterViewInit {
  items: MenuItem[] | undefined;

  activeItem: MenuItem | undefined;

  products: any[] = [];

  onActiveRequest: boolean = true;

  loading: boolean;

  showCurrentPageReport: boolean;

  constructor(private elementRef: ElementRef, private renderer: Renderer2) { }

  ngOnInit(): void {
    this.items = [
      { label: 'Requested', id: '0'},
      { label: 'Preparing', id: '1'},
      { label: 'Wait Shipper', id: '2'},
      { label: 'Shipping', id: '3'},
      { label: 'Completed', id: '4'},
      { label: 'InCompleted', id: '5'},
      { label: 'Cancel', id: '6'}
    ];

    this.activeItem = this.items[0];


    this.showCurrentPageReport = true;
    this.products = [{
        id: '1000',
        code: 'f230fh0g3',
        name: 'Bamboo Watch',
        description: 'Product Description',
        image: 'bamboo-watch.jpg',
        price: 65,
        category: 'Accessories',
        quantity: 24,
        inventoryStatus: 'INSTOCK',
        rating: 4,
        orders: [
            {
                id: '1000-0',
                productCode: 'f230fh0g3',
                date: '2020-09-13',
                amount: 65,
                quantity: 1,
                customer: 'David James',
                status: 'PENDING'
            },
        ]
    },];
  }

  ngAfterViewInit() {
    // const tabmenuitem = this.elementRef.nativeElement.querySelector('.p-tabmenuitem');
    // const tabmenu_ink_bar = this.elementRef.nativeElement.querySelector('.p-tabmenu-ink-bar');

    // this.renderer.setStyle(tabmenu_ink_bar, 'width', tabmenuitem.offsetWidth);

    //console.log('Width:', width);
  }

  onChangeTab(activeTab: MenuItem){
    this.activeItem = activeTab;
    this.onActiveRequest = false;
    console.log(this.activeItem.id);
  }

  
  getSeverity(status: string) {
    switch (status) {
        case 'INSTOCK':
            return 'success';
        case 'LOWSTOCK':
            return 'warning';
        case 'OUTOFSTOCK':
            return 'danger';
        default:
          return 'unknown';
    }
  }

  getStatusSeverity(status: string){
      switch (status) {
          case 'PENDING':
              return 'warning';
          case 'DELIVERED':
              return 'success';
          case 'CANCELLED':
              return 'danger';
          default:
            return 'unknown';
      }
  }

  onAccept(order: any){

  }

  onCancel(order: any, event){

  }

}
