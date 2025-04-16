import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { AuthenticationService } from './Services/authentication.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent{
  title = 'Aumigos';

  constructor(private auth: AuthenticationService) {}

  logOut(){
    this.auth.logout();
  }
}
