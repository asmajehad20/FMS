import { Component, Input } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { GVAR } from '../../GVAR';
import { response } from 'express';


@Component({
  selector: 'app-add-route-sidebar',
  standalone: true,
  imports: [MatSidenavModule, FormsModule, ReactiveFormsModule, CommonModule ],
  templateUrl: './add-route-sidebar.component.html',
  styleUrl: './add-route-sidebar.component.css'
})


export class AddRouteSidebarComponent {

  //data
  @Input() showSidebar = false;

  gvar: GVAR = new GVAR();
  selectedVehicleId: string = "";
  vehicles: any[] = [];

  VehicleID: string = '';
  VehicleDirection: string = '';
  Status: string = "0";
  VehicleSpeed: string = '';
  Epoch: Date = new Date();
  Address: string = '';
  Latitude: string = '';
  Longitude: string = '';

  //constructor
  constructor(private http: HttpClient) { }

  //functions
  ngOnInit(): void {

    this.http.get<GVAR>('http://localhost:5205/api/Vehicles').subscribe({
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


  submit() {

    const formData = {
      VehicleID : this.VehicleID,
      VehicleDirection : this.VehicleDirection,
      Status : this.Status,
      VehicleSpeed : this.VehicleSpeed,
      Epoch : this.Epoch.getTime().toString(),
      Address : this.Address,
      Latitude : this.Latitude,
      Longitude : this.Longitude
    };

    this.gvar.DicOfDic["Tags"] = formData;

    this.http.post('http://localhost:5205/api/RouteHistory', this.gvar)
      .subscribe({
        next: (response) => {
          console.log('Data sent successfully:', response);
          this.resetForm();
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });

    this.showSidebar = false;
    window.location.reload();
  }

  resetForm(): void {
    this.VehicleID = '';
    this.VehicleDirection = '';
    this.Status = '';
    this.VehicleSpeed = '';
    this.Epoch = new Date();
    this.Address = '';
    this.Latitude = '';
    this.Longitude = '';
  }

  cancel() {
    this.showSidebar = false;
  }

}
