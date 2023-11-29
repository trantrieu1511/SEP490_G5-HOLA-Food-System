import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-address-selector',
  templateUrl: './address-selector.component.html',
  styleUrls: ['./address-selector.component.scss']
})
export class AddressSelectorComponent {
  host = 'https://provinces.open-api.vn/api/';
  provinces: any[] = [];
  districts: any[] = [];
  wards: any[] = [];
  selectedResult: string = '';

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.callAPI(this.host + '?depth=1', 'province');
  }

  async callAPI(api: string, target: string) {
    
    const response = await firstValueFrom(this.http.get('https://provinces.open-api.vn/api/?depth=1'))
console.log(response);
        this.renderData(response, target);


  }

  renderData(data: any, target: string) {
    switch (target) {
      case 'province':
        this.provinces = data;
        break;
      case 'district':
        this.districts = data['districts'];
        break;
      case 'ward':
        this.wards = data['wards'];
        break;
      default:
        break;
    }
  }

  onProvinceChange() {

    const selectedProvinceCode = (document.getElementById('province') as HTMLSelectElement).value;
    this.callAPI(this.host + 'p/' + selectedProvinceCode + '?depth=2', 'district');
    this.printResult();
  }

  onDistrictChange() {
    const selectedDistrictCode = (document.getElementById('district') as HTMLSelectElement).value;
    this.callAPI(this.host + 'd/' + selectedDistrictCode + '?depth=2', 'ward');
    this.printResult();
  }

  onWardChange() {
    this.printResult();
  }

  printResult() {
    const selectedProvinceText = (document.getElementById('province') as HTMLSelectElement).options[
      (document.getElementById('province') as HTMLSelectElement).selectedIndex
    ].text;

    const selectedDistrictText = (document.getElementById('district') as HTMLSelectElement).options[
      (document.getElementById('district') as HTMLSelectElement).selectedIndex
    ].text;

    const selectedWardText = (document.getElementById('ward') as HTMLSelectElement).options[
      (document.getElementById('ward') as HTMLSelectElement).selectedIndex
    ].text;

    if (selectedDistrictText !== '' && selectedProvinceText !== '' && selectedWardText !== '') {
      this.selectedResult = `${selectedProvinceText} | ${selectedDistrictText} | ${selectedWardText}`;
    }
  }
}
