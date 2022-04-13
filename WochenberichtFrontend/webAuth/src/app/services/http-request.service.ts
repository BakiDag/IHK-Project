import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable } from 'rxjs';
import { responseCode } from '../enums/responseCode';
import { Mitarbeiter } from '../Models/mitarbeiter';
import { responseModel } from '../Models/responseModel';
import { Role } from '../Models/role';


@Injectable({
  providedIn: 'root'
})
export class HttpRequestService {
  private readonly weeklyReport:string="https://localhost:44397/api/wochenbericht/" //adresse aus postman
  private readonly userManagment:string="https://localhost:44397/api/userManagment/" //adresse aus postman
  
    
    constructor(private httpClient: HttpClient) { }
    
    public login(email:string, password:string)
    {
      const body={
        Email:email,
        Password:password
      }
      
      //let options = new RequestOptions({ headers: headers, withCredentials: true });
      return this.httpClient.post<responseModel>(this.userManagment+"Login", body )
      // return this.httpClient.post<responseModel>(this.baseURL+"Login", body,{ withCredentials: true })
      .pipe(map(user =>{
        
        return user;
      }));
      
    }
    public logout(email:string, password:string)
    {
      const body={
        Email:email,
        Password:password
      }
      return this.httpClient.post<responseModel>(this.userManagment+"Logout", body,{ withCredentials: true });
      
    }
    public register(_vorname:string, _nachname:string,_email:string , _password:string, _role: string)
     {
  
       const body={
        Vorname: _vorname,
        Nachname: _nachname,
        Email: _email,
        Password: _password,
        Role: _role
         
       }
      return this.httpClient.post<responseModel>(this.userManagment+"userManagment",body, { withCredentials: true });
     }
    
  
    public getAllMitarbeiter()
    { 
      return this.httpClient.get<responseModel>(this.userManagment + "GetAllEmployee").pipe(map(res=> {
        let mitarbeiterList = new Array<Mitarbeiter>();
        if (res.responseCode==responseCode.OK) {
          if(res.dateSet) {
            res.dateSet.map((x:Mitarbeiter) => {
              mitarbeiterList.push(x);
            })
          }
        }
        return mitarbeiterList;          
      }));
    }

    public loeschMitarbeiter(mitarbeiter: Mitarbeiter) 
    { 
      return this.httpClient.post<responseModel>(this.userManagment+"DeleteUser",mitarbeiter,{ withCredentials: true } );
     }
     public deleteContact(_email)
     {
      const body={
          
          Email: _email,
          
          }
      
       
          return this.httpClient.delete<responseModel>(this.userManagment+"deleteContact"+body,{ withCredentials: true });
       //return this.httpClient.delete<responseModel>(url,body);
       
       //return this.httpClient.delete(url);
     }
    public getAuszubildende()
    {          
      //return this.httpClient.get<responseModel>(this.userManagment+"GetAllApprentices",{ withCredentials: true }).pipe(map(res=>{
        return this.httpClient.get<responseModel>(this.userManagment+"GetAllApprentices").pipe(map(res=>{
        let mitarbeiterList=new Array<Mitarbeiter>();        
        if(res.responseCode==responseCode.OK)
        {
             if(res.dateSet)
             {
                res.dateSet.map((x:Mitarbeiter)=>
                {
                  mitarbeiterList.push(x);
                  console.log(mitarbeiterList);
                  
                })
             }
        }
            return mitarbeiterList;          
      }));
      
    }
    public getAllRoles()
    {        
      return this.httpClient.get<responseModel>(this.userManagment+"GetRoles",{ withCredentials: true }).pipe(map(res=>{
        let roleList=new Array<Role>();
        if(res.responseCode==responseCode.OK)
        {
             if(res.dateSet)
             {
                res.dateSet.map((x:string)=>
                {
                  roleList.push(new Role(x));
                })
             }
        }
            return roleList;          
      }));
      
    }
  }
  