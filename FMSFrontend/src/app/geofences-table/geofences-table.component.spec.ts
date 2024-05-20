import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GeofencesTableComponent } from './geofences-table.component';

describe('GeofencesTableComponent', () => {
  let component: GeofencesTableComponent;
  let fixture: ComponentFixture<GeofencesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GeofencesTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GeofencesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
