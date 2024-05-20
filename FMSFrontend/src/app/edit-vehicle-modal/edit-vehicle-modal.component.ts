import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { GVAR } from '../../GVAR';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-edit-vehicle-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './edit-vehicle-modal.component.html',
  styleUrl: './edit-vehicle-modal.component.css'
})


export class EditVehicleModalComponent implements OnInit {

  //data
  gvar: GVAR = new GVAR();
  VehicleNumber: string = "";
  VehicleType: string = ""
  VehicleMake: string = "";
  VehicleModel: string = "";
  PurchaseDate: Date = new Date();


  //constructor
  constructor(
    public dialogRef: MatDialogRef<EditVehicleModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private http: HttpClient,
  ){}


  //functions
  ngOnInit(): void {

    this.VehicleNumber = this.data.VehicleInformation.VehicleNumber;
    this.VehicleType = this.data.VehicleInformation.VehicleType;
    this.VehicleMake = this.data.VehicleInformation.VehicleMake;
    this.VehicleModel = this.data.VehicleInformation.VehicleModel;
    
  }

  onSave(formValue: any): void {

    this.PurchaseDate = new Date(formValue.PurchaseDate);

    const vehicleBody = {
      VehicleID: this.data.id,
      VehicleNumber: this.VehicleNumber,
      VehicleType: this.VehicleType

    };

    const vehicleinfoBody = {
      VehicleID: this.data.id,
      VehicleMake: this.VehicleMake,
      VehicleModel: this.VehicleModel,
      PurchaseDate: this.PurchaseDate.getTime().toString()
    };

    this.gvar.DicOfDic["Tags"] = vehicleBody;
    this.http.put(`http://localhost:5205/api/Vehicles/${this.data.id}`, this.gvar)
      .subscribe({
        next: (response) => {
          console.log('Request successful:', response);
        },
        error: (error) => {
          console.error('Error:', error);
        }
      }
    );

    this.gvar.DicOfDic["Tags"] = vehicleinfoBody;
    this.http.put(`http://localhost:5205/api/VehiclesInformations/${this.data.id}`, this.gvar)
      .subscribe({
        next: (response) => {
          console.log('Request successful:', response);
        },
        error: (error) => {
          console.error('Error:', error);
        }
      }
    );
       
    this.dialogRef.close();
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
