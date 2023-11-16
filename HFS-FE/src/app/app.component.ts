import { Component } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { User } from './services/auth.service';
import { PresenceService } from './services/presence.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent  {
  title = 'HFS-FE';
  role: string;
  isDivVisible: boolean = true;
  constructor(private primengConfig: PrimeNGConfig,public presence: PresenceService) {
  }

  ngOnInit() {
      this.primengConfig.ripple = true;
    this.role = sessionStorage.getItem('role');
    this.isDivVisible = !this.isDivVisible;

    this.setCurrentUser();
  }
  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    const token = sessionStorage.getItem('JWT');
   // debugger;
    if (user) {

      this.presence.createHubConnection(token);
    }
  }
}
