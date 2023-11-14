import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewfeedComponent } from './newfeed.component';

describe('NewfeedComponent', () => {
  let component: NewfeedComponent;
  let fixture: ComponentFixture<NewfeedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NewfeedComponent]
    });
    fixture = TestBed.createComponent(NewfeedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
