import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { sessionModel } from '../Model/sessionModel.model';
import { tokenReponseModel } from '../Model/tokenResponseModel.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  constructor(private http: HttpClient) { }

  async login(email: string, password: string){
    try{
      let response = await firstValueFrom(this.http.post<tokenReponseModel>('api/v1/auth', { Email: email, Password: password}));    

      let decodedToken = jwtDecode(response.token) as {id: string, role: string};
      
      let jsonsession = JSON.stringify(new sessionModel(Number.parseInt(decodedToken.id), decodedToken.role, response.token, response.refreshToken, response.refreshKey));
      sessionStorage.setItem('currentUser', jsonsession)

      return {response: "Sucesso", isSuccess: true};
    }catch (error: any){
      return {response: error.error, isSuccess: false};
    }
  }

  logout(): void{
    let currentUser = JSON.parse(sessionStorage.getItem('currentUser') ?? "");
    this.http.post(`api/v1/auth/revoke?userId=${currentUser.id}`, {});//revoka o token ativo

    sessionStorage.removeItem('currentUser');//limpa o storage impedindo acessar o interno do site
  }
}
