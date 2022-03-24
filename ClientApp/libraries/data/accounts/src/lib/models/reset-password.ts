import { Registration } from ".";

export interface ResetPassword extends Registration{
    token: string
}