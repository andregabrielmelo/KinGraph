export interface ProfileRecord {
  name: string;
  gender: string | null;
  dateOfBirth: string | null;
  occupationName: string | null;
  occupationSalary: number | null;
}

export interface UpdateProfileRequest {
  gender?: string;
  dateOfBirth?: string;
  occupationName?: string;
}
