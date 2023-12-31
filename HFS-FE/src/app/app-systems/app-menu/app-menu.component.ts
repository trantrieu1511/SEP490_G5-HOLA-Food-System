import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import {
    iComponentBase,
    iServiceBase, mType,
    ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../services/apiURL";


@Component({
  selector: 'app-menu',
  template: `
    <ul class="layout-menu">
      <li app-menuitem *ngFor="let item of model; let i = index;" [item]="item" [index]="i"
          [root]="true"></li>
    </ul>
  `,
})
export class AppMenuComponent implements OnInit {

  model: any[];

  constructor(
    private iServiceBase: iServiceBase,
    private jwtHelper: JwtHelperService
  ) {
    this.model = [];
  }

  ngOnInit(): void {
    // check role here
    // switch case and put data for model
    // each role has own model :
    // model được hiểu là menu bên trái nha


    //this.getRole();

    this.model = [
            {
                label: 'Favorites', icon: 'pi pi-fw pi-home',
                items: [
                    {
                        label: 'Order Management',
                        icon: 'pi pi-fw pi-home',
                        routerLink: ['/HFSBusiness/seller/order-management'],
                        badge: '4',
                        badgeClass: 'p-badge-info'
                    },
                    {
                        label: 'Dashboard Analytics',
                        icon: 'pi pi-fw pi-home',
                        routerLink: ['/favorites/dashboardanalytics'],
                        badge: '2',
                        badgeClass: 'p-badge-success'
                    }
                ]
            },
            {
                label: 'UI Kit', icon: 'pi pi-fw pi-star', routerLink: ['/uikit'],
                items: [
                    {
                        label: 'Input',
                        icon: 'pi pi-fw pi-check-square',
                        routerLink: ['/uikit/input'],
                        badge: '6',
                        badgeClass: 'p-badge-danger'
                    },
                    {
                        label: 'Float Label',
                        icon: 'pi pi-fw pi-bookmark',
                        routerLink: ['/uikit/floatlabel']
                    },
                    {
                        label: 'Invalid State',
                        icon: 'pi pi-fw pi-exclamation-circle',
                        routerLink: ['/uikit/invalidstate']
                    },
                    {
                        label: 'Button',
                        icon: 'pi pi-fw pi-mobile',
                        routerLink: ['/uikit/button'],
                        class: 'rotated-icon'
                    },
                    {
                        label: 'Table',
                        icon: 'pi pi-fw pi-table',
                        routerLink: ['/uikit/table'],
                        badge: '6',
                        badgeClass: 'p-badge-help'
                    },
                    {label: 'List', icon: 'pi pi-fw pi-list', routerLink: ['/uikit/list']},
                    {label: 'Tree', icon: 'pi pi-fw pi-share-alt', routerLink: ['/uikit/tree']},
                    {label: 'Panel', icon: 'pi pi-fw pi-tablet', routerLink: ['/uikit/panel']},
                    {label: 'Overlay', icon: 'pi pi-fw pi-clone', routerLink: ['/uikit/overlay']},
                    {label: 'Media', icon: 'pi pi-fw pi-image', routerLink: ['/uikit/media']},
                    {label: 'Menu', icon: 'pi pi-fw pi-bars', routerLink: ['/uikit/menu']},
                    {label: 'Message', icon: 'pi pi-fw pi-comment', routerLink: ['/uikit/message']},
                    {label: 'File', icon: 'pi pi-fw pi-file', routerLink: ['/uikit/file']},
                    {label: 'Chart', icon: 'pi pi-fw pi-chart-bar', routerLink: ['/uikit/charts']},
                    {label: 'Misc', icon: 'pi pi-fw pi-circle-off', routerLink: ['/uikit/misc']}
                ]
            },
            {
                label: 'Utilities', icon: 'pi pi-fw pi-compass', routerLink: ['utilities'],
                items: [
                    {
                        label: 'Form Layout',
                        icon: 'pi pi-fw pi-id-card',
                        routerLink: ['/uikit/formlayout'],
                        badge: '6',
                        badgeClass: 'p-badge-warning'
                    },
                    {
                        label: 'Display',
                        icon: 'pi pi-fw pi-desktop',
                        routerLink: ['utilities/display']
                    },
                    {
                        label: 'Elevation',
                        icon: 'pi pi-fw pi-external-link',
                        routerLink: ['utilities/elevation']
                    },
                    {
                        label: 'FlexBox',
                        icon: 'pi pi-fw pi-directions',
                        routerLink: ['utilities/flexbox']
                    },
                    {label: 'Icons', icon: 'pi pi-fw pi-search', routerLink: ['utilities/icons']},
                    {label: 'Text', icon: 'pi pi-fw pi-pencil', routerLink: ['utilities/text']},
                    {
                        label: 'Widgets',
                        icon: 'pi pi-fw pi-star-o',
                        routerLink: ['utilities/widgets']
                    },
                    {
                        label: 'Grid System',
                        icon: 'pi pi-fw pi-th-large',
                        routerLink: ['utilities/grid']
                    },
                    {
                        label: 'Spacing',
                        icon: 'pi pi-fw pi-arrow-right',
                        routerLink: ['utilities/spacing']
                    },
                    {
                        label: 'Typography',
                        icon: 'pi pi-fw pi-align-center',
                        routerLink: ['utilities/typography']
                    }
                ]
            },
            {
                label: 'Pages', icon: 'pi pi-fw pi-briefcase', routerLink: ['/pages'],
                items: [
                    {label: 'Crud', icon: 'pi pi-fw pi-pencil', routerLink: ['/pages/crud']},
                    {
                        label: 'Calendar',
                        icon: 'pi pi-fw pi-calendar-plus',
                        routerLink: ['/pages/calendar']
                    },
                    {
                        label: 'Timeline',
                        icon: 'pi pi-fw pi-calendar',
                        routerLink: ['/pages/timeline']
                    },
                    {label: 'Wizard', icon: 'pi pi-fw pi-star', routerLink: ['/pages/wizard']},
                    {
                        label: 'Landing',
                        icon: 'pi pi-fw pi-globe',
                        badge: '2',
                        badgeClass: 'p-badge-warning',
                        items: [
                            {
                                label: 'Static',
                                icon: 'pi pi-fw pi-globe',
                                url: 'assets/pages/landing.html',
                                target: '_blank'
                            },
                            {
                                label: 'Component',
                                icon: 'pi pi-fw pi-globe',
                                routerLink: ['/landing']
                            }
                        ]
                    },
                    {label: 'Login', icon: 'pi pi-fw pi-sign-in', routerLink: ['/login']},
                    {label: 'Invoice', icon: 'pi pi-fw pi-dollar', routerLink: ['/pages/invoice']},
                    {
                        label: 'Help',
                        icon: 'pi pi-fw pi-question-circle',
                        routerLink: ['/pages/help']
                    },
                    {label: 'Error', icon: 'pi pi-fw pi-times-circle', routerLink: ['/error']},
                    {
                        label: 'Not Found',
                        icon: 'pi pi-fw pi-exclamation-circle',
                        routerLink: ['/notfound']
                    },
                    {label: 'Access Denied', icon: 'pi pi-fw pi-lock', routerLink: ['/access']},
                    {label: 'Contact Us', icon: 'pi pi-fw pi-pencil', routerLink: ['/contactus']},
                    {label: 'Empty', icon: 'pi pi-fw pi-circle-off', routerLink: ['/pages/empty']}
                ]
            },
            {
                label: 'Hierarchy', icon: 'pi pi-fw pi-align-left',
                items: [
                    {
                        label: 'Submenu 1', icon: 'pi pi-fw pi-align-left',
                        items: [
                            {
                                label: 'Submenu 1.1', icon: 'pi pi-fw pi-align-left',
                                items: [
                                    {label: 'Submenu 1.1.1', icon: 'pi pi-fw pi-align-left'},
                                    {label: 'Submenu 1.1.2', icon: 'pi pi-fw pi-align-left'},
                                    {label: 'Submenu 1.1.3', icon: 'pi pi-fw pi-align-left'},
                                ]
                            },
                            {
                                label: 'Submenu 1.2', icon: 'pi pi-fw pi-align-left',
                                items: [
                                    {label: 'Submenu 1.2.1', icon: 'pi pi-fw pi-align-left'}
                                ]
                            },
                        ]
                    },
                    {
                        label: 'Submenu 2', icon: 'pi pi-fw pi-align-left',
                        items: [
                            {
                                label: 'Submenu 2.1', icon: 'pi pi-fw pi-align-left',
                                items: [
                                    {label: 'Submenu 2.1.1', icon: 'pi pi-fw pi-align-left'},
                                    {label: 'Submenu 2.1.2', icon: 'pi pi-fw pi-align-left'},
                                ]
                            },
                            {
                                label: 'Submenu 2.2', icon: 'pi pi-fw pi-align-left',
                                items: [
                                    {label: 'Submenu 2.2.1', icon: 'pi pi-fw pi-align-left'},
                                ]
                            },
                        ]
                    }
                ]
            },
            {
                label: 'Start', icon: 'pi pi-fw pi-download',
                items: [
                    {
                        label: 'Buy Now',
                        icon: 'pi pi-fw pi-shopping-cart',
                        url: ['https://www.primefaces.org/store']
                    },
                    {
                        label: 'Documentation',
                        icon: 'pi pi-fw pi-info-circle',
                        routerLink: ['/documentation']
                    }
                ]
            }
        ];
  }

  // async getRole(){


  //   var param = {
  //       email: "string@gmail.com",
  //       password: "123"
  //   }

  //   var res = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.TEST, API.API_TEST.SIGNIN, param);
  //   console.log("res", res);
  //   sessionStorage.setItem("token", res.token);

  //   var token = await this.jwtHelper.tokenGetter();
  //   const decodedToken = this.jwtHelper.decodeToken(token);
  //   console.log("token", decodedToken);

  //   const name = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
  //   const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

  //   console.log('Name:', name);
  //   console.log('Role:', role);
  // }

}
