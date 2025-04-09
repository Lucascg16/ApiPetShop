import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { tap } from 'rxjs';
import { sessionModel } from '../Model/sessionModel.model';
import { ITokenService } from './interface/ITokenService';
import { tokenReponseModel } from '../Model/tokenResponseModel.model';

@Injectable({
  providedIn: 'root'
})
export class TokenService implements ITokenService {
  private JwpHelper = new JwtHelperService();
  private currentUser: sessionModel;

  constructor(private httpClient: HttpClient) { }

  private VerifyToken(): boolean {
    this.currentUser = JSON.parse(sessionStorage.getItem('currentUser') ?? "");
    return this.JwpHelper.isTokenExpired(this.currentUser.token);
  }

  refreshToken(): void {
    let body = {
      userId: this.currentUser.id,
      refreshToken: this.currentUser.refreshToken,
      refreshKey: this.currentUser.refreshKey
    }

    if (this.VerifyToken()) {
      this.httpClient.post<tokenReponseModel>("api/v1/refresh", body).pipe(
        tap(
          response => sessionStorage.setItem('currentUser', JSON.stringify(new sessionModel(this.currentUser.id, this.currentUser.role, response.token, response.refreshToken, response.refreshKey)))
        ));
    }
  }
}
