using System.ComponentModel.DataAnnotations;

namespace Resume_Manager.DTOs
{
    public class UpdateEducationDTO
    {
        public string? SchoolName { get; set; }

        public string? Degree { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public int? UserID_FK { get; set; }
    }
}
