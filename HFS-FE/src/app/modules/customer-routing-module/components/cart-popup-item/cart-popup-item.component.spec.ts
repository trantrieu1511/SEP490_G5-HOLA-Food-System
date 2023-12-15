import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CartPopupItemComponent } from './cart-popup-item.component';

describe('CartPopupItemComponent', () => {
  let component: CartPopupItemComponent;
  let fixture: ComponentFixture<CartPopupItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CartPopupItemComponent]
    });
    fixture = TestBed.createComponent(CartPopupItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
