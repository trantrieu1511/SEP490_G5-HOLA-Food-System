import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostreportManagementComponent } from './postreport-management.component';

describe('PostreportManagementComponent', () => {
  let component: PostreportManagementComponent;
  let fixture: ComponentFixture<PostreportManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PostreportManagementComponent]
    });
    fixture = TestBed.createComponent(PostreportManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
