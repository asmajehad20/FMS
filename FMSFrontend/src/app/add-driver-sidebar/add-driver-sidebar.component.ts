import { Component, Input } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { GVAR } from '../../GVAR';


@Component({
  selector: 'app-add-driver-sidebar',
  standalone: true,
  imports: [MatSidenavModule, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './add-driver-sidebar.component.html',
  styleUrl: './add-driver-sidebar.component.css'
})


export class AddDriverSidebarComponent {

  //data
  @Input() showSidebar = false;
  gvar: GVAR = new GVAR();

  DriverName: string = '';
  PhoneNumber: string = '';

  //constructor
  constructor(private http: HttpClient) { }

  //functions
  submit() {

    const formData = {
      DriverName: this.DriverName,
      PhoneNumber: this.PhoneNumber,
    };

    this.gvar.DicOfDic["Tags"] = formData;
    this.http.post('http://localhost:5205/api/Driver', this.gvar)
      .subscribe({
        next: (response) => {
          console.log(response);
          this.resetForm();
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });

    this.showSidebar = false;
    window.location.reload();
  }

  resetForm(): void {
    this.DriverName = '';
    this.PhoneNumber = '';
  }

  cancel() {
    this.showSidebar = false;
  }

}
