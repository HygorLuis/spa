import { CanActivateFn, Router } from '@angular/router';
import { LoginService } from '../../../services/login.service';
import { inject } from '@angular/core';

export const loginGuard: CanActivateFn = (route, state) => {
  const loginService = inject(LoginService);

  if (!loginService.isLoggedIn()) {
    loginService.logout();
     return false;
  }

  return true;
};
