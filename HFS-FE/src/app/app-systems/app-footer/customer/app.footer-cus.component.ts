import { Component } from '@angular/core';
import { CustomerLayoutService } from 'src/app/layout/service/app.layout-cus.service';

@Component({
    selector: 'app-customer-footer',
    template: `
        <div class="layout-footer">
            <img src="assets/layout/images/{{layoutService.config.colorScheme === 'light' ? 'logo-dark' : 'logo-white'}}.svg" alt="Logo" height="20" class="mr-2"/>
            by
            <span class="font-medium ml-2">PrimeNG</span>
        </div>
    `
})
export class AppCustomerFooterComponent {
    constructor(public layoutService: CustomerLayoutService) { }
}
