import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from '../models/login.model';
import { environment } from '../../environments/environment';
import { catchError, map, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class LoginService {

  readonly AUTH_TOKEN = 'AUTH_TOKEN';

  constructor(private httpCliente: HttpClient) { }

  authenticate(login: Login): Observable<boolean> {
    return this.httpCliente.post<string>(`${environment.apiUrl}auth/gerarToken`, login, {
      responseType: 'text' as 'json'
    }).pipe(
      map((token) => {
        localStorage.setItem(this.AUTH_TOKEN, `${token}`);
        return true;
      }),
      catchError(() => of(false))
    );
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem(this.AUTH_TOKEN);
  }

  logout(): void {
    localStorage.removeItem(this.AUTH_TOKEN);
    location.href = 'login';
  }
}
