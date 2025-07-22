using Resume_Manager.Models;
using Resume_Manager.Data;
namespace Resume_Manager.Data
{
    public class SeedData
    {

        //ChatGPT-genererad metod för seed data
        public static void InitialiseDB(ManagerDbContext context)
        {
            if (context.Users.Any() || context.Educations.Any() || context.WorkExperiences.Any())
                return;

            var users = new List<User>
        {
            new()
            {
                FullName = "Anna Karlsson",
                Description = "Ambitiös mjukvaruutvecklare med fokus på backend.",
                Email = "anna.karlsson@example.se"
            },
            new()
            {
                FullName = "Erik Johansson",
                Description = "Frontend-specialist med erfarenhet inom React och UX.",
                Email = "erik.johansson@example.se"
            },
            new()
            {
                FullName = "Sara Nilsson",
                Description = "Fullstackutvecklare med fokus på cloudlösningar.",
                Email = "sara.nilsson@example.se"
            }
        };

            context.Users.AddRange(users);
            context.SaveChanges(); // Save first to generate UserId (if identity)

            // Now add related education and work experience
            var educations = new List<Education>
        {
            new()
            {
                SchoolName = "Lunds Universitet",
                Degree = "Civilingenjör Datateknik",
                StartDate = new DateTime(2015, 8, 15),
                EndDate = new DateTime(2020, 6, 10),
                UserId_FK = users[0].UserId,
                User = users[0]
            },
            new()
            {
                SchoolName = "Chalmers Tekniska Högskola",
                Degree = "Interaktionsdesign",
                StartDate = new DateTime(2014, 9, 1),
                EndDate = new DateTime(2017, 6, 1),
                UserId_FK = users[1].UserId,
                User = users[1]
            },
            new()
            {
                SchoolName = "Stockholms Universitet",
                Degree = "Systemvetenskap",
                StartDate = new DateTime(2016, 8, 20),
                EndDate = new DateTime(2019, 6, 10),
                UserId_FK = users[2].UserId,
                User = users[2]
            }
        };

            context.Educations.AddRange(educations);

            var experiences = new List<WorkExperience>
        {
            new()
            {
                JobTitle = "Backend-utvecklare",
                CompanyName = "Spotify AB",
                Description = "Arbetade med mikroservice-arkitektur och C#/.NET.",
                StartDate = new DateTime(2020, 8, 1),
                EndDate = null,
                UserId_FK = users[0].UserId,
                User = users[0]
            },
            new()
            {
                JobTitle = "Frontend-utvecklare",
                CompanyName = "Klarna AB",
                Description = "Byggde moderna UI-komponenter i React.",
                StartDate = new DateTime(2017, 9, 1),
                EndDate = null,
                UserId_FK = users[1].UserId,
                User = users[1]
            },
            new()
            {
                JobTitle = "Systemutvecklare",
                CompanyName = "Ericsson AB",
                Description = "Utvecklade molnbaserade lösningar med .NET och Azure.",
                StartDate = new DateTime(2019, 9, 1),
                EndDate = null,
                UserId_FK = users[2].UserId,
                User = users[2]
            }
        };

            context.WorkExperiences.AddRange(experiences);

            context.SaveChanges();
        }
    }
}
