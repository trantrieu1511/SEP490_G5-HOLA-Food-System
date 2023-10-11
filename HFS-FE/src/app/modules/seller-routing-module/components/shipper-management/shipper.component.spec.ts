import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShipperComponent } from './shipper.component';

describe('ShipperComponent', () => {
  let component: ShipperComponent;
  let fixture: ComponentFixture<ShipperComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShipperComponent]
    });
    fixture = TestBed.createComponent(ShipperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
