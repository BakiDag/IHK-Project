import { ViewportScroller } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { StatusAzubi, StatusAusbilder, Wochenbericht } from 'src/app/Models/wochenbericht';
import { JWTTokenService } from 'src/app/services/jwttoken.service';
import { MessageCenterService } from 'src/app/services/message-center.service';
import { WochenberichtService } from 'src/app/services/wochenbericht.service';

@Component({
  selector: 'app-report-control',
  templateUrl: './report-control.component.html',
  styleUrls: ['./report-control.component.scss']
})
export class ReportControlComponent implements OnInit {
  
  

  //public  _ID : number = new Wochenbericht().ID;// macht 0
  WeeklyReport_Id : number;
  Instructor_ID : number;
  Trainee_ID : number;
  _Calenderweek : number;
  _DateFrom : Date=null;
  _DateTil : Date=null;
  _Page : number =0;
  _SigningTrainee : string ;
  _SigningInstructor : string;
  
  StatusTrainee : StatusAzubi  = StatusAzubi.InBearbeitung;
  statusAzubiInBearbeitung : Boolean = true;
  statusAzubiHatUnterschrieben : boolean = false;
  
  StatusInstructor? :StatusAusbilder  = null;
  statusAusbilderInÜberprüfung : Boolean = false;
  statusAusbilderIstUnterschrieben : Boolean = false;

  

  MondayReport ="";
  MondayComment ="";
  TuesdayReport ="";
  TuesdayComment="";
  WednesdayReport ="";
  WednesdayComment ="";
  ThursdayReport ="";
  ThursdayComment="";
  FridayReport="";
  FridayComment ="";
    
  MontagGespeichert : Boolean = false;;
  DienstagGespeichert : Boolean = false;
  MittwochGespeichert : Boolean = false;
  DonnerstagGespeichert : Boolean = false;
  FreitagGespeichert : Boolean = false;
  
  Wochenbericht: Wochenbericht[] = [];
  
  
  HoursMon: number=0;
  HoursTuesday: number=0;
  HoursWednesday: number=0;
  HoursThursday: number=0;
  HoursFriday: number=0;

  WeeklyWorkHours : number=40;
  
  erfolgreichGesendet= false;
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
      this.StatusTrainee == StatusAzubi.InBearbeitung;
      this.message.AzubiBearbeitet= true;
    }
    else if(this.tokenInfo.Role== "Ausbilder")
    {
      console.log("Azubi nicht eineloggt");
      
    }
    
    if(this.StatusTrainee == StatusAzubi.InBearbeitung)
    {
      console.log("ist in Bearbeitung durch Azubi");
      this.message.AzubiBearbeitet= true;
      
    }
    else
    {
      console.log("nicht in bearbeitung durch Azubi");
      
    }
    
}


DatumsCheck()
{
  if(this._DateFrom == null || this._DateTil ==null)
  {
    alert("Bitte Datum Von und Datum Bis setzen")
  }
}

Succeed()
{
    this.erfolgreichGesendet= true;
    this._SigningTrainee = this.tokenInfo.FirstName+" "+this.tokenInfo.LastName+" "+ new Date().toLocaleString()
    this.StatusTrainee = StatusAzubi.IstUnterschrieben;
    this.StatusInstructor= StatusAusbilder.InÜberprüfung;
    console.clear();      
    console.log("ID: "+this.WeeklyReport_Id);
    console.log("AusbilderID: "+this.Instructor_ID);
    console.log("AuszubildendenID: "+this.Trainee_ID);
    console.log("_Kalenderwoche: "+this._Calenderweek);
    console.log("Datum Von: "+this._DateFrom);
    console.log("Datum bis: "+this._DateTil);
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
    this.WeeklyReport_Id,
    this.Instructor_ID ,
    this.Trainee_ID,
    this._Calenderweek, 
    this._DateFrom, 
    this._DateTil, 
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

Decline()
{
  alert("Bericht zur Korrektur zurueck")
}
Release()
{
  //Berechne Arbeitszeit der Woche
  this.WeeklyWorkHours=this.HoursMon+this.HoursTuesday+this.HoursWednesday+this.HoursThursday+this.HoursFriday
 
  if(this.WeeklyWorkHours>40)
  {
    alert("Wochenarbeitszeit überschreitet 40 Stunden\nDieArbeitszeiten werden auf null gesetzt\nBitte Korrektur vornehmen")
     
    this.WeeklyWorkHours=0;
    this.HoursMon=0;
    this.HoursTuesday=0;
    this.HoursWednesday=0;
    this.HoursThursday=0;
    this.HoursFriday=0;
  } 

  if(this.MontagGespeichert  == true && this.DienstagGespeichert  == true && this.MittwochGespeichert  == true && 
    this.DonnerstagGespeichert == true && this.FreitagGespeichert  == true &&this._DateFrom != null 
    && this._DateTil != null && this.WeeklyWorkHours <= 40 && this.MondayReport !="" && this.TuesdayReport !="" && 
    this.WednesdayReport!="" && this.ThursdayReport!="" && this.FridayReport!="")
    {
        this.Succeed();
    }  
    else
    {
      alert("Bitte prüfen Sie Ihr Bericht:\nDatum 'Woche von:' Datum 'Bis:' muss gesetzt sein\nDie Tagesberichte dürfen nicht leer sein.\nDie Tagesberichte müssen täglich gespeichert werden\nDie Wochenarbeitszeit darf 40 Stunden nicht überschreiten")
      
    this.WeeklyWorkHours=0;
    this.HoursMon=0;
    this.HoursTuesday=0;
    this.HoursWednesday=0;
    this.HoursThursday=0;
    this.HoursFriday=0;
    } 
  
}
}
