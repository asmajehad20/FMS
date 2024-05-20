import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { GVAR } from '../../GVAR';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';


@Component({
  selector: 'app-geofences-table',
  standalone: true,
  imports: [MatTableModule, CommonModule],
  templateUrl: './geofences-table.component.html',
  styleUrl: './geofences-table.component.css'
})


export class GeofencesTableComponent {

  //data
  gvar: GVAR = new GVAR();
  dataSource: MatTableDataSource<any>;
  displayedColumns: string[] = ['GeofenceID', 'GeofenceType', 'AddedDate', 'StrokeColor', 'StrokeOpacity', 'StrokeWeight', 'FillColor', 'FillOpacity'];

  //constructor
  constructor(private http: HttpClient, private dialog: MatDialog) {
    this.dataSource = new MatTableDataSource<any>();
  }

  //functions
  ngOnInit(): void {
    this.http.get<GVAR>('http://localhost:5205/api/Geofences')
      .subscribe({
        next: (data: GVAR) => {
          if (data && data.DicOfDT && data.DicOfDT.Geofences) {
            this.dataSource.data = data.DicOfDT.Geofences;
          }
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
  }

}
