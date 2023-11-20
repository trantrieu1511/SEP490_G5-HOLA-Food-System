import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WalletComponentSeller } from './wallet.component';

describe('WalletComponent', () => {
  let component: WalletComponentSeller;
  let fixture: ComponentFixture<WalletComponentSeller>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WalletComponentSeller]
    });
    fixture = TestBed.createComponent(WalletComponentSeller);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
