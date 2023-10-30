import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginNonCustomerComponent } from './login-non-customer.component';

describe('LoginNonCustomerComponent', () => {
  let component: LoginNonCustomerComponent;
  let fixture: ComponentFixture<LoginNonCustomerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LoginNonCustomerComponent]
    });
    fixture = TestBed.createComponent(LoginNonCustomerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
