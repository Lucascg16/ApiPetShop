import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { jwtDecode, JwtDecodeOptions } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  constructor(private http: HttpClient) { }

  async login(email: string, password: string){
    let token = await firstValueFrom(this.http.post('api/v1/Auth', { Email: email, Password: password}, {responseType: 'text'}));
    let decodedToken = jwtDecode(token) as {id: string, role: string};

    sessionStorage.setItem('currentUser', JSON.stringify({
      id: decodedToken.id,
      role: decodedToken.role,
      token: token
    }))
  }

  logout(){
    sessionStorage.removeItem('currentUser');
  }
}
