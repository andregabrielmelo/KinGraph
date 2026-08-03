import { ChangeDetectionStrategy, Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { RelationshipService } from '../relationship.service';
import { relationshipLabel } from '../relationship-label.util';
import { RelationshipRecord, UserSummary } from '../models/relationship.model';
import { RelationshipGraph } from '../relationship-graph/relationship-graph';
import { PersonService } from '../../profile/person.service';

type ViewMode = 'list' | 'graph';

@Component({
  selector: 'app-relationship-list-page',
  standalone: true,
  imports: [ReactiveFormsModule, RelationshipGraph],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './relationship-list-page.html'
})
export class RelationshipListPage implements OnInit {
  private readonly relationshipService = inject(RelationshipService);
  private readonly personService = inject(PersonService);
  private readonly formBuilder = inject(FormBuilder);

  readonly loading = signal(true);
  readonly relationships = signal<RelationshipRecord[]>([]);
  readonly otherUsers = signal<UserSummary[]>([]);
  readonly submitting = signal(false);
  readonly errorMessage = signal<string | null>(null);
  readonly viewMode = signal<ViewMode>('list');
  readonly myName = signal('');

  readonly form = this.formBuilder.nonNullable.group({
    relatedUserId: [0, [Validators.required, Validators.min(1)]],
    type: ['Friend' as 'Friend' | 'Family', [Validators.required]],
    kind: [''],
    cousinDegree: [2],
    isByMarriage: [false],
    isHalf: [false]
  });

  protected label = relationshipLabel;

  ngOnInit(): void {
    this.loadRelationships();
    this.relationshipService.listOtherUsers().subscribe((users) => this.otherUsers.set(users));
    this.personService.getMyProfile().subscribe((profile) => this.myName.set(profile.name));
  }

  private loadRelationships(): void {
    this.loading.set(true);
    this.relationshipService.listMyRelationships().subscribe({
      next: (relationships) => {
        this.loading.set(false);
        this.relationships.set(relationships);
      },
      error: () => {
        this.loading.set(false);
        this.errorMessage.set('Could not load your relationships.');
      }
    });
  }

  submit(): void {
    if (this.form.invalid || this.submitting()) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitting.set(true);
    this.errorMessage.set(null);

    const { relatedUserId, type, kind, cousinDegree, isByMarriage, isHalf } = this.form.getRawValue();
    this.relationshipService
      .createRelationship({
        relatedUserId,
        type,
        kind: type === 'Family' ? (kind as 'Parent' | 'Sibling' | 'Cousin') : undefined,
        cousinDegree: type === 'Family' && kind === 'Cousin' ? cousinDegree : undefined,
        isByMarriage,
        isHalf
      })
      .subscribe({
        next: () => {
          this.submitting.set(false);
          this.form.reset({
            relatedUserId: 0,
            type: 'Friend',
            kind: '',
            cousinDegree: 2,
            isByMarriage: false,
            isHalf: false
          });
          this.loadRelationships();
        },
        error: (error: HttpErrorResponse) => {
          this.submitting.set(false);
          this.errorMessage.set(extractErrorMessage(error));
        }
      });
  }
}

function extractErrorMessage(error: HttpErrorResponse): string {
  const errors = error.error?.errors as Record<string, string[]> | undefined;
  if (errors) {
    return Object.values(errors).flat().join(' ');
  }
  return 'Could not create the relationship.';
}
