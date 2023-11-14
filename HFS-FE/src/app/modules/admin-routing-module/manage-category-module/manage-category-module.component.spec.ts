import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageCategoryModuleComponent } from './manage-category-module.component';

describe('ManageCategoryModuleComponent', () => {
  let component: ManageCategoryModuleComponent;
  let fixture: ComponentFixture<ManageCategoryModuleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageCategoryModuleComponent]
    });
    fixture = TestBed.createComponent(ManageCategoryModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
