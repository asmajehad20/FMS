import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVehicleSidebarComponent } from './add-vehicle-sidebar.component';

describe('AddVehicleSidebarComponent', () => {
  let component: AddVehicleSidebarComponent;
  let fixture: ComponentFixture<AddVehicleSidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddVehicleSidebarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddVehicleSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
