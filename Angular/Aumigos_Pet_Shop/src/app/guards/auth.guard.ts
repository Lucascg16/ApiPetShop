import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";

export class authGuard implements CanActivate{
  constructor(private router: Router){}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if(localStorage.getItem('currentUser')){
      return true;
    }

    this.router.navigate(['/home']);
    return false;
  }
}