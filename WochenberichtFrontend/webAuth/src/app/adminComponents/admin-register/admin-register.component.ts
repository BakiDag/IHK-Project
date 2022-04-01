import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpRequestService } from 'src/app/services/http-request.service';

@Component({
  selector: 'app-admin-register',
  templateUrl: './admin-register.component.html',
  styleUrls: ['./admin-register.component.scss']
})
export class AdminRegisterComponent implements OnInit {
  public Vorname
  public Nachname
  public email
  public password
  public auswahl: number
  public userSignUp: any[] = [
    {  role:null },
    { role:"Admin" },
    {  role:"Ausbilder" },
    {  role:"Auszubildender" }  
    ];
    public user: any[] = [
      {  role:"" }  
    ];
  
  public registerForm=this.formBuilder.group({
    Vorname:['',[Validators.required]],
    Nachname:['',[Validators.required]],
    email:['',[Validators.email,Validators.required]],
    password:['',Validators.required]    
  })

  constructor(
    private formBuilder:FormBuilder,   
    private router: Router, 
    private request:HttpRequestService){ }
  
  ngOnInit(): void 
  {
    
  }
  selected()
  {
    switch (Number(this.auswahl))
  {
  case 0:              
  this.user[0].role = null;
      alert("Ungültige Auswahl")
    break;

  case 1://Admin       
      this.user[0].role=this.userSignUp[1].role      
      
    break;

  case 2://Ausbilder  
  this.user[0].role=this.userSignUp[2].role      
      
      break;
    case 3://Auszubildender
    this.user[0].role=this.userSignUp[3].role      
      
    
      break;

    default:           
    break;
  }
    
  }
    onSubmit()
    { 
      this.request.register(this.Vorname,this.Nachname,this.email,this.password,this.user[0].role).subscribe((data)=>{
        this.user[0].role=null;
        this.Vorname=null;
        this.Nachname=null;
        this.email=null;
        this.password=null;
        if(data.responseMessage!="")
        {
          alert("Message: "+ data.responseMessage+"\nResponse Code: "+data.responseCode) 
        }
        else
        {          
          alert("Meeage: "+data.responseMessage+ "\nResponse Code: "+data.responseCode)          
        }        
      },error=>{
        alert("Fehler: "+error)        
        error.forEach(element => {
          console.log(element)
        });
      })      
    }
}