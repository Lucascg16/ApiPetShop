import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { sessionModel } from '../Model/sessionModel';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  constructor(private http: HttpClient) { }

  async login(email: string, password: string){
    try{
      let token = await firstValueFrom(this.http.post('api/v1/Auth', { Email: email, Password: password}, {responseType: 'text'}));    

      let decodedToken = jwtDecode(token) as {id: string, role: string};
      sessionStorage.setItem('currentUser', JSON.stringify(new sessionModel(Number.parseInt(decodedToken.id), decodedToken.role, token)))

      return {response: "Sucesso", isSuccess: true};
    }catch (error: any){
      return {response: error.error, isSuccess: false};
    }
  }

  logout(): void{
    sessionStorage.removeItem('currentUser');
  }
}
