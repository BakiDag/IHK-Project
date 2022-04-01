using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Instructor
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateEntry { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
        public virtual ICollection<WeeklyReport> WeeklyReports { get; set; } = new List<WeeklyReport>();
        public virtual ICollection<Apprentice> Apprentices { get; set; } = new List<Apprentice>();
    }
}
