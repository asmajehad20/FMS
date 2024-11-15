import { Component, OnInit, PLATFORM_ID, Inject, OnDestroy } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { GVAR } from '../../GVAR';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { VehicleDetailsModalComponent } from '../vehicle-details-modal/vehicle-details-modal.component';
import { MatTableModule } from '@angular/material/table';

interface Vehicle {
  VehicleID: string;
  VehicleNumber: string;
  VehicleType: string;
  LastDirection: string;
  LastStatus: string;
  LastAddress: string;
}

@Component({
  selector: 'app-vehicles-table',
  standalone:true,
  imports: [MatTableModule, CommonModule],
  templateUrl: './vehicles-table.component.html',
  styleUrl: './vehicles-table.component.css',
})


export class VehiclesTableComponent implements OnInit, OnDestroy {
  
  //data
  messages: string[] = [];
  private socket: WebSocket | null = null;
  

  dataSource: MatTableDataSource<any>;
  displayedColumns: string[] = ['VehicleID', 'VehicleNumber', 'VehicleType', 'LastDirection', 'LastStatus', 'LastAddress', 'LastLatitude', 'LastLongitude', 'actions'];

  //constructor
  constructor(@Inject(PLATFORM_ID) private platformId: Object, private http: HttpClient, private dialog: MatDialog) {
    this.dataSource = new MatTableDataSource<any>();
    
  }

  

  //functions
  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.establishWebSocketConnection();
    }


    this.http.get<GVAR>('http://localhost:5205/api/Vehicles').subscribe({
      next: (data: any) => {
        if (data && data.DicOfDT && data.DicOfDT.Vehicles) {
          this.dataSource.data = data.DicOfDT.Vehicles;
        }
      },
      error: (error) => {
        console.error('Error sending data:', error);
        
      }
    });
     
  }
  ngOnDestroy(): void {
    this.socket?.close();
  }
  
  private establishWebSocketConnection() {
    this.socket = new WebSocket('ws://localhost:5205/ws');

    this.socket.onopen = (event) => {
      console.log("WebSocket is open now.");

      if(this.socket != null)
      if (this.socket.readyState === WebSocket.OPEN) {
        this.socket.send("{}");
      }

    };

    this.socket.onmessage = (event) => {
      //console.log("Received updated data from server: ", event.data);

      const eventData = JSON.parse(event.data);

      if (eventData && eventData.DicOfDT && eventData.DicOfDT.Vehicles) {
        //console.log(eventData.DicOfDT);
        this.dataSource.data = eventData.DicOfDT.Vehicles;
      }
    };

    this.socket.onclose = (event) => {
      //console.log("WebSocket connection is closed....");
      
    };

    this.socket.onerror = (error) => {
      //console.log("WebSocket error: ", error);
    };
  }

  showMoreInfo(vehicle: Vehicle) {

    this.http.get<GVAR>(`http://localhost:5205/api/VehiclesInformations/${vehicle.VehicleID}`).subscribe({
      next: (data: any) => {
        if (data && data.DicOfDT && data.DicOfDT.VehicleInformation) {
          const dialogRef = this.dialog.open(VehicleDetailsModalComponent, {
            data: {
              VehicleInformation: data.DicOfDT.VehicleInformation,
              id: vehicle.VehicleID
            }
          });
          dialogRef.afterClosed().subscribe(result => {
            location.reload();
          });
        }
      },
      error: (error) => {
        console.error('Error sending data:', error);
       
      }
    });
  }

 
}
