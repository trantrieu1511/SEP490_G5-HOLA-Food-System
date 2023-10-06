import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppManageLayoutComponent } from './app.manage.component';

describe('App.ManageComponent', () => {
  let component: AppManageLayoutComponent;
  let fixture: ComponentFixture<AppManageLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppManageLayoutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppManageLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
