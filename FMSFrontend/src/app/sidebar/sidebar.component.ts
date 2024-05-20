import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})


export class SidebarComponent {

  //data
  @Input() isOpen: boolean = false;
  @Output() toggleSidebar = new EventEmitter<void>();

  //functions
  onToggleSidebar() {
    this.toggleSidebar.emit();
  }
}
