import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AusbilderMessageCenterComponent } from './ausbilder-message-center.component';

describe('AusbilderMessageCenterComponent', () => {
  let component: AusbilderMessageCenterComponent;
  let fixture: ComponentFixture<AusbilderMessageCenterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AusbilderMessageCenterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AusbilderMessageCenterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
