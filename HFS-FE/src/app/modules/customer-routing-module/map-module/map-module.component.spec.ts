import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MapModuleComponent } from './map-module.component';

describe('MapModuleComponent', () => {
  let component: MapModuleComponent;
  let fixture: ComponentFixture<MapModuleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MapModuleComponent]
    });
    fixture = TestBed.createComponent(MapModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
