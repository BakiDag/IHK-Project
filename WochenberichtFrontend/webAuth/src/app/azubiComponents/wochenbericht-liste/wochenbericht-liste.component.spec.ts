import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WochenberichtListeComponent } from './wochenbericht-liste.component';

describe('WochenberichtListeComponent', () => {
  let component: WochenberichtListeComponent;
  let fixture: ComponentFixture<WochenberichtListeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WochenberichtListeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WochenberichtListeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
