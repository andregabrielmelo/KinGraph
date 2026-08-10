import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';

// Only gates routes that opt in via `data: { requiresLogin: true }` - routes that don't set
// it are accessible either way. When gated and the user isn't logged in, redirects to
// /register (not /login) per current product decision - there's nowhere else to send someone
// with no account yet.
export const AccessGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const requiresLogin = route.data['requiresLogin'] === true;
  if (!requiresLogin) {
    return true;
  }

  if (inject(AuthService).isAuthenticated()) {
    return true;
  }

  return inject(Router).createUrlTree(['/register']);
};
