using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resume_Manager.Models
{
    public class Education
    {
        [Key]
        public int EducationId { get; set; }

        [Required]
        [MaxLength(40)]
        public string SchoolName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Degree { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId_FK { get; set; }

        [Required]
        public User User { get; set; }

    }
}
