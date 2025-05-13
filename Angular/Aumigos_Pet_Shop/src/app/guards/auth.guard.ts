import { Injectable, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { AuthenticationService } from "../Services/authentication.service";
import { Subscription } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class authGuard implements CanActivate, OnInit, OnDestroy{
  loged:boolean = false;
  sub:Subscription

  constructor(private router: Router, private auth: AuthenticationService){}

  ngOnInit(): void {
    this.sub = this.auth.loged$.subscribe(v => this.loged = v);
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if(this.auth.isLoged()){
      return true;
    }
    
    this.router.navigate(['/login']);
    return false;
  }

  ngOnDestroy(): void {
    if(this.sub){
      this.sub.unsubscribe();
    }
  }
}