import { Component, Output, EventEmitter, OnDestroy } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})


export class NavbarComponent implements OnDestroy {

  private hubConnection!: HubConnection;
  @Output() toggleSidebar = new EventEmitter<void>();

  //constructor
  constructor() {
    //this.initializeSignalRConnection();
  }

  //functions
  onToggleSidebar() {
    this.toggleSidebar.emit();
  }

  private async initializeSignalRConnection(): Promise<void> {
    try {
      this.hubConnection = new HubConnectionBuilder()
        .withUrl('http://localhost:5205/updateHub')
        .build();


      if (this.hubConnection.state === HubConnectionState.Connected) {
        await this.hubConnection.start();
        console.log('SignalR connection started');
        
      } else {
        console.log('Connection is already started.');
      }

    } catch(error) {
       console.error('Error starting SignalR connection:', error);
    }
   
  }

  ngOnDestroy(): void {
    
  }

}
