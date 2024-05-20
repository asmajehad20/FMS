import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRouteSidebarComponent } from './add-route-sidebar.component';

describe('AddRouteSidebarComponent', () => {
  let component: AddRouteSidebarComponent;
  let fixture: ComponentFixture<AddRouteSidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddRouteSidebarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddRouteSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
