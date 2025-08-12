import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { DxTextBoxModule, DxButtonModule } from 'devextreme-angular';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { LoginService } from '../login-service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    DxTextBoxModule,
    DxButtonModule,
    HttpClientModule,
  ],
  templateUrl: './login.html',
  styleUrls: ['./login.scss'],
})
export class Login {
  username: string = '';
  password: string = '';

  constructor(private loginService: LoginService) {}

  onSubmit(form: NgForm) {
    if (form.valid) {
      const loginRequest = {
        userName: this.username,
        empPassword: this.password,
      };

      this.loginService.login(loginRequest).subscribe({
        next: (response: any) => {
          console.log('Login success:', response);
          alert('Login successful!');
          // هنا ممكن تضيف توجيه أو تخزين توكن
        },
        error: (err: any) => {
          console.error('Login failed', err);
          alert('Invalid username or password');
        },
      });
    } else {
      alert('Please fill in all required fields.');
    }
  }
}
