import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleInfoFormModalComponent } from './vehicle-info-form-modal.component';

describe('VehicleInfoFormModalComponent', () => {
  let component: VehicleInfoFormModalComponent;
  let fixture: ComponentFixture<VehicleInfoFormModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VehicleInfoFormModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VehicleInfoFormModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
