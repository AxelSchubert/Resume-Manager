using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<WorkExperienceDTO?> AddWorkExperience(WorkExperienceDTO newWorkExperience)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(newWorkExperience);
            bool isValid = Validator.TryValidateObject(newWorkExperience, validationContext, validationResults, true);

            if (!isValid)
            {
                return null;
            }

            var workExperience = new WorkExperience
            {
                JobTitle = newWorkExperience.JobTitle,
                CompanyName = newWorkExperience.CompanyName,
                Description = newWorkExperience.Description,
                StartDate = newWorkExperience.StartDate,
                EndDate = newWorkExperience.EndDate
            };

            await _context.WorkExperiences.AddAsync(workExperience);
            await _context.SaveChangesAsync();

            newWorkExperience.WorkExperienceId = workExperience.WorkExperienceId;

            return newWorkExperience;
        }

        public async Task<UpdateWorkExperienceDTO?> UpdateWorkExperience(UpdateWorkExperienceDTO updatedWorkExperience, int updateWEId)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(updatedWorkExperience);
            bool isValid = Validator.TryValidateObject(updatedWorkExperience, validationContext, validationResults, true);

            if (!isValid)
            {
                return null;
            }

            var workExperience = await _context.WorkExperiences.FirstOrDefaultAsync(we => we.WorkExperienceId == updateWEId);

            if (workExperience == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(updatedWorkExperience.JobTitle)) { workExperience.JobTitle = updatedWorkExperience.JobTitle; }
            if (!string.IsNullOrEmpty(updatedWorkExperience.CompanyName)) { workExperience.CompanyName = updatedWorkExperience.CompanyName; }
            if (!string.IsNullOrEmpty(updatedWorkExperience.Description)) { workExperience.Description = updatedWorkExperience.Description; }
            if (updatedWorkExperience.StartDate.HasValue) { workExperience.StartDate = updatedWorkExperience.StartDate.Value;}
            if (updatedWorkExperience.EndDate.HasValue) { workExperience.EndDate = updatedWorkExperience.EndDate; }

            await _context.SaveChangesAsync();

            return updatedWorkExperience;
        }
        public Task<WorkExperienceDTO?> DeleteWorkExperience()
        {
        }
    }
}
