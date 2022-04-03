using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Apprentice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int InstructorID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateEntry { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
        [NotMapped]
        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<WeeklyReport> WeeklyReports { get; set; } = new List<WeeklyReport>();
        public virtual ICollection<WeeklyReportPosition> WeeklyReportPositions { get; set; } = new List<WeeklyReportPosition>();
    }
}
