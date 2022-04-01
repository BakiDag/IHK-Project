export class Role{
    public role: string="";
    public isSelected:boolean=false;

    constructor(role:string, isSelected:boolean=false)  
    {
        this.role = role;

        //pruefen ob noch benoetigt wird 
        //selection erfolgt durch dropdown und nicht mehr mit input checkbox
        this.isSelected = isSelected;
    }
}