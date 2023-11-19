import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardSellerComponent } from './dashboard-seller.component';

describe('DashboardSellerComponent', () => {
  let component: DashboardSellerComponent;
  let fixture: ComponentFixture<DashboardSellerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DashboardSellerComponent]
    });
    fixture = TestBed.createComponent(DashboardSellerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
