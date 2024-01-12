import { Injectable } from '@angular/core';
import { User, User_VM } from '../interfaces/interfaces';
import { Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  Register(user: User): Observable<string> {
    return this.http.post<string>(
      'https://localhost:7210/api/Auth/Register',
      user
    );
  }

  Login(user: User_VM): Observable<{ token: string; name: string }> {
    return this.http
      .post<{ token: string; name: string }>(
        'https://localhost:7210/api/Auth/Login',
        user
      )
      .pipe(
        tap(({ token, name }) => {
          localStorage.setItem('auth-token', 'Bearer ' + token);
          localStorage.setItem('name', name);
        })
      );
  }
  getToken(): string {
    return localStorage.getItem('auth-token') as any;
  }
  isLoggedIn(): boolean {
    if (localStorage.getItem('auth-token')) {
      return true;
    }
    return false;
  }
}
