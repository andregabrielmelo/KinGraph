import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { API_BASE_URL } from '../core/api-config';
import { AuthService } from '../auth/auth.service';
import { ProfileRecord, UpdateProfileRequest } from './models/profile.model';

@Injectable({ providedIn: 'root' })
export class PersonService {
  private readonly http = inject(HttpClient);
  private readonly authService = inject(AuthService);

  getMyProfile(): Observable<ProfileRecord> {
    return this.http.get<ProfileRecord>(`${API_BASE_URL}/users/${this.myUserId()}/person`);
  }

  updateMyProfile(request: UpdateProfileRequest): Observable<ProfileRecord> {
    return this.http.put<ProfileRecord>(`${API_BASE_URL}/users/${this.myUserId()}/person`, request);
  }

  private myUserId(): number {
    const id = this.authService.currentUserId();
    if (id === null) {
      throw new Error('No authenticated user - this should be unreachable behind authGuard.');
    }
    return id;
  }
}
