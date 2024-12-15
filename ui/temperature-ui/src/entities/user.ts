export interface UserRegisterRequest {
    username?: string;
    password?: string;
    email?: string;
    phoneNumber?: string;
}

export interface UserLoginRequest {
    email?: string;
    password?: string;
}
