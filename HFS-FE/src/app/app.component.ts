import { Component } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { User } from './services/auth.service';
import { PresenceService } from './services/presence.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'HFS-FE';
  role: string;
  isDivVisible: boolean = true;
  constructor(private primengConfig: PrimeNGConfig,  public translate: TranslateService,public presence: PresenceService) {
    translate.addLangs(['en', 'vi']);

    translate.setDefaultLang('vi');
  }

  ngOnInit() {
    this.primengConfig.ripple = true;
    this.role = sessionStorage.getItem('role');
    this.isDivVisible = !this.isDivVisible;

  }

}
