using Resume_Manager.Services;
using Resume_Manager.DTOs;

namespace Resume_Manager.Endpoints
{
    public class EducationEndpoints
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapPost("/educations", async (EducationService educationService, EducationDTO education ) => { 
                
                if (education == null)
                {
                    return Results.BadRequest("EducationDTO is required.");
                }

                return await educationService.AddEducation(education);
            });

            app.MapPut("/educations/{educationId:int}", async (EducationService educationService, UpdateEducationDTO updateEducation, int educationId) =>
            {
                if (updateEducation == null)
                {
                    return Results.BadRequest("Invalid UpdateEducationDTO.");
                }

                return await educationService.UpdateEducation(updateEducation, educationId);
            });

            app.MapDelete("/educations/{educationId:int}", async (EducationService educationService, int educationId) =>
            {
                return await educationService.DeleteEducation(educationId);
            });
        }
    }
}
