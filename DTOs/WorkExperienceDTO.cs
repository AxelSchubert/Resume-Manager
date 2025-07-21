using System.ComponentModel.DataAnnotations;

namespace Resume_Manager.DTOs
{
    public class WorkExperienceDTO
    {
        public int? WorkExperienceId { get; set; }
        [Required(ErrorMessage = "Job title is required")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
