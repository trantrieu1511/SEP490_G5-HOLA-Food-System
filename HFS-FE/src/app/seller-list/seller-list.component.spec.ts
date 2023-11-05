import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerListComponent } from './seller-list.component';

describe('SellerListComponent', () => {
  let component: SellerListComponent;
  let fixture: ComponentFixture<SellerListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SellerListComponent]
    });
    fixture = TestBed.createComponent(SellerListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
