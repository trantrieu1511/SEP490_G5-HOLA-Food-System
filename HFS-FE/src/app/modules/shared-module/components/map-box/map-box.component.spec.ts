import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MapBoxComponent } from './map-box.component';

describe('MapBoxComponent', () => {
  let component: MapBoxComponent;
  let fixture: ComponentFixture<MapBoxComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MapBoxComponent]
    });
    fixture = TestBed.createComponent(MapBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
