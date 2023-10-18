import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopdetailComponent } from './shopdetail.component';

describe('ShopdetailComponent', () => {
  let component: ShopdetailComponent;
  let fixture: ComponentFixture<ShopdetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShopdetailComponent]
    });
    fixture = TestBed.createComponent(ShopdetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
