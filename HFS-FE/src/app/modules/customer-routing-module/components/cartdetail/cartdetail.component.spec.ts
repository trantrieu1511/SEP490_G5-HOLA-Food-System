import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CartdetailComponent } from './cartdetail.component';

describe('CartdetailComponent', () => {
  let component: CartdetailComponent;
  let fixture: ComponentFixture<CartdetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CartdetailComponent]
    });
    fixture = TestBed.createComponent(CartdetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
