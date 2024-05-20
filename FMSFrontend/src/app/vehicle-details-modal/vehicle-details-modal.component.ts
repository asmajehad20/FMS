import { Component, Inject, Output, OnInit,  EventEmitter, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';
import { GVAR } from '../../GVAR';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-vehicle-details-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './vehicle-details-modal.component.html',
  styleUrl: './vehicle-details-modal.component.css'
})


export class VehicleDetailsModalComponent implements OnInit {

  //data
  gvar: GVAR = new GVAR();
  selectedDriverName: string = "";
  drivers: any[] = [];

  @Output() openSidebarEvent = new EventEmitter<void>();
  showSidebar: boolean = false;

  //constructor
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<VehicleDetailsModalComponent>,
    private dialog: MatDialog,
    private http: HttpClient
  ){}


  //functions
  ngOnInit() {

    this.data.VehicleInformation[0].LastGPSTime = new Date(parseInt(this.data.VehicleInformation[0].LastGPSTime, 10)).toLocaleString('en-US', { timeZone: 'UTC' });
    
    this.http.get<GVAR>('http://localhost:5205/api/Driver')
      .subscribe({
        next: (data: GVAR) => {
          if (data && data.DicOfDT && data.DicOfDT.Drivers) {

            this.drivers = data.DicOfDT.Drivers;
            this.selectedDriverName = this.data.VehicleInformation[0].DriverName;

          }
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
     
  }

  onDriverSelected(): string {

    const selectedDriverObj = this.drivers.find(driver => driver.DriverName === this.selectedDriverName);
    
    const bodydata = {
      DriverID: selectedDriverObj.DriverID,
      VehicleID: this.data.id
    };

    this.gvar.DicOfDic["Tags"] = bodydata;
    this.http.put(`http://localhost:5205/api/VehiclesInformations/${this.data.id}`, this.gvar)
      .subscribe({
        next: (response) => {
          console.log('Request successful:', response);
        },
        error: (error) => {
          console.error('Error sending request:', error);
        }
      });

    const selectedDriver = this.drivers.find(driver => driver.PhoneNumber === selectedDriverObj.PhoneNumber);

    if (selectedDriver) {
      this.data.VehicleInformation[0].PhoneNumber = selectedDriver.PhoneNumber;
    }

    return selectedDriverObj ? selectedDriverObj.DriverName : '';
  }
}
