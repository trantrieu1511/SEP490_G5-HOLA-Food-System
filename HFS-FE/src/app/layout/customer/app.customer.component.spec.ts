import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppCustomerLayoutComponent } from './app.customer.component';

describe('App.CustomerComponent', () => {
  let component: AppCustomerLayoutComponent;
  let fixture: ComponentFixture<AppCustomerLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppCustomerLayoutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppCustomerLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
