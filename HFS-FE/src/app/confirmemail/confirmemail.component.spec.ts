import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmemailComponent } from './confirmemail.component';

describe('ConfirmemailComponent', () => {
  let component: ConfirmemailComponent;
  let fixture: ComponentFixture<ConfirmemailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ConfirmemailComponent]
    });
    fixture = TestBed.createComponent(ConfirmemailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
