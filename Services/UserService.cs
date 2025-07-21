using Resume_Manager.Models;
using Resume_Manager.Data;
using Resume_Manager.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Resume_Manager.Services
{
    public class UserService
    {
        public async Task<List<UserDTO>> FetchAllUsers(ManagerDbContext context)
        {
            var users = await context.Users.Select(u => new UserDTO
            {
                FullName = u.FullName,
                Description = u.Description,
                Email = u.Email,
                Educations = u.Educations.Select(e => new EducationDTO
                {
                    SchoolName = e.SchoolName,
                    Degree = e.Degree,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate
                }
                ).ToList(),
                WorkExperiences = u.WorkExperiences.Select(w => new WorkExperienceDTO
                {
                    CompanyName = w.CompanyName,
                    JobTitle = w.JobTitle,
                    Description = w.Description,
                    StartDate = w.StartDate,
                    EndDate = w.EndDate
                }).ToList()
            }).ToListAsync();

            return users;
        }

        public async Task<UserDTO> FetchUserByID(ManagerDbContext context, int userId)
        {
            var user = await context.Users.Where(u => u.UserId == userId).Select(u => new UserDTO
            {
                FullName = u.FullName,
                Description = u.Description,
                Email = u.Email,
                Educations = u.Educations.Select(e => new EducationDTO
                {
                    SchoolName = e.SchoolName,
                    Degree = e.Degree,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate
                }
                ).ToList(),
                WorkExperiences = u.WorkExperiences.Select(w => new WorkExperienceDTO
                {
                    CompanyName = w.CompanyName,
                    JobTitle = w.JobTitle,
                    Description = w.Description,
                    StartDate = w.StartDate,
                    EndDate = w.EndDate
                }).ToList()
            }).FirstOrDefaultAsync();
            
            return user;
        }
    }
}
