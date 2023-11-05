import { Component, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { CustomerLayoutService } from '../service/app.layout-cus.service';
import { NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AppCustomerTopBarComponent } from 'src/app/app-systems/app-topbar/customer/app.topbar-cus.component';
import { filter } from 'rxjs/operators';


@Component({
    selector: 'app-app.customer',
    templateUrl: './app.customer.component.html',
    styleUrls: ['./app.customer.component.scss']
})
export class AppCustomerLayoutComponent implements OnDestroy, OnInit {

    overlayMenuOpenSubscription: Subscription;

    menuOutsideClickListener: any;

    profileMenuOutsideClickListener: any;


    @ViewChild(AppCustomerTopBarComponent) appTopbar!: AppCustomerTopBarComponent;


    constructor(public layoutService: CustomerLayoutService,
        public renderer: Renderer2, public router: Router) {
        this.overlayMenuOpenSubscription = this.layoutService.overlayOpen$.subscribe(() => {

            if (!this.profileMenuOutsideClickListener) {
                this.profileMenuOutsideClickListener = this.renderer.listen('document', 'click', event => {
                    const isOutsideClicked = !(this.appTopbar.menu.nativeElement.isSameNode(event.target) || this.appTopbar.menu.nativeElement.contains(event.target)
                        || this.appTopbar.topbarMenuButton.nativeElement.isSameNode(event.target) || this.appTopbar.topbarMenuButton.nativeElement.contains(event.target));

                    if (isOutsideClicked) {
                        this.hideProfileMenu();
                    }
                });
            }

            if (this.layoutService.state.staticMenuMobileActive) {
                this.blockBodyScroll();
            }
        });

        this.router.events.pipe(filter(event => event instanceof NavigationEnd))
            .subscribe(() => {
                this.hideMenu();
                this.hideProfileMenu();
            });
    }

    hideMenu() {
        this.layoutService.state.overlayMenuActive = false;
        this.layoutService.state.staticMenuMobileActive = false;
        this.layoutService.state.menuHoverActive = false;
        if (this.menuOutsideClickListener) {
            this.menuOutsideClickListener();
            this.menuOutsideClickListener = null;
        }
        this.unblockBodyScroll();
    }

    hideProfileMenu() {
        this.layoutService.state.profileSidebarVisible = false;
        if (this.profileMenuOutsideClickListener) {
            this.profileMenuOutsideClickListener();
            this.profileMenuOutsideClickListener = null;
        }
    }

    blockBodyScroll(): void {
        if (document.body.classList) {
            document.body.classList.add('blocked-scroll');
        }
        else {
            document.body.className += ' blocked-scroll';
        }
    }

    unblockBodyScroll(): void {
        if (document.body.classList) {
            document.body.classList.remove('blocked-scroll');
        }
        else {
            document.body.className = document.body.className.replace(new RegExp('(^|\\b)' +
                'blocked-scroll'.split(' ').join('|') + '(\\b|$)', 'gi'), ' ');
        }
    }

    get containerClass() {
        return {
            'layout-theme-light': this.layoutService.config.colorScheme === 'light',
            'layout-theme-dark': this.layoutService.config.colorScheme === 'dark',
            'layout-overlay': this.layoutService.config.menuMode === 'overlay',
            'layout-static': this.layoutService.config.menuMode === 'static',
            'layout-static-inactive': this.layoutService.state.staticMenuDesktopInactive && this.layoutService.config.menuMode === 'static',
            'layout-overlay-active': this.layoutService.state.overlayMenuActive,
            'layout-mobile-active': this.layoutService.state.staticMenuMobileActive,
            'p-input-filled': this.layoutService.config.inputStyle === 'filled',
            'p-ripple-disabled': !this.layoutService.config.ripple
        }
    }



    ngOnDestroy() {
        if (this.overlayMenuOpenSubscription) {
            this.overlayMenuOpenSubscription.unsubscribe();
        }

        if (this.menuOutsideClickListener) {
            this.menuOutsideClickListener();
        }
    }

    // Not allow the user to access the page if they are not guests/customers
    checkUserAccessPermission() {
        let userRoleName = sessionStorage.getItem("userId").substring(0, 2);
        if (userRoleName !== 'CU') {
            this.router.navigateByUrl('/HFSBusiness');
            alert('You can only visit this website as a guest or customer.');
        }
    }

    ngOnInit(): void {
        const cssFilePaths = ['assets/theme/lara-light-indigo/theme.css',
            'assets/layout/customer/layout.css'];
        // Xóa các liên kết CSS hiện có trong document.head
        const existingLinks = document.head.querySelectorAll('link[rel="stylesheet"]');
        existingLinks.forEach((link: HTMLLinkElement) => {
            document.head.removeChild(link);
        });

        cssFilePaths.forEach(link => {
            const cssLink = this.renderer.createElement('link');
            this.renderer.setAttribute(cssLink, 'rel', 'stylesheet');
            this.renderer.setAttribute(cssLink, 'type', 'text/css');
            this.renderer.setAttribute(cssLink, 'href', link);
            this.renderer.appendChild(document.head, cssLink);
        });
        this.checkUserAccessPermission();
    }
}
