import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardPostmodComponent } from './dashboard-postmod.component';

describe('DashboardPostmodComponent', () => {
  let component: DashboardPostmodComponent;
  let fixture: ComponentFixture<DashboardPostmodComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DashboardPostmodComponent]
    });
    fixture = TestBed.createComponent(DashboardPostmodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
