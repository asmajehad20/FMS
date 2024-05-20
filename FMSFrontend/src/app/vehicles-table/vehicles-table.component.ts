import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { GVAR } from '../../GVAR';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { VehicleDetailsModalComponent } from '../vehicle-details-modal/vehicle-details-modal.component';
import { MatTableModule } from '@angular/material/table';
import { HubConnection } from '@microsoft/signalr';

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


export class VehiclesTableComponent implements OnInit {
  
  //data
  dataSource: MatTableDataSource<any>;
  displayedColumns: string[] = ['VehicleID', 'VehicleNumber', 'VehicleType', 'LastDirection', 'LastStatus', 'LastAddress', 'LastLatitude', 'LastLongitude', 'actions'];

  //constructor
  constructor(private http: HttpClient, private dialog: MatDialog) {
    this.dataSource = new MatTableDataSource<any>();
  }

  //functions
  ngOnInit(): void {

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
