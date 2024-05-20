import { Component, Output, EventEmitter } from '@angular/core';
import { DriverCardComponent } from '../driver-card/driver-card.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { GVAR } from '../../GVAR';
import { AddDriverSidebarComponent } from '../add-driver-sidebar/add-driver-sidebar.component';
import { MatSidenavModule } from '@angular/material/sidenav';


@Component({
  selector: 'app-drivers-page',
  standalone: true,
  imports: [
    DriverCardComponent,
    AddDriverSidebarComponent,
    MatSidenavModule,
    CommonModule,
    FormsModule,
  ],
  templateUrl: './drivers-page.component.html',
  styleUrl: './drivers-page.component.css'
})


export class DriversPageComponent {

  //data
  showSidebar: boolean = false;
  gvar: GVAR = new GVAR();
  drivers: any[] = [];

  //constructor
  constructor(private http: HttpClient) { }


  //functions
  ngOnInit(): void {

    this.http.get<GVAR>('http://localhost:5205/api/Driver')
      .subscribe({
        next:(data: GVAR) => {
          if (data && data.DicOfDT && data.DicOfDT.Drivers) {
            this.drivers = data.DicOfDT.Drivers;
          }
      },
        error: (error) => {
          console.error('Error fetching data:', error);
        }
      });
   
  }

  toggleSidebar() {
    this.showSidebar = !this.showSidebar;
  }
}
