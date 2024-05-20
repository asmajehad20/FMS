import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GeofencesPageComponent } from './geofences-page.component';

describe('GeofencesPageComponent', () => {
  let component: GeofencesPageComponent;
  let fixture: ComponentFixture<GeofencesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GeofencesPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GeofencesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
