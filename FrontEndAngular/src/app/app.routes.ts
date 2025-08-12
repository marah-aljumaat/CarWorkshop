import { Routes } from '@angular/router';
import { Login } from './login/login';
import { Dashboard } from './dashboard/dashboard';
import { Invoice } from './invoice/invoice';
import { Customer } from './customer/customer';
import { EmployeeAndAttendance } from './employee-and-attendance/employee-and-attendance';
import { Inventory } from './inventory/inventory';
import { InventoryGroup } from './inventory-group/inventory-group';
import { Task } from './task/task';
import { Navbar } from './navbar/navbar';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: Login },
  {
    path: '',
    component: Navbar, // this has Navbar + router-outlet
    children: [
      { path: 'dashboard', component: Dashboard },
      { path: 'customer', component: Customer },
      { path: 'inventory', component: Inventory },
      { path: 'invoices', component: Invoice },
      { path: 'employee', component: EmployeeAndAttendance },
      { path: 'inventoryGroup', component: InventoryGroup },
      { path: 'task', component: Task },
    ],
  },
  { path: '**', redirectTo: 'dashboard' },
];
