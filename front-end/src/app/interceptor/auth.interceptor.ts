import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { LoginService } from '../services/login.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const loginService = inject(LoginService);
  const authReq = req.clone({
    setHeaders: {
      Authorization: `Bearer ${localStorage.getItem('AUTH_TOKEN')}`
    }
  });

  return next(authReq).pipe(
    catchError((err: any) => {
      if (err instanceof HttpErrorResponse) {
        switch(err.status) {
          case 401:
            loginService.logout();
            console.error('Unauthorized request:', err);
            break;
          default:
            console.error('HTTP error:', err);
            break;
        }
      } else {
        console.error('An error occurred:', err);
      }

      return throwError(() => err);
    })
  );
};
