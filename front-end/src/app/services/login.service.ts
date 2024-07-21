import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from '../models/login.model';
import { environment } from '../../environments/environment';
import { catchError, map, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class LoginService {

  readonly AUTH_TOKEN = 'AUTH_TOKEN';

  constructor(private httpCliente: HttpClient, private router: Router) { }

  authenticate(login: Login): Observable<boolean> {
    return this.httpCliente.post<string>(`${environment.apiUrl}auth/gerarToken`, login, {
      responseType: 'text' as 'json'
    }).pipe(
      map((token) => {
        localStorage.setItem(this.AUTH_TOKEN, `${token}`);
        return true;
      }),
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem(this.AUTH_TOKEN);
  }

  logout(): void {
    localStorage.removeItem(this.AUTH_TOKEN);
    this.router.navigate(['/login']);
  }
}
