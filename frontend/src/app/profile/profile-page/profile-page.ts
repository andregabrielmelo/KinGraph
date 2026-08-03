import { ChangeDetectionStrategy, Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { PersonService } from '../person.service';

@Component({
  selector: 'app-profile-page',
  standalone: true,
  imports: [ReactiveFormsModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './profile-page.html'
})
export class ProfilePage implements OnInit {
  private readonly personService = inject(PersonService);
  private readonly formBuilder = inject(FormBuilder);

  readonly loading = signal(true);
  readonly saving = signal(false);
  readonly errorMessage = signal<string | null>(null);
  readonly savedMessage = signal<string | null>(null);

  readonly form = this.formBuilder.nonNullable.group({
    gender: [''],
    dateOfBirth: [''],
    occupationName: ['', [Validators.maxLength(200)]]
  });

  ngOnInit(): void {
    this.personService.getMyProfile().subscribe({
      next: (profile) => {
        this.loading.set(false);
        this.form.patchValue({
          gender: profile.gender ?? '',
          dateOfBirth: profile.dateOfBirth ? profile.dateOfBirth.substring(0, 10) : '',
          occupationName: profile.occupationName ?? ''
        });
      },
      error: () => {
        this.loading.set(false);
        this.errorMessage.set('Could not load your profile.');
      }
    });
  }

  submit(): void {
    if (this.saving()) {
      return;
    }

    this.saving.set(true);
    this.errorMessage.set(null);
    this.savedMessage.set(null);

    const { gender, dateOfBirth, occupationName } = this.form.getRawValue();
    this.personService
      .updateMyProfile({
        gender: gender || undefined,
        dateOfBirth: dateOfBirth || undefined,
        occupationName: occupationName || undefined
      })
      .subscribe({
        next: () => {
          this.saving.set(false);
          this.savedMessage.set('Profile saved.');
        },
        error: (error: HttpErrorResponse) => {
          this.saving.set(false);
          this.errorMessage.set('Could not save your profile. ' + (error.error?.title ?? ''));
        }
      });
  }
}
