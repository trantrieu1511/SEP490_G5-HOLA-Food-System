import { Component } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'HFS-FE';
role:string;
isDivVisible: boolean = true;
  constructor(private primengConfig: PrimeNGConfig) {
  }

  ngOnInit() {
      this.primengConfig.ripple = true;
      this.role=sessionStorage.getItem('role');
  }

  toggleDivVisibility() {
    this.isDivVisible = !this.isDivVisible;
  }
}
