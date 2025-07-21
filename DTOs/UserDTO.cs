namespace Resume_Manager.DTOs
{
    public class UserDTO
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public List<EducationDTO> Educations { get; set; }
        public List<WorkExperienceDTO> WorkExperiences { get; set; }

    }
}
