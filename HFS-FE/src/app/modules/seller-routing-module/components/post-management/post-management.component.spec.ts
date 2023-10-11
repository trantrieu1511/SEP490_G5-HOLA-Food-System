import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostManagementComponent } from './post-management.component';

describe('PostManagementComponent', () => {
  let component: PostManagementComponent;
  let fixture: ComponentFixture<PostManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PostManagementComponent]
    });
    fixture = TestBed.createComponent(PostManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
