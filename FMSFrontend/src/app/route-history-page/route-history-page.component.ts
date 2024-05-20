import { Component } from '@angular/core';
import { RouteTableComponent } from '../route-table/route-table.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GVAR } from '../../GVAR';
import { AddRouteSidebarComponent } from '../add-route-sidebar/add-route-sidebar.component';


@Component({
  selector: 'app-route-history-page',
  standalone: true,
  imports: [CommonModule, RouteTableComponent, FormsModule, AddRouteSidebarComponent],
  templateUrl: './route-history-page.component.html',
  styleUrl: './route-history-page.component.css'
})


export class RouteHistoryPageComponent {

  //data
  showSidebar: boolean = false;

  startDate: Date = new Date('2000-5-5'); 
  endDate: Date = new Date(); 
  selectedVehicleId: string = "";
  vehicles: any[] = [];


  //constructor
  constructor(private http: HttpClient){}


  //functions
  ngOnInit(): void {
    this.fetchVehicleNumbers();
  }

  fetchVehicleNumbers(): void {

    this.http.get<GVAR>('http://localhost:5205/api/Vehicles')
      .subscribe({
        next: (data: GVAR) => {
          if (data && data.DicOfDT && data.DicOfDT.Vehicles) {
            this.vehicles = data.DicOfDT.Vehicles;
            this.selectedVehicleId = this.vehicles[0].VehicleID;
          }
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
      
  }

  onVehicleSelected(): string {
    
    const selectedVehicleObj = this.vehicles.find(vehicle => vehicle.VehicleNumber === this.selectedVehicleId);
    return selectedVehicleObj ? selectedVehicleObj.VehicleID : '';
  }

  formatDate(date: string): string {
    return new Date(date).toISOString().slice(0, 10);
  }

  onStartDateChange(event: any): void {
    this.startDate = new Date(event.target.value);
  }

  onEndDateChange(event: any): void {
    this.endDate = new Date(event.target.value);
  }

  toggleSidebar() {
    this.showSidebar = !this.showSidebar;
  }

  
}
