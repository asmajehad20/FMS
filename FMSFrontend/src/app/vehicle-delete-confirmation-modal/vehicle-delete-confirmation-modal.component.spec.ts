import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleDeleteConfirmationModalComponent } from './vehicle-delete-confirmation-modal.component';

describe('VehicleDeleteConfirmationModalComponent', () => {
  let component: VehicleDeleteConfirmationModalComponent;
  let fixture: ComponentFixture<VehicleDeleteConfirmationModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VehicleDeleteConfirmationModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VehicleDeleteConfirmationModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
