using System.ComponentModel.DataAnnotations;

namespace Resume_Manager.DTOs
{
    public class EducationDTO
    {
        public int? EducationId { get; set; }
        [Required(ErrorMessage = "School name is required")]
        public string SchoolName { get; set; }

        [Required(ErrorMessage = "Degree is required")]
        public string Degree { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
