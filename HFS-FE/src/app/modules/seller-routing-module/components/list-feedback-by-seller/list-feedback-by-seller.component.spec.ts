import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListFeedbackBySellerComponent } from './list-feedback-by-seller.component';

describe('ListFeedbackBySellerComponent', () => {
  let component: ListFeedbackBySellerComponent;
  let fixture: ComponentFixture<ListFeedbackBySellerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ListFeedbackBySellerComponent]
    });
    fixture = TestBed.createComponent(ListFeedbackBySellerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
