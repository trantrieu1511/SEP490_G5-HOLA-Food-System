import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageshipaddressComponent } from './manageshipaddress.component';

describe('ManageshipaddressComponent', () => {
  let component: ManageshipaddressComponent;
  let fixture: ComponentFixture<ManageshipaddressComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageshipaddressComponent]
    });
    fixture = TestBed.createComponent(ManageshipaddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
