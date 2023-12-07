import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WithdrawRequestComponent } from './withdraw-request.component';

describe('WithdrawRequestComponent', () => {
  let component: WithdrawRequestComponent;
  let fixture: ComponentFixture<WithdrawRequestComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WithdrawRequestComponent]
    });
    fixture = TestBed.createComponent(WithdrawRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
