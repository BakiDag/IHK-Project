import { Injectable } from '@angular/core';
import { Constants } from '../Helper/constants';

@Injectable({
  providedIn: 'root'
})
export class JWTTokenService {
  private MitarbeiterToken=JSON.parse(localStorage.getItem(Constants.MITARBEITER_KEY));
  
  public FirstName =this.MitarbeiterToken?.vorname
  public LastName=this.MitarbeiterToken?.nachname
  public email=this.MitarbeiterToken?.email
  public BenutzerName= this.MitarbeiterToken?.userName
  public Role=this.MitarbeiterToken?.role
  public token=this.MitarbeiterToken?.token
  public password=this.MitarbeiterToken?.password
  constructor() { }
  
}
