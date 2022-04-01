import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { responseCode } from 'src/app/enums/responseCode';
import { Mitarbeiter } from 'src/app/Models/mitarbeiter';
import { Role } from 'src/app/Models/role';
import { HttpRequestService } from 'src/app/services/http-request.service';



@Component({
  selector: 'app-mitarbeiter-management',
  templateUrl: './mitarbeiter-management.component.html',
  styleUrls: ['./mitarbeiter-management.component.scss']
})
export class MitarbeiterManagementComponent implements OnInit {
  searchRole: string ;
  sucheVorname : string ;
  sucheNachname: string;
  sucheEmail : string;
  tableIndex: number;
  
  public roles:Role[] = [];
  public MitarbeiterListe: Mitarbeiter[] = [];

  constructor(
    private formBuilder:FormBuilder,   
    private router: Router, 
    private request:HttpRequestService){ }
    getAllRoles()
  {
    this.request.getAllRoles().subscribe(_roles=>{
      this.roles = _roles;
    });
  }
 
  ngOnInit(): void 
  {
    this.getAllRoles();
    console.log(this.getAllMitarbeiter());  
  }
getAllMitarbeiter()
{  
    this.request.getAllMitarbeiter().subscribe((data: Mitarbeiter[]) => {    
    this.MitarbeiterListe = data;
    if(data.length==0 && responseCode.OK)
    {
      alert("Keine Daten erhalten")      
    }    
    console.log(data);
    
  })  
  
}

loeschMitarbeiter( mitarbeiter: Mitarbeiter,i)
{  
  let text ="Soll der Datensatz wirklich gelöscht werden?"    
  if (confirm(text) == true) 
  {
    this.MitarbeiterListe.splice(i, 1);
    this.request.loeschMitarbeiter(mitarbeiter).subscribe(data => {
    if(data.responseCode==1)
    {
      alert("Message: "+data.responseMessage+"\nResponse Code: " +data.responseCode);   
    }
    else
    {
      alert("Keine Aenderungen durchgeführt")
    }
  });
  
  } 
  
else 
{
  alert("Keine Aenderungen durchgeführt")
}
}
deleteContact(email: string,i)
{
  let text ="Soll der Datensatz wirklich gelöscht werden?"    
  //this.MitarbeiterListe.splice(tableIndex, 1);
  //let text = "Press a button!\nEither OK or Cancel.";
  if (confirm(text) == true) {
    this.MitarbeiterListe.splice(i, 1);
    text = "";
    this.MitarbeiterListe.splice(i, 1);
    this.request.deleteContact(email).subscribe(data => {
    console.log(data);
    if(data.responseCode==1)
    {
      alert("Message: "+data.responseMessage+"\nResponse Code: " +data.responseCode);   
    }
    else
    {
      alert("Keine Aenderungen durchgeführt")
    }
    });
  
  } 
  else 
  {
    alert("Keine Aenderungen durchgeführt")
  }
  
  
}


}