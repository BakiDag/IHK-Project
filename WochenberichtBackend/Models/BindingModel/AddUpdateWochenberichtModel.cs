//using dotnetClaimAuthorization.Data.Entities;
//using System;

//namespace WochenberichtManagement.Models.BindingModel
//{
//    public class AddUpdateWochenberichtModel
//    {
//        public int ID { get; set; }


//        public int Kalenderwoche { get; set; }
//        public DateTime DatumVon { get; set; }
//        public DateTime DatumBis { get; set; }
//        public int Seite { get; set; }
//        public string Montagsbericht { get; set; }
//        public string Dienstagsbericht { get; set; }
//        public string Mittwochsbericht { get; set; }
//        public string Donnerstagsbericht { get; set; }
//        public string Freitagsbericht { get; set; }


//        public int AuszubildendenID { get; set; }//problem with identity User

//        public Auszubildendenden Auszubilndenden { get; set; }



//        //var id = UserManager.GetUserId(Ausbilder); // get user Id
//        //public int AusbilderID { get; set; } = UserManager.GetUserId();

//        public int AusbilderID { get; set; } //problem with identity User

//        //[ForeignKey("Ausbilder")]
//        public Ausbilder Ausbilder { get; set; }


//        public StatusAusbilder? StatusAusbilder { get; set; } = null;
//        public StatusAzubi StatusAzubi { get; set; } = StatusAzubi.inEditing;

//        public string UnterschriftAzubi { get; set; }
//        public string UnterschriftAusbilder { get; set; }


//    }
//    public enum StatusAzubi
//    {
//        inEditing,
//        isIsgned
//    }
//    public enum StatusAusbilder
//    {
//        inEditing,
//        isIsgned
//    }
//}

