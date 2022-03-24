import { Role } from ".";

export interface Registration{
    username?: string;
    email: string;
    password: string;
    roleId: string;
    role?: Role;
    confirmation: string;
}