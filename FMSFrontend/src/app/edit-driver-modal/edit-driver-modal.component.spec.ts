import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditDriverModalComponent } from './edit-driver-modal.component';

describe('EditDriverModalComponent', () => {
  let component: EditDriverModalComponent;
  let fixture: ComponentFixture<EditDriverModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditDriverModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditDriverModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
