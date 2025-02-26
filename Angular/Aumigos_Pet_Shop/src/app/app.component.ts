import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterLink, RouterOutlet } from '@angular/router';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';

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
  private routerSub: Subscription;

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.routerSub = this.router.events
    .pipe(filter(event => event instanceof NavigationEnd))
    .subscribe((event: any) => {
      this.isLogin = this.verifyIfLoginPage(event.url);
    });
  }
  ngOnDestroy(): void {
    if(this.routerSub){
      this.routerSub.unsubscribe();
    }
  }

  private verifyIfLoginPage(url: string){
    if(url.includes('/login')){
      return true;
    }
    return false;
  }
}
