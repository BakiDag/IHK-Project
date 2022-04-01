export class Wochenbericht
{
    public  ID :number
    public  AusbilderID :number
    public  AuszubildendenID :number
    public  Kalenderwoche :number
    public  DatumVon :Date
    public  DatumBis :Date 
    public  Seite :number
    public  Montagsbericht :string
    public  Dienstagsbericht :string
    public  Mittwochsbericht :string
    public  Donnerstagsbericht :string
    public  Freitagsbericht :string

    public StatusAzubi :StatusAzubi  = StatusAzubi.InBearbeitung;
    public StatusAusbilder? :StatusAusbilder  = null;
    
    public UnterschriftAzubi: string;
    public UnterschriftAusbilder : string;
    // public Ausbilder Ausbilder 
    // public Auszubildendenden Auszubilndenden { get; set;  }

    constructor() {
    }

    
}
export enum StatusAzubi
{
InBearbeitung,
IstUnterschrieben
 }
export enum StatusAusbilder
{
InÜberprüfung,
IstUnterschrieben
}