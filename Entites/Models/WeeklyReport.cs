using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class WeeklyReport
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int ApprenticeID { get; set; }
        public int InstructorID { get; set; }
        public int CalenderWeek { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int Page { get; set; }
        public StatusApprentice StatusApprentice { get; set; } = StatusApprentice.inEditing;
        public StatusInstructor StatusInstructor { get; set; }
        //public byte[] SigningApprentice { get; set; }
        //public byte[] SigningInstructor { get; set; }
        public string? SigningApprentice { get; set; }
        public string? SigningInstructor { get; set; }
        public DateTime SigningDateApprentice { get; set; }
        public DateTime SigningDateInstructor { get; set; }

        public virtual Apprentice Apprentice { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<WeeklyReportPosition> WeeklyReportPositions { get; set; } = new List<WeeklyReportPosition>();
    }
}
