using Resume_Manager.Services;

namespace Resume_Manager.Endpoints
{
    public class UserEndpoints
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapGet("/users", async (UserService userService) =>
            {
                return await userService.FetchAllUsers();
            });

            app.MapGet("/users/{userId:int}", async (UserService userService, int userId) =>
            {
                return await userService.FetchUserByID(userId);
            }
            );
        }
    }
}
