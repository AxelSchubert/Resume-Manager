using Resume_Manager.DTOs;
using Resume_Manager.Services;

namespace Resume_Manager.Endpoints
{
    public class WorkExperienceEndpoints
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapPost("/workexperiences", async (WorkExperienceService workExperienceService, CreateWorkExperienceDTO workExperience) =>
            {

                if (workExperience == null)
                {
                    return Results.BadRequest("WorkExperienceDTO is required.");
                }

                return await workExperienceService.AddWorkExperience(workExperience);
            });

            app.MapPut("/workexperiences/{workExperienceId:int}", async (WorkExperienceService workExperienceService, UpdateWorkExperienceDTO updateWorkExperience, int workExperienceId) =>
            {
                if (updateWorkExperience == null)
                {
                    return Results.BadRequest("Invalid UpdateWorkExperienceDTO.");
                }

                return await workExperienceService.UpdateWorkExperience(updateWorkExperience, workExperienceId);
            });

            app.MapDelete("/workexperiences/{workExperienceId:int}", async (WorkExperienceService workExperienceService, int workExperienceId) =>
            {
                return await workExperienceService.DeleteWorkExperience(workExperienceId);
            });
        }
    }
}
