import { Component } from '@angular/core';
import { VehiclesTableComponent } from '../vehicles-table/vehicles-table.component';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [VehiclesTableComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})

export class HomePageComponent {

}
