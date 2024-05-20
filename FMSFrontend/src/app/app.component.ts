import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SidebarComponent } from './sidebar/sidebar.component';
import { MatTableModule } from '@angular/material/table';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { VehiclesTableComponent } from './vehicles-table/vehicles-table.component';
import { VehiclesPageComponent } from './vehicles-page/vehicles-page.component';
import { VehicleCardComponent } from './vehicle-card/vehicle-card.component';
import { HomePageComponent } from './home-page/home-page.component';
import { AddVehicleSidebarComponent } from './add-vehicle-sidebar/add-vehicle-sidebar.component';
import { MatSidenavModule } from '@angular/material/sidenav';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    FontAwesomeModule,
    NavbarComponent,
    VehiclesTableComponent,
    SidebarComponent,
    MatTableModule,
    HttpClientModule,
    MatDialogModule,
    VehiclesPageComponent,
    HomePageComponent,
    VehicleCardComponent,
    AddVehicleSidebarComponent,
    MatSidenavModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [
   
  ]
})

export class AppComponent {
  title = 'FMS';
  isOpen = "isOpen";

}
