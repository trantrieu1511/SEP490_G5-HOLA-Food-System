import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageSellerModuleComponent } from './manage-seller-module.component';

describe('ManageSellerModuleComponent', () => {
  let component: ManageSellerModuleComponent;
  let fixture: ComponentFixture<ManageSellerModuleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageSellerModuleComponent]
    });
    fixture = TestBed.createComponent(ManageSellerModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
