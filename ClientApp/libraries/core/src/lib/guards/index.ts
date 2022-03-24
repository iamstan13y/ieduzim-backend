import { AuthService } from "./auth.service";
import { PasswordService } from "./password.service";
import { AuthGuard } from './auth.guard';

export * from "./auth.service";
export * from "./request-service";
export * from './password.service';

export const CORESERVICES = [
    AuthService,
    PasswordService,
    AuthGuard
]