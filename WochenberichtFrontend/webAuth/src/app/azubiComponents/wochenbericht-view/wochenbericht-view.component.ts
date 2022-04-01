import { Component, OnInit } from '@angular/core';
import { DatePipe, ViewportScroller } from "@angular/common";
import { Mitarbeiter } from 'src/app/Models/mitarbeiter';
import { StatusAusbilder, StatusAzubi as StatusTrainee, Wochenbericht } from 'src/app/Models/wochenbericht';
import { HttpRequestService } from 'src/app/services/http-request.service';
import { JWTTokenService } from 'src/app/services/jwttoken.service';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { WochenberichtService } from 'src/app/services/wochenbericht.service';
import { Router } from '@angular/router';
import { Ausbilder } from 'src/app/Models/Ausbilder';
import { Auszubildender } from 'src/app/Models/Auszubildender';
import { MessageCenterService } from 'src/app/services/message-center.service';


@Component({
  selector: 'app-wochenbericht-view',
  templateUrl: './wochenbericht-view.component.html',
  styleUrls: ['./wochenbericht-view.component.scss']
})
export class WochenberichtViewComponent implements OnInit   {
  togleInfo : Boolean =false;
 

  //public  _ID : number = new Wochenbericht().ID;// macht 0
  _WeeklyReportId : number;
  _InstructorId : number;
  _TraineeId : number;
  _Calenderweek : number;
  _DateFrom : Date=null;
  _DateTo : Date=null;
  _Page : number;
  _SigningTrainee : string ;
  _SigningInstructor : string;
  
  StatusTrainee : StatusTrainee  = StatusTrainee.InBearbeitung;
  statusTraineeInEditing : Boolean = true;
  statusTraineeisSigned : boolean = false;
  
  StatusInstructor? :StatusAusbilder  = null;
  statusInstructorInControl : Boolean = false;
  statusAusbilderIstUnterschrieben : Boolean = false;

  

  MondayReport ="";
  TuesdayReport ="";
  WednesdayReport ="";
  ThursdayReport ="";
  FridayReport ="";
  
  MondaySaved : Boolean = false;;
  TuesdaySaved : Boolean = false;
  WednesdaySaved : Boolean = false;
  ThursdaySaved : Boolean = false;
  FridaySaved : Boolean = false;
  
  WeeklyReport: Wochenbericht[] = [];
  
  
  HoursMonday: number=0;
  HoursTuesday: number=0;
  HoursWednesday: number=0;
  HoursThursday: number=0;
  HoursFriday: number=0;

  WeeklyWorkHours : number=0;
  
  succesfullSend= false;
  constructor(
    public message : MessageCenterService,
    private request:WochenberichtService, public tokenInfo:JWTTokenService, 
    public form:FormBuilder,private scroller: ViewportScroller, private router: Router) {
    
   }

  ngOnInit() {

    // request benoetigt fuer Satus Wochenbericht w
    //azubi darf nicht editieren wenn bereits unterschrieben von azubi
    if (this.tokenInfo.Role== "Auszubildender" ) 
    {
      console.log("Azubi  eineloggt"); 
      this.StatusTrainee == StatusTrainee.InBearbeitung;
      this.message.AzubiBearbeitet= true;
    }
    else if(this.tokenInfo.Role== "Ausbilder")
    {
      console.log("Azubi nicht eineloggt");
      
    }
    
    if(this.StatusTrainee == StatusTrainee.InBearbeitung)
    {
      console.log("ist in Bearbeitung durch Azubi");
      this.message.AzubiBearbeitet= true;
      
    }
    else
    {
      console.log("nicht in bearbeitung durch Azubi");
      
    }
    
}
toggleInfoButton()
{  
  this.togleInfo=!this.togleInfo
}
  

SaveMonday()
{  
  if (this.HoursMonday<= 8 && this.HoursMonday >= 0 && 
    this._DateFrom != null && this._DateTo != null && this.MondayReport != "") {    
    this.MondaySaved  = true;
    alert("Bericht gespeichert")
  }
  else
  {    
    alert("Bitte Datum Von und Datum Bis setzen! \nStunden pro Tag duerfen 0 nicht unterschreiten !\nStunden Pro Tag duerfen 8 Stunden nicht ueberschreiten")
    this.HoursMonday = 0    
  }
  
  //request senden zum speichern
}
SaveDien()
{
  if (this.HoursTuesday<= 8 && this.HoursTuesday >= 0 && 
    this._DateFrom != null && this._DateTo != null && this.TuesdayReport != "") {    
    this.TuesdaySaved  = true;
    alert("Bericht gespeichert")
  }
  else
  {    
    alert("Bitte Datum Von und Datum Bis setzen! \nStunden pro Tag duerfen 0 nicht unterschreiten !\nStunden Pro Tag duerfen 8 Stunden nicht ueberschreiten")
    this.HoursTuesday = 0    
  }
  //request senden zum speichern
}
SaveMit()
{
  if (this.HoursWednesday<= 8 && this.HoursWednesday >= 0 && 
    this._DateFrom != null && this._DateTo != null && this.WednesdayReport != "") {    
    this.WednesdaySaved  = true;
    alert("Bericht gespeichert")
  }
  else
  {    
    alert("Bitte Datum Von und Datum Bis setzen! \nStunden pro Tag duerfen 0 nicht unterschreiten !\nStunden Pro Tag duerfen 8 Stunden nicht ueberschreiten")
    this.HoursWednesday = 0    
  }
  //request senden zum speichern
}
SaveThursday()
{
  if (this.HoursThursday<= 8 && this.HoursThursday >= 0 && 
    this._DateFrom != null && this._DateTo != null && this.ThursdayReport != "") {    
    this.ThursdaySaved  = true;
    alert("Bericht gespeichert")
  }
  else
  {    
    alert("Bitte Datum Von und Datum Bis setzen! \nStunden pro Tag duerfen 0 nicht unterschreiten !\nStunden Pro Tag duerfen 8 Stunden nicht ueberschreiten")
    this.HoursThursday = 0    
  }
  //request senden zum speichern
}
SaveFriday()
{
  if (this.HoursFriday<= 8 && this.HoursFriday >= 0 && 
    this._DateFrom != null && this._DateTo != null && this.FridayReport != "") {    
    this.FridaySaved  = true;
    alert("Bericht gespeichert")
  }
  else
  {    
    alert("Bitte Datum Von und Datum Bis setzen! \nStunden pro Tag duerfen 0 nicht unterschreiten !\nStunden Pro Tag duerfen 8 Stunden nicht ueberschreiten")
    this.HoursFriday = 0    
  }
  //request senden zum speichern  
}

Succeed()
{
    this.succesfullSend= true;
    this._SigningTrainee = this.tokenInfo.FirstName+" "+this.tokenInfo.LastName+" "+ new Date().toLocaleString()
    this.StatusTrainee = StatusTrainee.IstUnterschrieben;
    this.StatusInstructor= StatusAusbilder.InÜberprüfung;
    console.clear();      
    console.log("ID: "+this._WeeklyReportId);
    console.log("AusbilderID: "+this._InstructorId);
    console.log("AuszubildendenID: "+this._TraineeId);
    console.log("_Kalenderwoche: "+this._Calenderweek);
    console.log("Datum Von: "+this._DateFrom);
    console.log("Datum bis: "+this._DateTo);
    console.log("Seite: "+this._Page);
    console.log("Montag: "+this.MondayReport);
    console.log("Dienstag: "+this.TuesdayReport);
    console.log("Mittwoch: "+this.WednesdayReport);
    console.log("Donnerstag: "+this.ThursdayReport);
    console.log("Freitag: "+this.FridayReport);
    console.log("Istunterschrieben: "+this.StatusTrainee);
    console.log("InÜberprüfung: "+this.StatusInstructor);
    console.log("Unterschrift Azubi: "+this._SigningTrainee);
    console.log("Unterschrift Ausbilder: "+ this._SigningInstructor);
    
    
    this.request.SaveWochenbericht(
    this._WeeklyReportId,
    this._InstructorId ,
    this._TraineeId,
    this._Calenderweek, 
    this._DateFrom, 
    this._DateTo, 
    this._Page , 
    this.MondayReport ,
    this.TuesdayReport,
    this.WednesdayReport,
    this.ThursdayReport  , 
    this.FridayReport ,
    this.StatusTrainee, 
    this.StatusInstructor, 
    this._SigningTrainee,
    this._SigningInstructor).subscribe((data)=>{
      
    
    if(data.responseMessage!="")
    {
      alert("Message: "+data.responseMessage+" ResponsCode: "+ data.responseCode)      
    }
    else
    {      
      alert("Message: "+data.responseMessage+" ResponsCode: "+ data.responseCode)
    }
    
  },error=>{
    alert("Fehler: "+error)
    console.log("Fehler",error)
    error.forEach(element => {
      console.log(element)
    });
  })
    alert("Bericht erfolgreich eingesendet")//sollte versetzt werden    
    this.router.navigate(["/AzubiMessageCenter"]); //sollte entfernt werden
}

Send()
{
  //Berechne Arbeitszeit der Woche
  this.WeeklyWorkHours=this.HoursMonday+this.HoursTuesday+this.HoursWednesday+this.HoursThursday+this.HoursFriday
 
  if(this.WeeklyWorkHours > 40 && this.WeeklyWorkHours < 0)
  {
    alert("Wochenarbeitszeit überschreitet 40 Stunden oder ist eine negative Zahl\nDieArbeitszeiten werden auf null gesetzt\nBitte Korrektur vornehmen")
    this.togleInfo=true;    
    this.WeeklyWorkHours=0;
    this.HoursMonday=0;
    this.HoursTuesday=0;
    this.HoursWednesday=0;
    this.HoursThursday=0;
    this.HoursFriday=0;    
  } 
  else
  {
    if(this.MondaySaved  == true && this.TuesdaySaved  == true && this.WednesdaySaved  == true && 
    this.ThursdaySaved == true && this.FridaySaved  == true &&this._DateFrom != null 
    && this._DateTo != null && this.WeeklyWorkHours <= 40 && this.MondayReport !="" && this.TuesdayReport !="" && 
    this.WednesdayReport!="" && this.ThursdayReport!="" && this.FridayReport!="")
    {
        this.Succeed();
    }  
    else
    {
      alert("Bitte prüfen Sie Ihr Bericht:\nDatum 'Woche von:' Datum 'Bis:' muss gesetzt sein\nDie Tagesberichte dürfen nicht leer sein.\nDie Tagesberichte müssen täglich gespeichert werden\nDie Wochenarbeitszeit darf 40 Stunden nicht überschreiten")
      this.togleInfo=true;    
    this.WeeklyWorkHours=0;
    this.HoursMonday=0;
    this.HoursTuesday=0;
    this.HoursWednesday=0;
    this.HoursThursday=0;
    this.HoursFriday=0;
    }
  }

   
  
}
}
