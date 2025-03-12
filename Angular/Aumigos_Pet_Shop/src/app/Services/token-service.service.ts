import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { tap } from 'rxjs';
import { sessionModel } from '../Model/sessionModel';
import { ITokenService } from './interface/ITokenService';

@Injectable({
  providedIn: 'root'
})
export class TokenService implements ITokenService {
  private JwpHelper = new JwtHelperService();
  private currentUser: sessionModel;

  constructor(private httpClient: HttpClient) {}

  private VerifyToken(): boolean {
    let userdata = sessionStorage.getItem('currentUser');

    if (userdata) {
      this.currentUser = JSON.parse(userdata);
    }
    return this.JwpHelper.isTokenExpired(this.currentUser.token);
  }

  refreshToken() : void {
    if (this.VerifyToken()) {
      this.httpClient.post<string>("api/v1/refresh", {}).pipe(
        tap(
          response => sessionStorage.setItem('currentUser', JSON.stringify(new sessionModel(this.currentUser.id, this.currentUser.role, response)))
        ));
    }
  }
}
