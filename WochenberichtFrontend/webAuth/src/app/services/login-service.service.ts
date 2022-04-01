import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginServiceService {

  constructor(private router: Router) { }
  
  public auswahl:number
  
  public userLogin: any[] = [
    

    { id: 0 , vorname: "",nachname: "",email: "", benutzerName: "", role:"",password: "" }  ,
  {  id: 1 ,vorname: "Max",nachname: "Mustermann",email: "admin@gmail.com", benutzerName: "admin@gmail.com", role:"Admin",password: "12345678910!aA!" },
  { id: 2 , vorname: "Uwe",nachname: "Meier",email: "ausbilder@gmail.com", benutzerName: "ausbilder@gmail.com", role:"Ausbilder",password: "12345678910!aA!" },
  {  id: 3 ,vorname: "Ruwen",nachname: "Müller",email: "auszubildender@gmail.com", benutzerName: "auszubildender@gmail.com", role:"Auszubildender",password: "12345678910!aA!" }  
  ];
  
      
  public user: any[] = [
     { id: 0 ,vorname: "",nachname: "",email: "", benutzerName: "", role:"",password: null }  
    
  ];

   selected()
  {
    switch (Number(this.auswahl))
  {
  case 0:              
      //alert("Ungültige Auswahl")
    break;

  case 1://Admin        
      this.user[0].id=this.userLogin[this.auswahl].id
      this.user[0].vorname=this.userLogin[this.auswahl].vorname
      this.user[0].nachname=this.userLogin[this.auswahl].nachname
      this.user[0].email=this.userLogin[this.auswahl].email
      this.user[0].benutzerName=this.userLogin[this.auswahl].benutzerName
      this.user[0].role = this.userLogin[this.auswahl].role
      this.user[0].password = this.userLogin[this.auswahl].password
      
    break;

  case 2://Ausbilder  
  this.user[0].id=this.userLogin[this.auswahl].id
  this.user[0].vorname=this.userLogin[this.auswahl].vorname
  this.user[0].nachname=this.userLogin[this.auswahl].nachname
  this.user[0].email=this.userLogin[this.auswahl].email
  this.user[0].benutzerName=this.userLogin[this.auswahl].benutzerName
  this.user[0].role = this.userLogin[this.auswahl].role
  this.user[0].password = this.userLogin[this.auswahl].password

      
  
      break;
    case 3://Auszubildender
    this.user[0].id=this.userLogin[this.auswahl].id
      this.user[0].vorname=this.userLogin[this.auswahl].vorname
      this.user[0].nachname=this.userLogin[this.auswahl].nachname
      this.user[0].email=this.userLogin[this.auswahl].email
      this.user[0].benutzerName=this.userLogin[this.auswahl].benutzerName
      this.user[0].role = this.userLogin[this.auswahl].role
      this.user[0].password = this.userLogin[this.auswahl].password
      
      break;

    default:           
    break;
  }
    
  }
 

}
