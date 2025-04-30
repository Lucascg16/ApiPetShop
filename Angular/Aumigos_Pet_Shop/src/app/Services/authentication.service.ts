import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { BehaviorSubject, firstValueFrom } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { sessionModel } from '../Model/sessionModel.model';
import { tokenReponseModel } from '../Model/tokenResponseModel.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private loged = new BehaviorSubject<boolean>(false);
  loged$ = this.loged.asObservable();

  constructor(private http: HttpClient) {
    try{
      const session = sessionStorage.getItem('currentUser');
      this.loged.next(!!session);  
    }catch{}
  }

  async login(email: string, password: string) {
    try {
      let response = await firstValueFrom(this.http.post<tokenReponseModel>('api/v1/auth', { Email: email, Password: password }));

      let decodedToken = jwtDecode(response.token) as { id: string, role: string };

      let jsonsession = JSON.stringify(new sessionModel(Number.parseInt(decodedToken.id), decodedToken.role, response.token, response.refreshToken, response.refreshKey));
      sessionStorage.setItem('currentUser', jsonsession)

      this.setLoged(true);
      return { response: "Sucesso", isSuccess: true };
    } catch (error: any) {
      return { response: error.error, isSuccess: false };
    }
  }

  logout(): void {
    let currentUser = JSON.parse(sessionStorage.getItem('currentUser') ?? "");
    this.http.post(`api/v1/auth/revoke?userId=${currentUser.id}`, {});//revoka o token ativo

    this.setLoged(false);
    sessionStorage.removeItem('currentUser');//limpa o storage impedindo acessar o interno do site
  }

  private setLoged(value: boolean): void {
    this.loged.next(value);//seta a variavel conforme o momento, uso interno
  }

  isLoged(): boolean {
    return this.loged.value;
  }
}
