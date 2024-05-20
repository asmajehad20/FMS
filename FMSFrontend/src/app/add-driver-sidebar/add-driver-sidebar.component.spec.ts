import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddDriverSidebarComponent } from './add-driver-sidebar.component';

describe('AddDriverSidebarComponent', () => {
  let component: AddDriverSidebarComponent;
  let fixture: ComponentFixture<AddDriverSidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddDriverSidebarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddDriverSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
