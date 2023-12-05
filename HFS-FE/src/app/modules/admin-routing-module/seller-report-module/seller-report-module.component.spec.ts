import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerReportModuleComponent } from './seller-report-module.component';

describe('SellerReportModuleComponent', () => {
  let component: SellerReportModuleComponent;
  let fixture: ComponentFixture<SellerReportModuleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SellerReportModuleComponent]
    });
    fixture = TestBed.createComponent(SellerReportModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
