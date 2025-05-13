import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { catchError, of, Subscription, tap } from 'rxjs';
import { sessionModel } from '../Model/sessionModel.model';
import { ITokenService } from './interface/ITokenService';
import { tokenReponseModel } from '../Model/tokenResponseModel.model';

@Injectable({
  providedIn: 'root'
})
export class TokenService implements ITokenService, OnDestroy {
  private JwpHelper = new JwtHelperService();
  private currentUser: sessionModel;
  private sub: Subscription;

  constructor(private httpClient: HttpClient) { }

  private VerifyToken(): boolean {
    this.currentUser = JSON.parse(sessionStorage.getItem('currentUser') ?? "");
    return this.JwpHelper.isTokenExpired(this.currentUser.token);
  }

  refreshToken(): void {
    let jsonValid = this.VerifyToken();
    
    if (jsonValid) {
      this.sub = this.httpClient.post<tokenReponseModel>("api/v1/auth/refresh", {
        userId: this.currentUser.id,
        refreshToken: this.currentUser.refreshToken,
        refreshKey: this.currentUser.refreshKey
      }).pipe(
        tap(
          response => {
            sessionStorage.setItem('currentUser', JSON.stringify(new sessionModel(this.currentUser.id, this.currentUser.role, response.token, response.refreshToken, response.refreshKey)))
          }
        ),
      catchError(
        err =>{
          console.log(err)
          return of();
        }  
      )).subscribe();
    }
  }

  ngOnDestroy(): void {
    if(this.sub){
      this.sub.unsubscribe();
    }
  }
}
