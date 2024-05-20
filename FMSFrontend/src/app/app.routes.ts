import { Routes } from '@angular/router';
import { DriversPageComponent } from './drivers-page/drivers-page.component';
import { GeofencesPageComponent } from './geofences-page/geofences-page.component';
import { RouteHistoryPageComponent } from './route-history-page/route-history-page.component';
import { VehiclesPageComponent } from './vehicles-page/vehicles-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { AppComponent } from './app.component';


export const routes: Routes = [

  {
    path: '',
    component: AppComponent,
    children: [
      { path: '', component: HomePageComponent },
    ]
  },

  { path: 'drivers-page', component: DriversPageComponent },
  { path: 'geofences-page', component: GeofencesPageComponent },
  { path: 'route-history-page', component: RouteHistoryPageComponent },
  { path: 'vehicles-page', component: VehiclesPageComponent },
];

