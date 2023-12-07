import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerpostreportComponent } from './customerpostreport.component';

describe('CustomerpostreportComponent', () => {
  let component: CustomerpostreportComponent;
  let fixture: ComponentFixture<CustomerpostreportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustomerpostreportComponent]
    });
    fixture = TestBed.createComponent(CustomerpostreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
