import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagePostmoderatorModuleComponent } from './manage-postmoderator-module.component';

describe('ManagePostmoderatorModuleComponent', () => {
  let component: ManagePostmoderatorModuleComponent;
  let fixture: ComponentFixture<ManagePostmoderatorModuleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManagePostmoderatorModuleComponent]
    });
    fixture = TestBed.createComponent(ManagePostmoderatorModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
