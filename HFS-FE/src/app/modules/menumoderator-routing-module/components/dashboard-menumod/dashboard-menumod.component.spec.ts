import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardMenumodComponent } from './dashboard-menumod.component';

describe('DashboardMenumodComponent', () => {
  let component: DashboardMenumodComponent;
  let fixture: ComponentFixture<DashboardMenumodComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DashboardMenumodComponent]
    });
    fixture = TestBed.createComponent(DashboardMenumodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
