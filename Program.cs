
using Microsoft.EntityFrameworkCore;
using Resume_Manager.Data;
using Resume_Manager.Endpoints;
using Resume_Manager.Services;

namespace Resume_Manager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ManagerDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddHttpClient();

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<WorkExperienceService>();
            builder.Services.AddScoped<EducationService>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            UserEndpoints.RegisterEndpoints(app);
            EducationEndpoints.RegisterEndpoints(app);
            WorkExperienceEndpoints.RegisterEndpoints(app);
            GithubEndpoints.RegisterEndpoints(app);

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ManagerDbContext>();
                context.Database.EnsureCreated(); // Or Migrate() if using migrations
                SeedData.InitialiseDB(context);
            }

            app.Run();
        }
    }
}
