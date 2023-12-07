import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageMenumoderatorModuleComponent } from './manage-menumoderator-module.component';

describe('ManageMenumoderatorModuleComponent', () => {
  let component: ManageMenumoderatorModuleComponent;
  let fixture: ComponentFixture<ManageMenumoderatorModuleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageMenumoderatorModuleComponent]
    });
    fixture = TestBed.createComponent(ManageMenumoderatorModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
