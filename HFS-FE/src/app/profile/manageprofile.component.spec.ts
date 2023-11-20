import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageprofileComponent } from './manageprofile.component';

describe('ManageprofileComponent', () => {
  let component: ManageprofileComponent;
  let fixture: ComponentFixture<ManageprofileComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageprofileComponent]
    });
    fixture = TestBed.createComponent(ManageprofileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
