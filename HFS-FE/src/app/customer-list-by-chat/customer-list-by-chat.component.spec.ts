import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerListByChatComponent } from './customer-list-by-chat.component';

describe('CustomerListByChatComponent', () => {
  let component: CustomerListByChatComponent;
  let fixture: ComponentFixture<CustomerListByChatComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustomerListByChatComponent]
    });
    fixture = TestBed.createComponent(CustomerListByChatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
