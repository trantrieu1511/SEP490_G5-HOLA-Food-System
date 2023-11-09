import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvitationShipperComponent } from './invitation-shipper.component';

describe('InvitationShipperComponent', () => {
  let component: InvitationShipperComponent;
  let fixture: ComponentFixture<InvitationShipperComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InvitationShipperComponent]
    });
    fixture = TestBed.createComponent(InvitationShipperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
