import { Injectable } from "@angular/core";
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable()
export class AuthService {
    static NAME = 'token'
    static TOKEN = '';
    public token: string;
    tokenPayload: any;

    constructor(public JWTHelper: JwtHelperService) {
        this.token = AuthService.TOKEN = this.getToken();
        this.setTokenPayload();
        if (!this.tokenPayload) return;
    }

    setToken(token: string) {
        sessionStorage.setItem(AuthService.NAME, token)
    }

    getToken() {
        return sessionStorage.getItem(AuthService.NAME)
    }

    public setTokenPayload() {
        this.tokenPayload = this.decodeToken();
        if (this.tokenPayload) return;
        let data: any = {};
        this.tokenPayload = data;
    }

    public clearToken() {
        sessionStorage.clear()
    }

    public decodeToken() {
        return this.JWTHelper.decodeToken(this.token)
    }

    public isAuthenticated() {
        return !this.JWTHelper.isTokenExpired(this.token);
    }

    public isLoggedIn(){
        return this.getToken() ? true : false
    }

    public isAdmin(): boolean {
        return this.isAuthenticated() &&
            this.tokenPayload.Role == 'Administrator';
    }
}