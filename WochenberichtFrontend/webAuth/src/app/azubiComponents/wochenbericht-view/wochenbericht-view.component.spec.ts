import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WochenberichtViewComponent } from './wochenbericht-view.component';

describe('WochenberichtViewComponent', () => {
  let component: WochenberichtViewComponent;
  let fixture: ComponentFixture<WochenberichtViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WochenberichtViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WochenberichtViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
