using System.ComponentModel.DataAnnotations;

namespace Resume_Manager.DTOs
{
    public class CreateEducationDTO
    {
        [Required(ErrorMessage = "School name is required")]
        public string SchoolName { get; set; }

        [Required(ErrorMessage = "Degree is required")]
        public string Degree { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Corresponding user is required")]

        public int UserID_FK { get; set; }
    }
}
