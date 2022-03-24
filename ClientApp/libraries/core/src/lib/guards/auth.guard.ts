import { Injectable } from '@angular/core';
import { Router, CanActivate, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable()
export class ClientGuard implements CanActivate {
    constructor(public router: Router, public service: AuthService) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (this.service.isAuthenticated()) return true;
        this.service.clearToken();
        this.router.navigate(['/account/login'], { queryParams: { returnUrl: state.url } });
        return false;
    }
}

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(public router: Router, public service: AuthService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (this.service.isAuthenticated() && this.service.isAdmin()) return true;
        this.service.clearToken();
        window.location.href = `${window.location.origin}/account/login?returnUrl=${state.url}`
        return false;
    }
}
