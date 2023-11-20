import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewFeedModuleComponent } from './new-feed-module.component';

describe('NewFeedModuleComponent', () => {
  let component: NewFeedModuleComponent;
  let fixture: ComponentFixture<NewFeedModuleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NewFeedModuleComponent]
    });
    fixture = TestBed.createComponent(NewFeedModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
