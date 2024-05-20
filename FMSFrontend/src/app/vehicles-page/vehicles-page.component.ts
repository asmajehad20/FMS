import { Component } from '@angular/core';
import { VehicleCardComponent } from '../vehicle-card/vehicle-card.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { GVAR } from '../../GVAR';
import { AddVehicleSidebarComponent } from '../add-vehicle-sidebar/add-vehicle-sidebar.component';
import { MatSidenavModule } from '@angular/material/sidenav';


@Component({
  selector: 'app-vehicles-page',
  standalone: true,
  imports: [
    VehicleCardComponent,
    AddVehicleSidebarComponent,
    CommonModule,
    FormsModule,
    MatSidenavModule
  ],
  templateUrl: './vehicles-page.component.html',
  styleUrl: './vehicles-page.component.css'
})


export class VehiclesPageComponent {

  //data
  showSidebar: boolean = false;

  gvar: GVAR = new GVAR();
  vehicles: any[] = [];

  //constructor
  constructor(private http: HttpClient) {}


  //functions
  ngOnInit(): void {
    this.http.get<GVAR>('http://localhost:5205/api/Vehicles')
      .subscribe({
        next: (data: GVAR) => {
          if (data && data.DicOfDT && data.DicOfDT.Vehicles) {
            this.vehicles = data.DicOfDT.Vehicles;
          }
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
     
  }

  toggleSidebar() {
    this.showSidebar = !this.showSidebar;
  }

}
