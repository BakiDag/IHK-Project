import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { Constants } from '../Helper/constants';
import { Mitarbeiter } from '../Models/mitarbeiter';



@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private router: Router) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
   
    const mitarbeiter = JSON.parse(localStorage.getItem(Constants.MITARBEITER_KEY)) as Mitarbeiter;
    if (mitarbeiter && mitarbeiter.email) {     
      return true;
    } else {
      localStorage.removeItem(Constants.MITARBEITER_KEY);
     this.router.navigate(["login"]);          
      return false;
    }
  }
}