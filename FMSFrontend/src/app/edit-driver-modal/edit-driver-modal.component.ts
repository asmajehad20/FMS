import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { GVAR } from '../../GVAR';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-edit-driver-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './edit-driver-modal.component.html',
  styleUrl: './edit-driver-modal.component.css'
})


export class EditDriverModalComponent {

  //data
  gvar: GVAR = new GVAR();

  DriverID: string = "";
  DriverName: string = "";
  PhoneNumber: string = "";


  //constructor
  constructor(
    public dialogRef: MatDialogRef<EditDriverModalComponent>,
    @Inject(MAT_DIALOG_DATA) public driverData: any,
    private http: HttpClient,
  ){}


  //functions
  ngOnInit(): void {

    this.DriverID = this.driverData.data.DriverID;
    this.DriverName = this.driverData.data.DriverName;
    this.PhoneNumber = this.driverData.data.PhoneNumber;

  }

  onSave(): void {

    const driverBody = {
      DriverID: this.DriverID,
      DriverName: this.DriverName,
      PhoneNumber: this.PhoneNumber
    };

    this.gvar.DicOfDic["Tags"] = driverBody;
    this.http.put(`http://localhost:5205/api/Driver/${this.DriverID}`, this.gvar)
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
