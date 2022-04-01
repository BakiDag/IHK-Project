import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MessageCenterService {

  public AzubiBearbeitet : Boolean 
  public AzubiSigned:Boolean
  public AusbilderPrueft : Boolean
  public AusbilderSigned:Boolean
  public AusbilderDenyrelease : Boolean
  
  constructor() { }
}
