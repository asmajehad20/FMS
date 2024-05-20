import { Component } from '@angular/core';
import { GeofencesTableComponent } from '../geofences-table/geofences-table.component';

@Component({
  selector: 'app-geofences-page',
  standalone: true,
  imports: [GeofencesTableComponent],
  templateUrl: './geofences-page.component.html',
  styleUrl: './geofences-page.component.css'
})

export class GeofencesPageComponent {
  
}
