import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Injectable()
export class MenuDataService {

  dashboard: string
  notification: string
  orderManagement: string


  constructor(
    public translate: TranslateService,
    private http: HttpClient
    ){
    // this.translate.get('menuLabel').subscribe( (text: any) => {
    //   this.dashboard = text.dashboard

    // });
  }

  getJsonData(lang: any){

    if(lang == 'en'){
      return this.http.get('assets/config/enMenuData.json')
    }

    return this.http.get('assets/config/vnMenuData.json')
  }

  menusSeller() {
    return [
      {
        label: 'Menu',
        icon: 'pi pi-fw pi-home',
        items: [
          {
            label: 'Dashboard',
            icon: 'pi pi-fw pi-chart-line',
            routerLink: ['/HFSBusiness/seller/dashboard'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Notification',
            icon: 'pi pi-fw pi-bell',
            routerLink: ['/HFSBusiness/notify-management'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Order Management',
            icon: 'pi pi-fw pi-book',
            routerLink: ['/HFSBusiness/order-management'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Food/Drink Management',
            icon: 'pi pi-fw pi-list',
            routerLink: ['/HFSBusiness/menu-management'],
            badgeClass: 'p-badge-success',
          },
          {
            label: 'Post Management',
            icon: 'pi pi-fw pi-list',
            routerLink: ['/HFSBusiness/post-management'],
            badgeClass: 'p-badge-success',
          },
          {
            label: 'Shipper Management',
            icon: 'pi pi-fw pi-users',
            routerLink: ['/HFSBusiness/seller/shipper-management'],
            badgeClass: 'p-badge-success',
          },
          {
            label: 'Voucher Management',
            icon: 'pi pi-fw pi-ticket',
            routerLink: ['/HFSBusiness/seller/voucher-management'],
            badgeClass: 'p-badge-success',
          },

          {
            label: 'Wallet Management',
            icon: 'pi pi-fw pi-dollar',
            routerLink: ['/HFSBusiness/seller/wallet'],
            badgeClass: 'p-badge-success',
          },
          {
            label: 'Feedback Management',
            icon: 'pi pi-fw pi-comments',
            routerLink: ['/HFSBusiness/seller/reply'],
            badgeClass: 'p-badge-success',
          },
          // {
          //     label: 'Submenu 1', icon: 'pi pi-fw pi-align-left',
          //     items: [
          //         {
          //             label: 'Submenu 1.1', icon: 'pi pi-fw pi-align-left',
          //             items: [
          //                 {label: 'Submenu 1.1.1', icon: 'pi pi-fw pi-align-left'},
          //                 {label: 'Submenu 1.1.2', icon: 'pi pi-fw pi-align-left'},
          //                 {label: 'Submenu 1.1.3', icon: 'pi pi-fw pi-align-left'},
          //             ]
          //         },
          //         {
          //             label: 'Submenu 1.2', icon: 'pi pi-fw pi-align-left',
          //             items: [
          //                 {label: 'Submenu 1.2.1', icon: 'pi pi-fw pi-align-left'}
          //             ]
          //         },
          //     ]
          // },
        ],
      },
    ];
  }

  menusAdmin() {
    return [
      {
        label: 'Menu',
        icon: 'pi pi-fw pi-home',
        items: [
          {
            label: 'Dashboard',
            icon: 'pi pi-fw pi-chart-line',
            routerLink: ['/HFSBusiness/admin/dashboard'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Notification',
            icon: 'pi pi-fw pi-bell',
            routerLink: ['/HFSBusiness/notify-management'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Customer Management',
            icon: 'pi pi-fw pi-users',
            routerLink: ['/HFSBusiness/admin/customer-management'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Seller Management',
            icon: 'pi pi-fw pi-users',
            routerLink: ['/HFSBusiness/admin/seller-management'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Category Management',
            icon: 'pi pi-chevron-circle-right',
            routerLink: ['/HFSBusiness/admin/category-management'],
            badgeClass: 'p-badge-info',
          }
          , {
            label: 'Shipper Management',
            icon: 'pi pi-fw pi-truck',
            routerLink: ['/HFSBusiness/admin/shipper-management'],
            badgeClass: 'p-badge-info',
          }
          , {
            label: 'Post Moderator Management',
            icon: 'pi pi-fw pi-users',
            routerLink: ['/HFSBusiness/admin/post-moderator'],
            badgeClass: 'p-badge-info',
          }
          , {
            label: 'Menu Moderator Management',
            icon: 'pi pi-fw pi-users',
            routerLink: ['/HFSBusiness/admin/menu-moderator'],
            badgeClass: 'p-badge-info',
          }

          ,  {
            label: 'Accountant Management',
            icon: 'pi pi-fw pi-users',
            routerLink: ['/HFSBusiness/admin/accountant'],
            badgeClass: 'p-badge-info',
          }

          ,{
            label: 'Report Seller Management',
            icon: 'pi pi-fw pi-align-justify',
            class:'fa-solid fa-truck-fast',
            routerLink: ['/HFSBusiness/admin/report'],
            badgeClass: 'p-badge-info',
          }

        ],
      },
    ];
  }

  menusShipper() {
    return [
      {
        label: 'Menu',
        icon: 'pi pi-fw pi-home',
        items: [
          {
            label: 'Dashboard',
            icon: 'pi pi-fw pi-chart-line',
            routerLink: ['/HFSBusiness/shipper/dashboad'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Notification',
            icon: 'pi pi-fw pi-bell',
            routerLink: ['/HFSBusiness/notify-management'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Order Management',
            icon: 'pi pi-fw pi-book',
            routerLink: ['/HFSBusiness/order-management'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'History Management',
            icon: 'pi pi-fw pi-history',
            routerLink: ['/HFSBusiness/shipper/history'],
            badgeClass: 'p-badge-info',
          },
        ],
      },
    ];
  }

  menusPostModerator() {
    return [
      {
        label: 'Menu',
        icon: 'pi pi-fw pi-home',
        items: [
          {
            label: 'Dashboard',
            icon: 'pi pi-fw pi-chart-line',
            routerLink: ['/HFSBusiness/postmoderator/dashboard'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Notification',
            icon: 'pi pi-fw pi-bell',
            routerLink: ['/HFSBusiness/notify-management'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Post Management',
            icon: 'pi pi-fw pi-list',
            routerLink: ['/HFSBusiness/post-management'],
            badgeClass: 'p-badge-success',
          },
          {
            label: 'Post Report Management',
            icon: 'pi pi-fw pi-list',
            routerLink: ['/HFSBusiness/postreport-management'],
            badgeClass: 'p-badge-success',
          },
        ],
      },
    ];
  }

  menusMenuModerator() {
    return [
      {
        label: 'Menu',
        icon: 'pi pi-fw pi-home',
        items: [
          {
            label: 'Dashboard',
            icon: 'pi pi-fw pi-chart-line',
            routerLink: ['/HFSBusiness/menumoderator/dashboard'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Notification',
            icon: 'pi pi-fw pi-bell',
            routerLink: ['/HFSBusiness/notify-management'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Menu Management',
            icon: 'pi pi-fw pi-list',
            routerLink: ['/HFSBusiness/menu-management'],
            badgeClass: 'p-badge-success',
          },
          {
            label: 'Menu Report Management',
            icon: 'pi pi-fw pi-list',
            routerLink: ['/HFSBusiness/menureport-management'],
            badgeClass: 'p-badge-success',
          },
        ],
      },
    ];
  }

  menuAccountant() {
    return [
      {
        label: 'Menu',
        icon: 'pi pi-fw pi-home',
        items: [
          {
            label: 'Dashboard',
            icon: 'pi pi-fw pi-chart-line',
            routerLink: ['/HFSBusiness/accountant/dashboard'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Withdraw Request',
            icon: 'pi pi-fw pi-bell',
            routerLink: ['/HFSBusiness/accountant/withdraw-request'],
            badgeClass: 'p-badge-info',
          }
        ],
      },
    ];
  }

  getMenusSeller() {
    return Promise.resolve(this.menusSeller());
  }
}
