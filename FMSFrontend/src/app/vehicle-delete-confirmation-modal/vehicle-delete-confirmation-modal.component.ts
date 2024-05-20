import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';
import { GVAR } from '../../GVAR';


@Component({
  selector: 'app-vehicle-delete-confirmation-modal',
  standalone: true,
  imports: [],
  templateUrl: './vehicle-delete-confirmation-modal.component.html',
  styleUrl: './vehicle-delete-confirmation-modal.component.css'
})


export class VehicleDeleteConfirmationModalComponent {

  //constructor
  constructor(
    public dialogRef: MatDialogRef<VehicleDeleteConfirmationModalComponent>,
    private http: HttpClient, private dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ){}

  //functions
  onCancel(): void {
    this.dialogRef.close(false);
  }

  onConfirm(): void {
    this.http.delete<any>(`http://localhost:5205/api/Vehicles/${this.data.id}`)
      .subscribe({
        next: (response) => {
          console.log('Response:', response);
        },
        error: (error) => {
          console.log('Error:', error);
        }
      });
     
    this.dialogRef.close(true);
  }
}
