import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportSellerComponent } from './report-seller.component';

describe('ReportSellerComponent', () => {
  let component: ReportSellerComponent;
  let fixture: ComponentFixture<ReportSellerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReportSellerComponent]
    });
    fixture = TestBed.createComponent(ReportSellerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
