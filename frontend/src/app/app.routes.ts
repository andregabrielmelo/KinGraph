import { Routes } from '@angular/router';
import { authGuard } from './auth/auth.guard';

export const routes: Routes = [
  {
    path: 'register',
    loadComponent: () => import('./auth/register-page/register-page').then((m) => m.RegisterPage)
  },
  {
    path: 'login',
    loadComponent: () => import('./auth/login-page/login-page').then((m) => m.LoginPage)
  },
  {
    path: 'profile',
    canActivate: [authGuard],
    loadComponent: () => import('./profile/profile-page/profile-page').then((m) => m.ProfilePage)
  },
  {
    path: 'relationships',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./relationships/relationship-list-page/relationship-list-page').then(
        (m) => m.RelationshipListPage
      )
  }
];
