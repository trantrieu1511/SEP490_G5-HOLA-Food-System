import { Component, Renderer2, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-error',
  templateUrl: './app.error.component.html',
})
export class AppErrorComponent implements OnInit{

  constructor(public renderer: Renderer2, private location: Location) { }

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
    this.location.back();
  }

}
