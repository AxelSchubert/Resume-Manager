using System.ComponentModel.DataAnnotations;


namespace Resume_Manager.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(40)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(80)]
        public string Description { get; set; }
        [Required]
        [MaxLength(30)]
        [EmailAddress]
        public string Email { get; set; }
        public List<Education> Educations { get; set; }

        public List<WorkExperience> WorkExperiences { get; set; }

    }
}
