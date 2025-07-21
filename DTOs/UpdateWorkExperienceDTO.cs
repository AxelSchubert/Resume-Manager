using System.ComponentModel.DataAnnotations;

namespace Resume_Manager.DTOs
{
    public class UpdateWorkExperienceDTO
    {
        public string? JobTitle { get; set; }
        public string? CompanyName { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
