import { responseModel } from './../Models/responseModel';

import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { responseCode } from '../enums/responseCode';
import { stringify } from 'querystring';
import { keyframes } from '@angular/animations';
import { Mitarbeiter } from '../Models/mitarbeiter';

import { LoginServiceService } from '../services/login-service.service';
import { HttpRequestService } from '../services/http-request.service';
import { JWTTokenService } from '../services/jwttoken.service';
import { Constants } from '../Helper/constants';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public loginForm = this.formBuilder.group({
    email: ['', [Validators.email, Validators.required]],
    password: ['', Validators.required]
  })
  
  public auswahl:number
  
        
    public user: any[] = [
       { id: 0 , vorname: "",nachname: "",email: "", benutzerName: "", dateCreated: 0,this_dateModified: 0, role:"",password: "" }  
      
    ];
    
  constructor(private formBuilder: FormBuilder, 

    private request:HttpRequestService,
    private router: Router,    
    public loggedInUser: LoginServiceService ) { }

  ngOnInit(): void {
          this.auswahl=0
  }
  selected( )
{
  switch (Number(this.auswahl))
  {
  case 0:    
  this.user[0].id=0
  this.user[0].vorname=""
  this.user[0].nachname=""
  this.user[0].email=""
  this.user[0].benutzerName=""
  this.user[0].role = ""
  this.user[0].password = ""
      
    break;

  case 1://Admin        
      this.user[0].id=1
      this.user[0].vorname=this.loggedInUser.userLogin[this.auswahl].vorname
      this.user[0].nachname=this.loggedInUser.userLogin[this.auswahl].nachname
      this.user[0].email=this.loggedInUser.userLogin[this.auswahl].email
      this.user[0].benutzerName=this.loggedInUser.userLogin[this.auswahl].benutzerName
      this.user[0].role = this.loggedInUser.userLogin[this.auswahl].role
      this.user[0].password = this.loggedInUser.userLogin[this.auswahl].password      
    break;

  case 2://Ausbilder  
  this.user[0].id=2
  this.user[0].vorname=this.loggedInUser.userLogin[this.auswahl].vorname
  this.user[0].nachname=this.loggedInUser.userLogin[this.auswahl].nachname
  this.user[0].email=this.loggedInUser.userLogin[this.auswahl].email
  this.user[0].benutzerName=this.loggedInUser.userLogin[this.auswahl].benutzerName
  this.user[0].role = this.loggedInUser.userLogin[this.auswahl].role
  this.user[0].password = this.loggedInUser.userLogin[this.auswahl].password
      break;
    case 3://Auszubildender
    this.user[0].id=3
    this.user[0].vorname=this.loggedInUser.userLogin[this.auswahl].vorname
    this.user[0].nachname=this.loggedInUser.userLogin[this.auswahl].nachname
    this.user[0].email=this.loggedInUser.userLogin[this.auswahl].email
    this.user[0].benutzerName=this.loggedInUser.userLogin[this.auswahl].benutzerName
    this.user[0].role = this.loggedInUser.userLogin[this.auswahl].role
    this.user[0].password = this.loggedInUser.userLogin[this.auswahl].password
      break;

    default:      
     
    break;
  }
  
}

  
  onSubmit() {   

//aufbewahren erstmal
//    let email = this.loginForm.controls["email"].value;
  //  let password = this.loginForm.controls["password"].value;  
    if(this.user[0].email==""||this.user[0].password =="")
    {
      return false;
    }
      return this.request.login(this.user[0].email, this.user[0].password).subscribe((data: responseModel)=>
      {
     
      if(data.responseCode==1)
      {              
        localStorage.setItem(Constants.MITARBEITER_KEY, JSON.stringify(data.dateSet));                
        let datas=data.dateSet as Mitarbeiter;
        if (datas.role=="Admin") 
        { 
          this.router.navigate(["/mitarbeiter-management"]);
          //console.log(datas);

          this.user[0]._dateCreated= new Date(Date.now())
          this.user[0]._dateModified= new Date(Date.now())

          
          
          alert("Welcome "+datas.role+"\nMessage: " + data.responseMessage)
          
         
            
        }
        else if(datas.role=="Ausbilder")
        {
          
          this.router.navigate(["/azubi-management"]);
          alert("Welcome "+datas.role+"\nMessage: " + data.responseMessage)
          
        }
        else
        {
          
          this.router.navigate(["/azubi-dashboard"]);
          alert("Welcome "+datas.role+"\nMessage: " + data.responseMessage)          
          
        }                
      }
      else if(data.responseCode==0 || data.responseCode==2)
      {
        localStorage.removeItem(Constants.MITARBEITER_KEY);
        
        //console.log("response: ", data.responseMessage);          
        alert("Message: "+ data.responseMessage+"\nResponse Code: " +data.responseCode);          
      }
      
      },error=>{
        
      alert("Fehler: " +error.message)      
      });
     
  }
}
