import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AzubiDashboardComponent } from './azubi-dashboard.component';

describe('AzubiDashboardComponent', () => {
  let component: AzubiDashboardComponent;
  let fixture: ComponentFixture<AzubiDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AzubiDashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AzubiDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
