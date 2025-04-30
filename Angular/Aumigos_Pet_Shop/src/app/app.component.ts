import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AuthenticationService } from './Services/authentication.service';
import { Subscription } from 'rxjs';
import { UserModel } from './Model/user.model';
import { ApiServices } from './Services/petShopApi.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'Aumigos';
  loged: boolean;
  sub: Subscription[] = [];
  user: UserModel;

  constructor(private auth: AuthenticationService, private apiservice: ApiServices, private router: Router) { }

  ngOnInit(): void {
    this.sub.push(this.auth.loged$.subscribe(v => {
      this.loged = v

      try {
        let id: number = JSON.parse(sessionStorage.getItem('currentUser') as string).id;
        this.sub.push(
          this.apiservice.get<UserModel>(`api/v1/users?id=${id}`).subscribe(res => {
            this.user = res
          })
        );
      }
      catch { }
    }));
  }

  logOut() {
    this.auth.logout();
    this.router.navigate(['/home']);
  }

  ngOnDestroy(): void {
    this.sub.forEach(sub => sub.unsubscribe());
  }
}
