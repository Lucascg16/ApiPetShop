import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterLink, RouterOutlet } from '@angular/router';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { AuthenticationService } from './Services/authentication.service';
import { CompanyModel } from './Model/CompanyModel.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'Aumigos';
  isLogin: boolean
  companie: CompanyModel;
  private subs: Subscription[] = [];

  constructor(private router: Router, private auth: AuthenticationService, private http: HttpClient) {}

  ngOnInit(): void {
    this.subs.push(
      this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: any) => {
        this.isLogin = this.verifyIfLoginPage(event.url);
      })
    );

    this.subs.push(
      this.http.get<CompanyModel>("api/v1/company").subscribe(res =>  this.companie = res)
    );
  }

  ngOnDestroy(): void {
    this.subs.forEach(sub => sub.unsubscribe());
  }

  logOut(){
    this.auth.logout();
  }

  private verifyIfLoginPage(url: string){
    if(url.includes('/login') || url.includes('/forgot') || url.includes('/reset')){
      return true;
    }
    return false;
  }
}
