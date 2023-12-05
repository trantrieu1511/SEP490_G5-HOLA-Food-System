import { Component, OnInit } from '@angular/core';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../services/apiURL";
import {
  ConfirmationService,
  LazyLoadEvent,
  MenuItem,
  MessageService,
  SelectItem,
  TreeNode
} from "primeng/api";
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

declare var google: any;
@Component({
  selector: 'app-map-module',
  templateUrl: './map-module.component.html',
  styleUrls: ['./map-module.component.scss']
})

export class MapModuleComponent extends iComponentBase implements OnInit  {

  lat:number;
  lng:number;
  ngOnInit() {
    // this.initMap(37.4223866,-122.0841896);
    this.geocodeAddress();
  }

  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    public router: Router,
    public authService: AuthService
    // private appCustomerTopBarComponent: AppCustomerTopBarComponent
  ) {
    super(messageService);
  }

  // initMap() {
  //   // Khởi tạo bản đồ
  //   const map = new google.maps.Map(document.getElementById('map'), {
  //     center: { lat: 	21.028511, lng: 105.804817 },
  //     zoom: 8
  //   });
  // }
  // constructor(private http: HttpClient) {}

  // getCoordinates(address: string): Observable<any> {
  //   const apiKey = 'AIzaSyADFZsgkfl-RAhyQs4rTHeqtPb50ch878Y';
  //   const encodedAddress = encodeURIComponent(address);
  //   const url = `https://maps.googleapis.com/maps/api/geocode/json?address=${encodedAddress}&key=${apiKey}`;
  //   return this.http.get(url);
  // }

  // ngOnInit() {
  //   this.geocodeAddress('1600 Amphitheatre Parkway, Mountain View, CA');
  // }

  // geocodeAddress(address: string) {
  //   this.getCoordinates(address).subscribe(
  //     (data: any) => {
  //       if (data.status === 'OK') {
  //         const location = data.results[0].geometry.location;
  //         this.initMap(location.lat, location.lng);
  //       } else {
  //         console.error('Error getting coordinates from address:', data.status);
  //       }
  //     },
  //     (error) => {
  //       console.error('Error:', error);
  //     }
  //   );
  // }
 async geocodeAddress() {
  const param={
     "address":"ĐH FPT,Khu Công nghệ cao Hòa Lạc, Km29 Đại lộ Thăng Long, huyện Thạch Thất, Hà Nội "
  }
  const response = await  this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_USER.MAP,param);
  const location = response.results[0].geometry.location;
           this.initMap(location.lat, location.lng);

  }
  initMap(latitude: number, longitude: number) {
    const map = new google.maps.Map(document.getElementById('map'), {
      center: { lat: latitude, lng: longitude },
      zoom: 12
    });

    const marker = new google.maps.Marker({
      position: { lat: latitude, lng: longitude },
      map: map,
      title: 'Specific Address'
    });
  }

}
