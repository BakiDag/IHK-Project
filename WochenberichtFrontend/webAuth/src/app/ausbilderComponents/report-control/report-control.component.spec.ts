import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportControlComponent } from './report-control.component';

describe('ReportControlComponent', () => {
  let component: ReportControlComponent;
  let fixture: ComponentFixture<ReportControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportControlComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
