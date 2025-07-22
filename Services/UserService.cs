using Resume_Manager.Models;
using Resume_Manager.Data;
using Resume_Manager.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Resume_Manager.Services
{
    public class UserService
    {
        private readonly ManagerDbContext _context;
        public UserService(ManagerDbContext context)
        {
            _context = context;
        }
        public async Task<IResult> FetchAllUsers()
        {
            var users = await _context.Users.Select(u => new UserDTO
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

            if (!users.Any()) { return Results.NotFound("No users found"); }

            return Results.Ok(users);
        }

        public async Task<IResult> FetchUserByID(int userId)
        {
            if (userId <= 0) { return Results.BadRequest("userId must be greater than zero"); }

            var user = await _context.Users.Where(u => u.UserId == userId).Select(u => new UserDTO
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

            if (user == null) { return Results.NotFound($"User with id {userId} not found."); }

            return Results.Ok(user);
        }
    }
}
