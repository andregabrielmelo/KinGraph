export interface RegisterRequest {
  name: string;
  email: string;
  password: string;
  phoneNumber?: string;
}

export interface RegisterResponse {
  id: number;
  name: string;
}
