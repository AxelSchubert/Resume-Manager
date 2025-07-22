using System.Text.Json;
using Resume_Manager.DTOs;

namespace Resume_Manager.Endpoints
{
    public class GithubEndpoints
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapGet("/github/{username}", async (HttpClient client, string username) =>
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    return Results.BadRequest("Username cannot be empty.");
                }

                client.DefaultRequestHeaders.UserAgent.ParseAdd("ResumeManager");

                var response = await client.GetAsync($"https://api.github.com/users/{username}/repos");

                if (!response.IsSuccessStatusCode)
                {
                    return Results.BadRequest();
                }

                if (response.Content == null)
                {
                    return Results.NotFound("No repos found for this user.");
                }

                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var data = JsonSerializer.Deserialize<List<RepoDTO>>(json, options);

                var filtered = data.Select(repo => new RepoDTO
                {
                    Name = string.IsNullOrWhiteSpace(repo.Name) ? "No name found for this repo" : repo.Name,
                    Description = string.IsNullOrWhiteSpace(repo.Description) ? "This repo has no description" : repo.Description,
                    Language = string.IsNullOrWhiteSpace(repo.Language) ? "No language specified" : repo.Language
                });

                return Results.Ok(filtered);
            });
        }
    }
}
