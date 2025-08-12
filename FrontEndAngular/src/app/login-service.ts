import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private apiUrl = 'https://localhost:7059';

  constructor(private http: HttpClient) {}

  login(loginRequest: {
    userName: string;
    empPassword: string;
  }): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, loginRequest);
  }
}
