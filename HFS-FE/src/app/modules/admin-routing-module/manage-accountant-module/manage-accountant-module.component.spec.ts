import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAccountantModuleComponent } from './manage-accountant-module.component';

describe('ManageAccountantModuleComponent', () => {
  let component: ManageAccountantModuleComponent;
  let fixture: ComponentFixture<ManageAccountantModuleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageAccountantModuleComponent]
    });
    fixture = TestBed.createComponent(ManageAccountantModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
