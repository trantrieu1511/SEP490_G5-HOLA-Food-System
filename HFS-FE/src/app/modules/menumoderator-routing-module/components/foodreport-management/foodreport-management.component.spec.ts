import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodreportManagementComponent } from './foodreport-management.component';

describe('FoodreportManagementComponent', () => {
  let component: FoodreportManagementComponent;
  let fixture: ComponentFixture<FoodreportManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FoodreportManagementComponent]
    });
    fixture = TestBed.createComponent(FoodreportManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
