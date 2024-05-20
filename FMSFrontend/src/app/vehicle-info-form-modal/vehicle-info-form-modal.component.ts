import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { GVAR } from '../../GVAR';


@Component({
  selector: 'app-vehicle-info-form-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './vehicle-info-form-modal.component.html',
  styleUrl: './vehicle-info-form-modal.component.css'
})


export class VehicleInfoFormModalComponent {

  //data
  DriverID: number = 0; 
  VehicleMake: string = '';
  VehicleModel: string = '';
  PurchaseDate: Date = new Date();

  gvar: GVAR = new GVAR();

  //constructor
  constructor(
    private http: HttpClient,
    public dialogRef: MatDialogRef<VehicleInfoFormModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any){}

  //functions
  submit(formValue: any) {

    this.PurchaseDate = new Date(formValue.PurchaseDate);

    const formData = {
      VehicleID: this.data.id,
      DriverID: this.DriverID.toString(),
      VehicleMake: this.VehicleMake,
      VehicleModel: this.VehicleModel,
      PurchaseDate: this.PurchaseDate.getTime().toString()
    };
    
    this.gvar.DicOfDic["Tags"] = formData;
    this.http.post('http://localhost:5205/api/VehiclesInformations', this.gvar)
      .subscribe({
        next: (response) => {
          console.log('Data sent successfully:', response);
          this.resetForm();
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });

    this.dialogRef.close();
  }

  resetForm(): void {
    this.VehicleMake = '';
    this.VehicleModel = '';
    this.DriverID = 0;
    this.PurchaseDate = new Date();
  }

  cancel() {
    this.dialogRef.close();
  }
}
