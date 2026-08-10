import { Routes } from '@angular/router';
import { AccessGuard } from './auth/access.guard';

export const routes: Routes = [
  // Initial page is /register for now - revisit once there's a real landing page.
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  {
    path: 'register',
    loadComponent: () => import('./auth/register-page/register-page').then((m) => m.RegisterPage)
  },
  {
    path: 'login',
    loadComponent: () => import('./auth/login-page/login-page').then((m) => m.LoginPage)
  },
  {
    path: 'home',
    data: { requiresLogin: true },
    canActivate: [AccessGuard],
    loadComponent: () => import('./home/home-page').then((m) => m.HomePage)
  }
];
