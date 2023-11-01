import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ComponentModule} from '../components-module/component.modules';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { StyleClassModule } from 'primeng/styleclass';
import {iComponentBase} from './functions/iComponentBase.component';
import {iServiceBase} from './functions/iServiceBase';
import {LoadingComponent} from './components/loading-component/loading.component';
import {OpenPagePopupComponent} from './components/open-page-popup/open-page-popup.component';
import { iFunction } from './shared-module';
import { DataRealTimeService } from 'src/app/services/SignalR/data-real-time.service';



@NgModule({
  imports: [
      ComponentModule,
      FormsModule,
      CommonModule,
      ReactiveFormsModule,
      StyleClassModule,
  ],
  providers: [
      iServiceBase,
      iFunction,
      DataRealTimeService
  ],
  declarations: [
      iComponentBase,
      LoadingComponent,
      OpenPagePopupComponent,
  ],
  exports: [
      iComponentBase,
      LoadingComponent,
      OpenPagePopupComponent
  ],
})

export class SharedModule { }
