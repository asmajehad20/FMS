import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RouteHistoryPageComponent } from './route-history-page.component';

describe('RouteHistoryPageComponent', () => {
  let component: RouteHistoryPageComponent;
  let fixture: ComponentFixture<RouteHistoryPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouteHistoryPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RouteHistoryPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
