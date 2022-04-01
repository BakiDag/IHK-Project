import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  public userLogin: any[] = [

    { vorname: "",nachname: "",email: "", benutzerName: "", role:"",password: "" },
    { vorname: "Max",nachname: "Mustermann",email: "admin@gmail.com", benutzerName: "admin@gmail.com", role:"Admin",password: "12345678910!aA!" },
    { vorname: "Uwe",nachname: "Meier",email: "ausbilder@gmail.com", benutzerName: "ausbilder@gmail.com", role:"Ausbilder",password: "12345678910!aA!" },
    { vorname: "Ruwen",nachname: "Müller",email: "auszubildender@gmail.com", benutzerName: "auszubildender@gmail.com", role:"Auszubildender",password: "12345678910!aA!" }  
    ];
  constructor() { }
}
