import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentverifySellerComponent } from './paymentverify.component';

describe('PaymentverifyComponent', () => {
  let component: PaymentverifySellerComponent;
  let fixture: ComponentFixture<PaymentverifySellerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PaymentverifySellerComponent]
    });
    fixture = TestBed.createComponent(PaymentverifySellerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
