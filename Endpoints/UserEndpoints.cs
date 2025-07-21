using Resume_Manager.Services;

namespace Resume_Manager.Endpoints
{
    public class UserEndpoints
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapGet("/users", async (UserService userService) =>
            {
                var users = await userService.FetchAllUsers();

                if (users == null)
                {
                    return Results.NotFound("No users found.");
                }

                return Results.Ok(users);
            });

            app.MapGet("/users/{userId:int}", async (UserService userService, int userId) =>
            {
                if (userId <= 0)
                {
                    return Results.BadRequest("Invalid user ID.");
                }

                var user = await userService.FetchUserByID(userId);

                if (user == null)
                {
                    return Results.NotFound("Specified user not found.");
                }

                return Results.Ok(user);
            }
            );
        }
    }
}
