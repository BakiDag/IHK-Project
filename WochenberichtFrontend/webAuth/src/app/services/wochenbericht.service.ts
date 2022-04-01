import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Ausbilder } from '../Models/Ausbilder';
import { Auszubildender } from '../Models/Auszubildender';
import { responseModel } from '../Models/responseModel';
import { StatusAusbilder, StatusAzubi } from '../Models/wochenbericht';
import { JWTTokenService } from './jwttoken.service';

@Injectable({
  providedIn: 'root'
})
export class WochenberichtService {
  private readonly baseURL:string="https://localhost:44397/api/Wochenbericht/" //adresse aus postman
  constructor(private httpClient: HttpClient,private token: JWTTokenService) { }



  public SaveWochenbericht(_ID:Number, _AusbilderID:Number,_AuszubildendenID:Number , _Kalenderwoche:Number,
               _DatumVon: Date,_DatumBis :Date,_Seite :Number, _Montagsbericht:string, _Dienstagsbericht:string,
               _Mittwochsbericht: string, _Donnerstagsbericht: string, _Freitagsbericht: string, _StatusAzubi: StatusAzubi,
                _StatusAusbilder: StatusAusbilder, _UnterschriftAzubi: string,_UnterschriftAusbilder: string)//, _Ausbilder: Ausbilder, _Auszubildenden: Auszubildender)
     {
  
       const body={
        ID: _ID,
        AusbilderID: _AusbilderID,
        AuszubildendenID: _AuszubildendenID,
        Kalenderwoche: _Kalenderwoche,
        DatumVon: _DatumVon,
        DatumBis: _DatumBis,
        Seite : _Seite,
        Montagsbericht : _Montagsbericht,
        Dienstagsbericht : _Dienstagsbericht,
        Mittwochsbericht : _Mittwochsbericht,
        Donnerstagsbericht : _Donnerstagsbericht,
        Freitagsbericht : _Freitagsbericht,
        StatusAzubi : _StatusAzubi,
        StatusAusbilder : _StatusAusbilder,
        UnterschriftAzubi : _UnterschriftAzubi,
        UnterschriftAusbilder: _UnterschriftAusbilder//,
        //Ausbilder : _Ausbilder,
        //Auszubilndenden : _Auszubildenden
         
       }
      return this.httpClient.post<responseModel>(this.baseURL+"SaveWochenbericht",body);
     }
}

