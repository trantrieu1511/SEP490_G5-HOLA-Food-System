import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderhistoryComponent } from './orderhistory.component';

describe('OrderhistoryComponent', () => {
  let component: OrderhistoryComponent;
  let fixture: ComponentFixture<OrderhistoryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OrderhistoryComponent]
    });
    fixture = TestBed.createComponent(OrderhistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
