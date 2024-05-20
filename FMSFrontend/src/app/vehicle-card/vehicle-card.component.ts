import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { GVAR } from '../../GVAR';
import { VehicleDetailsModalComponent } from '../vehicle-details-modal/vehicle-details-modal.component';
import { MatDialog } from '@angular/material/dialog';
import { EditVehicleModalComponent } from '../edit-vehicle-modal/edit-vehicle-modal.component';
import { VehicleDeleteConfirmationModalComponent } from '../vehicle-delete-confirmation-modal/vehicle-delete-confirmation-modal.component';


@Component({
  selector: 'app-vehicle-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './vehicle-card.component.html',
  styleUrl: './vehicle-card.component.css'
})


export class VehicleCardComponent {

  //data
  @Input() data: any;
  gvar: GVAR = new GVAR();

  //constructor
  constructor(private http: HttpClient, private dialog: MatDialog) {}

  //functions
  showMoreInfo(vehicleId: string) {

    this.http.get<any>(`http://localhost:5205/api/VehiclesInformations/${vehicleId}`)
      .subscribe({
        next: (data: GVAR) => {
          if (data && data.DicOfDT && data.DicOfDT.VehicleInformation) {
            
            const dialogRef = this.dialog.open(VehicleDetailsModalComponent, {
              data: {
                VehicleInformation: data.DicOfDT.VehicleInformation,
                id: vehicleId
              }
            });

          }
        },
        error: (error) => {
          console.error('Error :', error);
        }
      });
     
  }

  openEditVehicleModal(vehicleId: string) {

    this.http.get<any>(`http://localhost:5205/api/VehiclesInformations/${vehicleId}`)
      .subscribe({
        next: (data: GVAR) => {
          if (data && data.DicOfDT && data.DicOfDT.VehicleInformation) {

            const dialogRef = this.dialog.open(EditVehicleModalComponent, {
              data: {
                VehicleInformation: data.DicOfDT.VehicleInformation[0],
                id: vehicleId
              }
            });

            dialogRef.afterClosed().subscribe(result => {
              location.reload();
            });

          }
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
    
  }

  deleteVehicle(vehicleId: string) {
    const dialogRef = this.dialog.open(VehicleDeleteConfirmationModalComponent, {
      data: { id: vehicleId } 
    });

    dialogRef.afterClosed().subscribe(result => {
      location.reload();
    });
  }
}
