import { Component, Output, EventEmitter, OnDestroy } from '@angular/core';


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})


export class NavbarComponent implements OnDestroy {

  @Output() toggleSidebar = new EventEmitter<void>();

  //constructor
  constructor() {}

  //functions
  onToggleSidebar() {
    this.toggleSidebar.emit();
  }

  ngOnDestroy(): void {
    
  }

}
