import { Component, Renderer2, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { RoleNames } from '../utils/roleName';

@Component({
  selector: 'app-notfound',
  templateUrl: './app.notfound.component.html',
})
export class AppNotfoundComponent implements OnInit{

  constructor(public renderer: Renderer2, 
    private location: Location,
    private authService: AuthService,
    private router: Router  
  ) { }

  ngOnInit(): void {
    const cssFilePaths = ['assets/theme/indigo/theme-light.css', 
    'assets/layout/css/layout-light.css'];

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
  }
  
  backClicked() {
    //this.location.back();
    const role = this.authService.getUserInfor().role;
    if(role == null || 
      RoleNames[role] == 'Customer'){
        this.router.navigateByUrl('');
    }else{
      this.router.navigateByUrl('HFSBusiness');
    }
  }
}
