using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int WeeklyReportPositionsID { get; set; }
        public int InstructorID { get; set; }
        public string? Comment { get; set; }

        public virtual Instructor Instructor { get; set; }
        public virtual WeeklyReportPosition WeeklyReportPosition { get; set; }
    }
}
