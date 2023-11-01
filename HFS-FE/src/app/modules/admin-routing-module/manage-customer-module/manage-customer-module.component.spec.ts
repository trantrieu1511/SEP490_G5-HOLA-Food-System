import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageCustomerModuleComponent } from './manage-customer-module.component';

describe('ManageCustomerModuleComponent', () => {
  let component: ManageCustomerModuleComponent;
  let fixture: ComponentFixture<ManageCustomerModuleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageCustomerModuleComponent]
    });
    fixture = TestBed.createComponent(ManageCustomerModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
