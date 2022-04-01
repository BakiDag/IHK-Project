export class Mitarbeiter
{
    public ID: number;
    public firstName:string="";
    public lastName:string="";
    public email:string="";
    public benutzerName:string="";
    public role:string="";

    constructor(_id: number,_vorname:string, _nachname:string, _email:string, _benutzerName:string , _role:string )
    {
        this.ID = _id;
        this.firstName=_vorname;
        this.lastName=_nachname;
        this.email=_email;
        this.benutzerName= _benutzerName;
        this.role= _role;
    }
}