import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, map } from 'rxjs';
import { API_BASE_URL } from '../core/api-config';
import { AuthService } from '../auth/auth.service';
import { CreateRelationshipRequest, RelationshipRecord, UserSummary } from './models/relationship.model';

interface UserListResponse {
  items: { id: number; name: string }[];
}

@Injectable({ providedIn: 'root' })
export class RelationshipService {
  private readonly http = inject(HttpClient);
  private readonly authService = inject(AuthService);

  listMyRelationships(): Observable<RelationshipRecord[]> {
    return this.http.get<RelationshipRecord[]>(`${API_BASE_URL}/users/${this.myUserId()}/relationships`);
  }

  createRelationship(request: CreateRelationshipRequest): Observable<void> {
    return this.http.post<void>(`${API_BASE_URL}/users/${this.myUserId()}/relationships`, request);
  }

  // Lists other registered users to pick from when adding a relationship.
  // Fetches a single reasonably-sized page - a searchable picker is a future improvement.
  listOtherUsers(): Observable<UserSummary[]> {
    const myId = this.myUserId();
    return this.http
      .get<UserListResponse>(`${API_BASE_URL}/users`, { params: { per_page: 100 } })
      .pipe(
        map((response) =>
          response.items.filter((user) => user.id !== myId).map((user) => ({ id: user.id, name: user.name }))
        )
      );
  }

  private myUserId(): number {
    const id = this.authService.currentUserId();
    if (id === null) {
      throw new Error('No authenticated user - this should be unreachable behind authGuard.');
    }
    return id;
  }
}
