import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FooddetailComponent } from './fooddetail.component';

describe('FooddetailComponent', () => {
  let component: FooddetailComponent;
  let fixture: ComponentFixture<FooddetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FooddetailComponent]
    });
    fixture = TestBed.createComponent(FooddetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
