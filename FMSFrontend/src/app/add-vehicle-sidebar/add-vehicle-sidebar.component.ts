import { Component, Input } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { GVAR } from '../../GVAR';
import { response } from 'express';


@Component({
  selector: 'app-add-vehicle-sidebar',
  standalone: true,
  imports: [MatSidenavModule, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './add-vehicle-sidebar.component.html',
  styleUrl: './add-vehicle-sidebar.component.css'
})


export class AddVehicleSidebarComponent {

  //data
  @Input() showSidebar = false;
  gvar: GVAR = new GVAR();

  VehicleNumber: string = '';
  VehicleType: string = '';

  //constructor
  constructor(private http: HttpClient) { }

  //functions
  submit() {

    const vehicleformData = {
      VehicleNumber: this.VehicleNumber,
      VehicleType: this.VehicleType,
    };
    
    this.gvar.DicOfDic["Tags"] = vehicleformData;

    this.http.post('http://localhost:5205/api/Vehicles', this.gvar)
      .subscribe({
        next: (response) => {
          console.log('Data sent successfully:', response);
          this.resetForm();
        },
        error: (error) => {
          console.error('Error sending data:', error);
        }
      });
      
    this.showSidebar = false;
    window.location.reload();
  }

  resetForm(): void {
    this.VehicleNumber = '';
    this.VehicleType = '';
  }

  cancel() {
    this.showSidebar = false;
  }
}
