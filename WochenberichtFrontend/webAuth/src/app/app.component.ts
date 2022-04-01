
  import { Component } from '@angular/core';
  import { Router } from '@angular/router';
  import { Constants } from './Helper/constants';
  import { Mitarbeiter } from './Models/mitarbeiter';  
  import { HttpRequestService } from './services/http-request.service';
  import { JWTTokenService } from './services/jwttoken.service';
  
  
  
  @Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
  })
  export class AppComponent {
    title = 'webAuth';
    isExpanded = false;
    constructor(private router:Router,private request:HttpRequestService,private token: JWTTokenService){
    if(this.isMitarbeiterLogin)
    {
      this.router.navigate(['/']);
    }
  }
  
  
    collapse() {
      this.isExpanded = false;
    }
  
    toggle() {
      this.isExpanded = !this.isExpanded;
    }
    onLogout()
    {
      localStorage.removeItem(Constants.MITARBEITER_KEY);
      this.request.logout(this.token.email, this.token.password).subscribe(data => {
        
      if (data.responseCode==1) {
             
        alert("Message: " + data.responseMessage)      
        
      }
      else
      {    
        alert("Message: " + data.responseMessage)      
      }
      }
    );
      
      
      
    }
    get isMitarbeiterLogin()
    {
      const mitarbeiter = localStorage.getItem(Constants.MITARBEITER_KEY);
      
      return mitarbeiter && mitarbeiter.length>0;
     
    }
    get mitarbeiter():Mitarbeiter
    {
      return JSON.parse(localStorage.getItem(Constants.MITARBEITER_KEY)) as Mitarbeiter;
    }
    get Admin():boolean
    {
      return this.mitarbeiter.role=="Admin";
    }
    get Ausbilder():boolean
    {
      return this.mitarbeiter.role=="Ausbilder";
    }
    get Auszubildender():boolean
    {
      return this.mitarbeiter.role=="Auszubildender";
    }
    get isMitarbeiter():boolean
    {
      if(this.mitarbeiter.role=="Ausbilder" || this.mitarbeiter.role=="Admin"|| this.mitarbeiter.role=="Auszubildender")
      {
        return true
      }
      else
      {
        return false
      }
      //return false;
    }
  
  }