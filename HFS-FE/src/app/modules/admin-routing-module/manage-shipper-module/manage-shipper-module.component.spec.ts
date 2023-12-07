import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageShipperModuleComponent } from './manage-shipper-module.component';

describe('ManageShipperModuleComponent', () => {
  let component: ManageShipperModuleComponent;
  let fixture: ComponentFixture<ManageShipperModuleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageShipperModuleComponent]
    });
    fixture = TestBed.createComponent(ManageShipperModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
