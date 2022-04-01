import { Component, OnInit } from '@angular/core';
import { Mitarbeiter } from 'src/app/Models/mitarbeiter';
import { HttpRequestService } from 'src/app/services/http-request.service';


@Component({
  selector: 'app-azubi-management',
  templateUrl: './azubi-management.component.html',
  styleUrls: ['./azubi-management.component.scss']
})
export class AzubiManagementComponent implements OnInit {  
  sucheVorname : string ;
  sucheNachname: string;
  sucheEmail : string;  
  public azubiList: Mitarbeiter[] = [];
  constructor(private request:HttpRequestService) { }

  ngOnInit(): void 
  {
    this.getAuszubildende();
    
  }
  
  getAuszubildende()
{
  this.request.getAuszubildende().subscribe((data: Mitarbeiter[]) => {    
    this.azubiList = data;
    if(data.length==0)
    {
      alert("Keine Daten erhalten")  
    }
    
  })
}
loeschAzubi( mitarbeiter: Mitarbeiter,i)
{  
  let text ="Soll der Datensatz wirklich gelöscht werden?"    
  if (confirm(text) == true) 
  {
    this.azubiList.splice(i, 1);
    this.request.loeschMitarbeiter(mitarbeiter).subscribe(data => {
    if(data.responseCode==1)
    {
      alert("Message: "+data.responseMessage+"\nResponse Code: " +data.responseCode);   
      
    }
    else
    {
      alert("Message: " + data.responseMessage+"\nResponse Code: " + data.responseCode);
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
    this.azubiList.splice(i, 1);
    text = "";
    this.azubiList.splice(i, 1);
    this.request.deleteContact(email).subscribe(data => {
    console.log(data);
    if(data.responseCode==1)
    {
      alert("Message: "+data.responseMessage+"\nResponse Code: " +data.responseCode);   
    }
    else
    {
      alert("Message: " + data.responseMessage+"\nResponse Code: " + data.responseCode);
    }
    });
  
  } 
  else 
  {
    alert("Keine Aenderungen durchgeführt")
  }
  
  
}
}
