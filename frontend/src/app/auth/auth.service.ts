import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { API_BASE_URL } from '../core/api-config';
import { decodeJwtPayload } from './jwt.util';
import { LoginRequest, LoginResponse, RegisterRequest, RegisterResponse } from './models/auth.model';
import { TokenStorageService } from './token-storage.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly tokenStorage = inject(TokenStorageService);

  register(request: RegisterRequest): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>(`${API_BASE_URL}/users`, request);
  }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http
      .post<LoginResponse>(`${API_BASE_URL}/login`, request)
      .pipe(tap((response) => this.tokenStorage.setToken(response.token)));
  }

  logout(): void {
    this.tokenStorage.clearToken();
  }

  isAuthenticated(): boolean {
    return this.tokenStorage.getToken() !== null;
  }

  getToken(): string | null {
    return this.tokenStorage.getToken();
  }

  currentUserId(): number | null {
    const token = this.tokenStorage.getToken();
    if (!token) {
      return null;
    }
    const payload = decodeJwtPayload(token);
    const id = payload?.sub ? Number(payload.sub) : NaN;
    return Number.isFinite(id) ? id : null;
  }
}
