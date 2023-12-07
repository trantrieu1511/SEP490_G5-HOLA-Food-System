import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboadAdminModuleComponent } from './dashboad-admin-module.component';

describe('DashboadAdminModuleComponent', () => {
  let component: DashboadAdminModuleComponent;
  let fixture: ComponentFixture<DashboadAdminModuleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DashboadAdminModuleComponent]
    });
    fixture = TestBed.createComponent(DashboadAdminModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
