import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerfoodreportComponent } from './customerfoodreport.component';

describe('CustomerfoodreportComponent', () => {
  let component: CustomerfoodreportComponent;
  let fixture: ComponentFixture<CustomerfoodreportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustomerfoodreportComponent]
    });
    fixture = TestBed.createComponent(CustomerfoodreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
