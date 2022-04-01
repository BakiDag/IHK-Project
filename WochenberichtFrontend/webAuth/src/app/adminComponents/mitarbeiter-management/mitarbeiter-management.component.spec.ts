import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MitarbeiterManagementComponent } from './mitarbeiter-management.component';

describe('MitarbeiterManagementComponent', () => {
  let component: MitarbeiterManagementComponent;
  let fixture: ComponentFixture<MitarbeiterManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MitarbeiterManagementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MitarbeiterManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
