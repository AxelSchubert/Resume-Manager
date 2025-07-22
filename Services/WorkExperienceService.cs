using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Resume_Manager.Data;
using Resume_Manager.DTOs;
using Resume_Manager.Models;

namespace Resume_Manager.Services
{
    public class WorkExperienceService
    {
        private readonly ManagerDbContext _context;
        public WorkExperienceService(ManagerDbContext context)
        {
            _context = context;
        }
        public async Task<IResult?> AddWorkExperience(CreateWorkExperienceDTO newWorkExperience)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(newWorkExperience);
            bool isValid = Validator.TryValidateObject(newWorkExperience, validationContext, validationResults, true);

            if (!isValid)
            {
                return Results.BadRequest(validationResults);
            }

            var workExperience = new WorkExperience
            {
                JobTitle = newWorkExperience.JobTitle,
                CompanyName = newWorkExperience.CompanyName,
                Description = newWorkExperience.Description,
                StartDate = newWorkExperience.StartDate,
                EndDate = newWorkExperience.EndDate,
                UserId_FK = newWorkExperience.UserID_FK
            };

            await _context.WorkExperiences.AddAsync(workExperience);
            await _context.SaveChangesAsync();

           return Results.Ok(newWorkExperience);
        }

        public async Task<IResult?> UpdateWorkExperience(UpdateWorkExperienceDTO updatedWorkExperience, int updateWEId)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(updatedWorkExperience);
            bool isValid = Validator.TryValidateObject(updatedWorkExperience, validationContext, validationResults, true);

            if (!isValid)
            {
                return Results.BadRequest(validationResults);
            }

            var workExperience = await _context.WorkExperiences.FirstOrDefaultAsync(we => we.WorkExperienceId == updateWEId);

            if (workExperience == null)
            {
                return Results.NotFound($"WorkExperience with id {updateWEId} not found.");
            }

            if (!string.IsNullOrEmpty(updatedWorkExperience.JobTitle)) { workExperience.JobTitle = updatedWorkExperience.JobTitle; }
            if (!string.IsNullOrEmpty(updatedWorkExperience.CompanyName)) { workExperience.CompanyName = updatedWorkExperience.CompanyName; }
            if (!string.IsNullOrEmpty(updatedWorkExperience.Description)) { workExperience.Description = updatedWorkExperience.Description; }
            if (updatedWorkExperience.StartDate.HasValue) { workExperience.StartDate = updatedWorkExperience.StartDate.Value;}
            if (updatedWorkExperience.EndDate.HasValue) { workExperience.EndDate = updatedWorkExperience.EndDate; }

            await _context.SaveChangesAsync();

            return Results.Ok(updatedWorkExperience);
        }
        public async Task<IResult?> DeleteWorkExperience(int workExperienceId)
        {
            if (workExperienceId <= 0)
            {
                return Results.BadRequest("Invalid WorkExperienceID");
            }

            var workExperience = await _context.WorkExperiences.FirstOrDefaultAsync(we => we.WorkExperienceId == workExperienceId);

            if (workExperience == null)
            {
                return Results.NotFound($"Work experience with id {workExperienceId} not found.");
            }

            _context.WorkExperiences.Remove(workExperience);
            await _context.SaveChangesAsync();

            return Results.Ok(new WorkExperienceDTO
            {
                WorkExperienceId = workExperience.WorkExperienceId,
                JobTitle = workExperience.JobTitle,
                CompanyName = workExperience.CompanyName,
                Description = workExperience.Description,
                StartDate = workExperience.StartDate,
                EndDate = workExperience.EndDate
            });
        }
    }
}
