import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentverifyComponent } from './paymentverify.component';

describe('PaymentverifyComponent', () => {
  let component: PaymentverifyComponent;
  let fixture: ComponentFixture<PaymentverifyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PaymentverifyComponent]
    });
    fixture = TestBed.createComponent(PaymentverifyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
