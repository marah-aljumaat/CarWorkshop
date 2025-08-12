import { Component, signal } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DxTextBoxModule, DxButtonModule } from 'devextreme-angular';
import { Login } from './login/login';
import { Dashboard } from './dashboard/dashboard';
import { Invoice } from './invoice/invoice';
import { Customer } from './customer/customer';
import { EmployeeAndAttendance } from './employee-and-attendance/employee-and-attendance';
import { Inventory } from './inventory/inventory';
import { InventoryGroup } from './inventory-group/inventory-group';
import { Task } from './task/task';

@Component({
  selector: 'app-root',
  standalone: true, // ✅ مهم جداً
  imports: [
    HttpClientModule,
    RouterOutlet,
    FormsModule,
    DxTextBoxModule,
    DxButtonModule,
    Login,
    Dashboard,
    Invoice,
    Customer,
    EmployeeAndAttendance,
    Inventory,
    InventoryGroup,
    Task,
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.scss'], // ✅ جمع وليس مفرد
})
export class App {
  protected readonly title = signal('FrontEndAngular');
}
