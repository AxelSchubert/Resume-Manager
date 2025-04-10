using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resume_Manager.Models
{
    public class WorkExperience
    {
        [Key]
        public int WorkExperienceId { get; set; }

        [Required]
        [MaxLength(40)]
        public string JobTitle { get; set; }

        [Required]
        [MaxLength(40)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(80)]
        public string Description { get; set; }

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
