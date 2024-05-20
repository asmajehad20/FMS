import { Component, OnInit, Output, Input, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { GVAR } from '../../GVAR';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-route-table',
  standalone: true,
  imports: [MatTableModule, CommonModule, FormsModule],
  templateUrl: './route-table.component.html',
  styleUrl: './route-table.component.css'
})


export class RouteTableComponent implements OnInit, OnChanges {

  //data
  @Input() startDate!: Date ;
  @Input() endDate!: Date ;
  @Input() selectedVehicleId!: string;
  gvar: GVAR = new GVAR();
  dataSource: MatTableDataSource<any>;
  displayedColumns: string[] = ['VehicleID', 'VehicleNumber', 'Address', 'Status', 'Latitude', 'Longitude', 'VehicleDirection', 'GPSSpeed', 'GPSTime' ];


  //constructor
  constructor(private http: HttpClient, private dialog: MatDialog) {
    this.dataSource = new MatTableDataSource<any>();
  }


  //functions
  ngOnInit(): void {
    this.fetchRouteHistory();
  }

  ngOnChanges(changes: SimpleChanges): void {

    if (this.inputsChanged(changes)) {
      this.fetchRouteHistory();
    }
  }

  private inputsChanged(changes: SimpleChanges): boolean {
    return (
      'startDate' in changes ||
      'endDate' in changes ||
      'selectedVehicleId' in changes
    );
  }

  fetchRouteHistory(): void {

    if (!(this.startDate) || !(this.endDate) || !this.selectedVehicleId) {
      return;
    }

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'StartTime': this.startDate.getTime().toString(),
      'EndTime': this.endDate.getTime().toString()
    });

    this.http.get<GVAR>(`http://localhost:5205/api/RouteHistory/${this.selectedVehicleId}`, { headers })
      .subscribe({
        next: (data: GVAR) => {
          if (data && data.DicOfDT && data.DicOfDT.RouteHistory) {

            data.DicOfDT.RouteHistory.forEach((entry: any) => { // Explicitly specify the type of entry
              entry.GPSTime = new Date(parseInt(entry.GPSTime, 10)).toLocaleString('en-US', { timeZone: 'UTC' });
            });
            this.dataSource.data = data.DicOfDT.RouteHistory;
            
          }
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
       
  }
  
}
