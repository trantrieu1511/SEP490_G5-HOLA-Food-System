import {Component, AfterViewInit, Renderer2, OnInit, OnDestroy} from '@angular/core';
import { MenuService } from '../../app-systems/app-menu/app.menu.service';
import { PrimeNGConfig } from 'primeng/api';
import { LayoutService } from '../service/app.layout.service';

@Component({
  selector: 'app-app.manage',
  templateUrl: './app.manage.component.html',
  styleUrls: ['./app.manage.component.scss']
})
export class AppManageLayoutComponent implements AfterViewInit, OnInit, OnDestroy {

  documentClickListener: () => void;

  constructor(public renderer: Renderer2, private menuService: MenuService, 
    public layoutService: LayoutService) { }

  ngOnInit(): void {
    this.layoutService.state.menuActive = this.layoutService.isStatic() 
      && !this.layoutService.isMobile();

      const cssFilePaths = ['assets/theme/indigo/theme-light.css', 
        'assets/layout/css/layout-light.css'];
      cssFilePaths.forEach(link => {
        const cssLink = this.renderer.createElement('link');
        this.renderer.setAttribute(cssLink, 'rel', 'stylesheet');
        this.renderer.setAttribute(cssLink, 'type', 'text/css');
        this.renderer.setAttribute(cssLink, 'href', link);
        this.renderer.appendChild(document.head, cssLink);
      });
  }

  ngAfterViewInit() {
    // hides the horizontal submenus or top menu if outside is clicked
    this.documentClickListener = this.renderer.listen('body', 'click', () => {
        if (!this.layoutService.state.topbarItemClick) {
            this.layoutService.state.activeTopbarItem = null;
            this.layoutService.state.topbarMenuActive = false;
        }

        if (!this.layoutService.state.menuClick && 
          (this.layoutService.isHorizontal() || this.layoutService.isSlim())) {
            this.menuService.reset();
        }

        if (this.layoutService.state.configActive 
          && !this.layoutService.state.configClick) {
            this.layoutService.state.configActive = false;
        }

        if (!this.layoutService.state.menuClick) {
            if (this.layoutService.state.mobileMenuActive) {
                this.layoutService.state.mobileMenuActive = false;
            }

            if (this.layoutService.isOverlay()) {
                this.layoutService.state.menuActive = false;
            }

            this.layoutService.state.menuHoverActive = false;
            this.layoutService.unblockBodyScroll();
        }

        if (!this.layoutService.state.searchClick) {
            this.layoutService.state.search = false;
        }

        if (this.layoutService.state.inlineMenuActive[
              this.layoutService.state.currentInlineMenuKey
            ] && !this.layoutService.state.inlineMenuClick) {
            
            this.layoutService.state.inlineMenuActive[
              this.layoutService.state.currentInlineMenuKey
            ] = false;
        }

        this.layoutService.state.inlineMenuClick = false;
        this.layoutService.state.searchClick = false;
        this.layoutService.state.configClick = false;
        this.layoutService.state.topbarItemClick = false;
        this.layoutService.state.topbarRightClick = false;
        this.layoutService.state.menuClick = false;
      });
  }

  ngOnDestroy() {
    if (this.documentClickListener) {
        this.documentClickListener();
    }
  }
}
