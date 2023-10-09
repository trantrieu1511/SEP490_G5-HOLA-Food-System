import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShipperHistoryManagementComponent } from './shipper-history-management.component';

describe('ShipperHistoryManagementComponent', () => {
  let component: ShipperHistoryManagementComponent;
  let fixture: ComponentFixture<ShipperHistoryManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShipperHistoryManagementComponent]
    });
    fixture = TestBed.createComponent(ShipperHistoryManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
