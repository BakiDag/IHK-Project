import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AzubiManagementComponent } from './azubi-management.component';

describe('AzubiManagementComponent', () => {
  let component: AzubiManagementComponent;
  let fixture: ComponentFixture<AzubiManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AzubiManagementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AzubiManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
