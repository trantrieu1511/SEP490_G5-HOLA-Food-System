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

  constructor(private elementRef: ElementRef, private renderer: Renderer2) { }

  ngOnInit(): void {
    this.items = [
      { label: 'Requested'},
      { label: 'Preparing'},
      { label: 'Shipping'},
      { label: 'Wait Shipper'},
      { label: 'Completed'},
      { label: 'InCompleted'},
      { label: 'Cancel'}
    ];

    this.activeItem = this.items[0];
  }

  ngAfterViewInit() {
    // const tabmenuitem = this.elementRef.nativeElement.querySelector('.p-tabmenuitem');
    // const tabmenu_ink_bar = this.elementRef.nativeElement.querySelector('.p-tabmenu-ink-bar');

    // this.renderer.setStyle(tabmenu_ink_bar, 'width', tabmenuitem.offsetWidth);

    //console.log('Width:', width);
  }

  onChangeTab(event: MenuItem){
    console.log(event.label);
  }

}
