import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AusbilderDashboardComponent } from './ausbilder-dashboard.component';

describe('AusbilderDashboardComponent', () => {
  let component: AusbilderDashboardComponent;
  let fixture: ComponentFixture<AusbilderDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AusbilderDashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AusbilderDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
