import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DriverDeleteConfirmationModalComponent } from './driver-delete-confirmation-modal.component';

describe('DriverDeleteConfirmationModalComponent', () => {
  let component: DriverDeleteConfirmationModalComponent;
  let fixture: ComponentFixture<DriverDeleteConfirmationModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DriverDeleteConfirmationModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DriverDeleteConfirmationModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
