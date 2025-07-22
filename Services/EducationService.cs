using Microsoft.EntityFrameworkCore;
using Resume_Manager.Data;
using Resume_Manager.DTOs;
using Resume_Manager.Models;
using System.ComponentModel.DataAnnotations;

namespace Resume_Manager.Services
{
    public class EducationService
    {
        private readonly ManagerDbContext _context;
        public EducationService(ManagerDbContext context)
        {
            _context = context;
        }
        public async Task<IResult> AddEducation(CreateEducationDTO newEducation)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(newEducation);
            bool isValid = Validator.TryValidateObject(newEducation, validationContext, validationResults, true);

            if (!isValid)
            {
                return Results.BadRequest(validationResults);
            }

            var education = new Education
            {
                SchoolName = newEducation.SchoolName,
                Degree = newEducation.Degree,
                StartDate = newEducation.StartDate,
                EndDate = newEducation.EndDate,
                UserId_FK = newEducation.UserID_FK 
            };

            await _context.Educations.AddAsync(education);
            await _context.SaveChangesAsync();

            return Results.Ok(newEducation);
        }

        public async Task<IResult?> UpdateEducation(UpdateEducationDTO updatedEducation, int updateEducationId)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(updatedEducation);
            bool isValid = Validator.TryValidateObject(updatedEducation, validationContext, validationResults, true);

            if (!isValid)
            {
                return Results.BadRequest(validationResults);
            }

            var education = await _context.Educations.FirstOrDefaultAsync(e => e.EducationId == updateEducationId);

            if (education == null)
            {
                return Results.NotFound($"Education with id {updateEducationId} not found.");
            }

            if (!string.IsNullOrEmpty(updatedEducation.SchoolName)) { education.SchoolName = updatedEducation.SchoolName; }
            if (!string.IsNullOrEmpty(updatedEducation.Degree)) { education.Degree = updatedEducation.Degree; }
            if (updatedEducation.StartDate.HasValue) { education.StartDate = updatedEducation.StartDate.Value; }
            if (updatedEducation.EndDate.HasValue) { education.EndDate = updatedEducation.EndDate; }

            await _context.SaveChangesAsync();

            return Results.Ok(updatedEducation);
        }
        public async Task<IResult?> DeleteEducation(int educationId)
        {
            if (educationId <= 0)
            {
                return Results.BadRequest("Invalid education ID");
            }

            var education = await _context.Educations.FirstOrDefaultAsync(e => e.EducationId == educationId);

            if (education == null)
            {
                return Results.NotFound($"Education with id {educationId} not found.");
            }

            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();

            return Results.Ok( new EducationDTO
            {
                EducationId = education.EducationId,
                SchoolName = education.SchoolName,
                Degree = education.Degree,
                StartDate = education.StartDate,
                EndDate = education.EndDate
            });
        }
    }
}
