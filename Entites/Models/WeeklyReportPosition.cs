using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class WeeklyReportPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int WeeklyReportID { get; set; }
        public int ApprenticeID { get; set; }
        public int NoteID { get; set; }
        public string? DailyReport { get; set; }
        public int DailyHours { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public virtual WeeklyReport? WeeklyReport { get; set; }
        [NotMapped]
        public virtual Note? Note { get; set; }
        [NotMapped]
        public virtual Apprentice? apprentice { get; set; }

    }
}
