import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboadShipperModuleComponent } from './dashboad-shipper-module.component';

describe('DashboadShipperModuleComponent', () => {
  let component: DashboadShipperModuleComponent;
  let fixture: ComponentFixture<DashboadShipperModuleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DashboadShipperModuleComponent]
    });
    fixture = TestBed.createComponent(DashboadShipperModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
