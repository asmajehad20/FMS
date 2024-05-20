import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { GVAR } from '../../GVAR';
import { MatDialog } from '@angular/material/dialog';
import { EditDriverModalComponent } from '../edit-driver-modal/edit-driver-modal.component';
import { DriverDeleteConfirmationModalComponent } from '../driver-delete-confirmation-modal/driver-delete-confirmation-modal.component';


@Component({
  selector: 'app-driver-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './driver-card.component.html',
  styleUrl: './driver-card.component.css'
})


export class DriverCardComponent {

  //data
  @Input() data: any;
  gvar: GVAR = new GVAR();

  //constructor
  constructor(private http: HttpClient, private dialog: MatDialog) { }


  //functions
  openEditDriverModal(data: any) {

    const dialogRef = this.dialog.open(EditDriverModalComponent, {
      data: { data } 
    });

    dialogRef.afterClosed().subscribe(result => {
      location.reload();
    });
  }

  deleteDriver(driverId: string) {
    const dialogRef = this.dialog.open(DriverDeleteConfirmationModalComponent, {
      data: { id: driverId }
    });

    dialogRef.afterClosed().subscribe(result => {
      location.reload();
    });
  }
}
