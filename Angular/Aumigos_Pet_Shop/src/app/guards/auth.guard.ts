import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class authGuard implements CanActivate{
  constructor(private router: Router){}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    try{
      if(sessionStorage?.getItem('currentUser')){
        return true;
      }
    }catch{}


    this.router.navigate(['/home']);
    return false;
  }
}