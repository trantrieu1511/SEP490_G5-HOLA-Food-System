import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export interface AppConfig {
    topbarTheme: string;
    menuTheme: string;
    layoutMode: string;
    menuMode: string;
    inlineMenuPosition: string;
    inputStyle: string;
    ripple: boolean;
    isRTL: boolean;
}

interface LayoutState {
    topbarMenuActive: boolean;

    menuActive: boolean;

    staticMenuDesktopInactive: boolean;

    mobileMenuActive: boolean;

    menuClick: boolean;

    mobileTopbarActive: boolean;

    topbarRightClick: boolean;

    topbarItemClick: boolean;

    activeTopbarItem: string;

    configActive: boolean;

    configClick: boolean;

    rightMenuActive: boolean;

    menuHoverActive: boolean;

    searchClick: boolean;

    search: boolean;

    currentInlineMenuKey: string;

    inlineMenuActive: any[];

    inlineMenuClick: boolean;
}

@Injectable({
    providedIn: 'root',
})
export class LayoutService {

    app: AppConfig = {
        topbarTheme: 'blue',
        menuTheme: 'light',
        layoutMode: 'light',
        menuMode: 'static',
        inlineMenuPosition: 'bottom',
        inputStyle: 'outlined',
        ripple: true,
        isRTL: false
    };

    state: LayoutState = {
        topbarMenuActive: false,
        menuActive: false,
        staticMenuDesktopInactive: false,
        mobileMenuActive: false,
        menuClick: false,
        mobileTopbarActive: false,
        topbarRightClick: false,
        topbarItemClick: false,
        activeTopbarItem: '',
        configActive: false,
        configClick: false,
        rightMenuActive: false,
        menuHoverActive: false,
        searchClick: false,
        search: false,
        currentInlineMenuKey: '',
        inlineMenuActive: [],
        inlineMenuClick: false
    };

    onMenuButtonClick(event: any) {
        this.state.menuActive = !this.state.menuActive;
        this.state.topbarMenuActive = false;
        this.state.topbarRightClick = true;
        this.state.menuClick = true;

        if (this.isDesktop()) {
            this.state.staticMenuDesktopInactive = !this.state.staticMenuDesktopInactive;
        } else {
            this.state.mobileMenuActive = !this.state.mobileMenuActive;
            if (this.state.mobileMenuActive) {
                this.blockBodyScroll();
            } else {
                this.unblockBodyScroll();
            }
        }

        event.preventDefault();
    }

    onTopbarMobileButtonClick(event: any) {
        this.state.mobileTopbarActive = !this.state.mobileTopbarActive;
        event.preventDefault();
    }

    onRightMenuButtonClick(event : any) {
        this.state.rightMenuActive = !this.state.rightMenuActive;
        event.preventDefault();
    }

    onMenuClick($event: any) {
        this.state.menuClick = true;

        if (this.state.inlineMenuActive[this.state.currentInlineMenuKey] 
                && !this.state.inlineMenuClick) {
            this.state.inlineMenuActive[this.state.currentInlineMenuKey] = false;
        }
    }

    onSearchKeydown(event: any) {
        if (event.keyCode === 27) {
            this.state.search = false;
        }
    }

    onInlineMenuClick(event: any, key: any) {
        if (key !== this.state.currentInlineMenuKey) {
            this.state.inlineMenuActive[this.state.currentInlineMenuKey] = false;
        }

        this.state.inlineMenuActive[key] = !this.state.inlineMenuActive[key];
        this.state.currentInlineMenuKey = key;
        this.state.inlineMenuClick = true;
    }

    onTopbarItemClick(event: any, item: any) {
        this.state.topbarItemClick = true;

        if (this.state.activeTopbarItem === item) {
            this.state.activeTopbarItem = '';
        }
        else {
            this.state.activeTopbarItem = item;
        }

        if (item === 'search') {
            this.state.search = !this.state.search;
            this.state.searchClick = !this.state.searchClick;
        }

        event.preventDefault();
    }

    onTopbarSubItemClick(event: any) {
        event.preventDefault();
    }

    isDesktop() {
        return window.innerWidth > 991;
    }

    isMobile() {
        return window.innerWidth <= 991;
    }

    isOverlay() {
        return this.app.menuMode === 'overlay';
    }

    isStatic() {
        return this.app.menuMode === 'static';
    }

    isHorizontal() {
        return this.app.menuMode === 'horizontal';
    }

    isSlim() {
        return this.app.menuMode === 'slim';
    }

    blockBodyScroll(): void {
        if (document.body.classList) {
            document.body.classList.add('blocked-scroll');
        } else {
            document.body.className += ' blocked-scroll';
        }
    }

    unblockBodyScroll(): void {
        if (document.body.classList) {
            document.body.classList.remove('blocked-scroll');
        } else {
            document.body.className = document.body.className.replace(new RegExp('(^|\\b)' +
                'blocked-scroll'.split(' ').join('|') + '(\\b|$)', 'gi'), ' ');
        }
    }


}
