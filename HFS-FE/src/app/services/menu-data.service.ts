import { Injectable } from '@angular/core';

@Injectable()
export class MenuDataService {
  menusSeller() {
    return [
      {
        label: 'Menu',
        icon: 'pi pi-fw pi-home',
        items: [
          {
            label: 'Dashboard',
            icon: 'pi pi-fw pi-home',
            routerLink: ['/HFSBusiness'],
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
            routerLink: ['/HFSBusiness/shipper-management'],
            badgeClass: 'p-badge-success',
          },
          {
            label: 'Voucher Management',
            icon: 'pi pi-fw pi-ticket',
            routerLink: ['/HFSBusiness/voucher-management'],
            badgeClass: 'p-badge-success',
          },

          {
            label: 'Wallet Management',
            icon: 'pi pi-fw pi-dollar',
            routerLink: ['/favorites/dashboardanalytics'],
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
            icon: 'pi pi-fw pi-home',
            routerLink: ['/HFSBusiness'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Notification',
            icon: 'pi pi-fw pi-bell',
            routerLink: ['/HFSBusiness/notify-management'],
            badgeClass: 'p-badge-info',
          },
          {
            label: 'Account Management',
            icon: 'pi pi-fw pi-users',
            routerLink: ['/HFSBusiness/seller/order-management'],
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
            icon: 'pi pi-fw pi-home',
            routerLink: ['/HFSBusiness'],
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
            icon: 'pi pi-fw pi-home',
            routerLink: ['/HFSBusiness'],
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
            icon: 'pi pi-fw pi-home',
            routerLink: ['/HFSBusiness'],
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

  getMenusSeller() {
    return Promise.resolve(this.menusSeller());
  }
}
