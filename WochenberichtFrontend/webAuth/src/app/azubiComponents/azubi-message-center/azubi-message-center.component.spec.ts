import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AzubiMessageCenterComponent } from './azubi-message-center.component';

describe('AzubiMessageCenterComponent', () => {
  let component: AzubiMessageCenterComponent;
  let fixture: ComponentFixture<AzubiMessageCenterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AzubiMessageCenterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AzubiMessageCenterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
