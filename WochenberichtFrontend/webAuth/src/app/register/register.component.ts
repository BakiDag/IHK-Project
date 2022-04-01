import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { Role } from '../Models/role';
import * as $ from 'jquery';
import { LoginServiceService } from '../services/login-service.service';
import { RegisterService } from './register.service';
import { HttpRequestService } from '../services/http-request.service';




@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  public roles:Role[] = [];
  
  public auswahl:number
  
    
        
    public user: any[] = [
      { vorname: "",nachname: "",email: "", benutzerName: "", role:"",password: "" }  
    ];

  public registerForm=this.formBuilder.group({
    Vorname:['',[Validators.required]],
    Nachname:['',[Validators.required]],
    email:['',[Validators.email,Validators.required]],
    password:['',Validators.required]
  })
  public registerForm2=this.formBuilder.group({
    Vorname:['',[Validators.required]],
    Nachname:['',[Validators.required]],
    email:['',[Validators.email,Validators.required]],
    password:['',Validators.required]
  })
  public roterAlarm: boolean = false;
  public gurenerAlarm: boolean = false;
  public bootstrapAlarmNachricht= null;

    constructor(
      private formBuilder:FormBuilder,      
      private request:HttpRequestService,
      private regDaten :RegisterService
      ) { 
    
    }
  
    ngOnInit(): void 
    {
      this.auswahl=0;
      this.getAllRoles();

    }
    
  getAllRoles()
  {
    this.request.getAllRoles().subscribe(roles=>{
      this.roles = roles;
    });
  }

  // pruefen ob noch benoetigt wird
  onRoleChange(role:string)
  {
    this.roles.forEach(x=>{
      if(x.role == role)
      {
        x.isSelected=true;
      }
      else
      {
        x.isSelected=false;
      }
    })
  }
  selected( )
{
  switch (Number(this.auswahl))
  {
  case 0:      
  this.user[0].vorname = ""
  this.user[0].nachname=""
  this.user[0].email=""
  this.user[0].benutzerName=""
  this.user[0].role = ""
  this.user[0].password = ""

    break;

  case 1: //Admin 
  this.user[0].vorname = this.regDaten.userLogin[this.auswahl].vorname
  this.user[0].nachname=this.regDaten.userLogin[this.auswahl].nachname  
  this.user[0].email=this.regDaten.userLogin[this.auswahl].email  
  this.user[0].benutzerName=this.regDaten.userLogin[this.auswahl].benutzerName  
  this.user[0].role = this.regDaten.userLogin[this.auswahl].role  
  this.user[0].password = this.regDaten.userLogin[this.auswahl].password  

 

  
    break;

  case 2: //Ausbilder
    this.user[0].vorname = this.regDaten.userLogin[this.auswahl].vorname
  this.user[0].nachname=this.regDaten.userLogin[this.auswahl].nachname  
  this.user[0].email=this.regDaten.userLogin[this.auswahl].email  
  this.user[0].benutzerName=this.regDaten.userLogin[this.auswahl].benutzerName  
  this.user[0].role = this.regDaten.userLogin[this.auswahl].role  
  this.user[0].password = this.regDaten.userLogin[this.auswahl].password  

  
      break;
    case 3://Auszubildender
    this.user[0].vorname = this.regDaten.userLogin[this.auswahl].vorname
    this.user[0].nachname=this.regDaten.userLogin[this.auswahl].nachname  
    this.user[0].email=this.regDaten.userLogin[this.auswahl].email  
    this.user[0].benutzerName=this.regDaten.userLogin[this.auswahl].benutzerName  
    this.user[0].role = this.regDaten.userLogin[this.auswahl].role  
    this.user[0].password = this.regDaten.userLogin[this.auswahl].password  
      break;
      
    default:      
     
    break;
  }
  
}
onSubmit()
{      
 
  
  
  this.roles[0].role=this.user[0].role
  
  this.request.register
  (
    this.user[0].vorname,
    this.user[0].nachname,
    this.user[0].email,
    this.user[0].password,
    this.user[0].role).subscribe((data)=>{
    //this.roles.filter(x=>x.isSelected)[0].role)
    
    
    //this.mitarbeiterServie.register(Vorname,Nachname,email,password,this.roles.filter(x=>x.isSelected)[0].role).subscribe((data)=>{


    let errorMessage=data.responseMessage
    this.roles.forEach(x=>x.isSelected=false);
    if(data.responseMessage!="")
    {
      this.gurenerAlarm = true          
      this.bootstrapAlarmNachricht= data.responseMessage
      
      alert(data.responseMessage)
      
    }
    else
    {
      this.roterAlarm = true
      this.bootstrapAlarmNachricht= data.dateSet
      let ResponseCode= data.responseCode      
      
      alert(data.responseMessage)
    }
    
  },error=>{
    alert("Fehler: "+error)
    console.log("Fehler: ",error)
    error.forEach(element => {
      console.log(element)
    });
  })
  
  
}
  }