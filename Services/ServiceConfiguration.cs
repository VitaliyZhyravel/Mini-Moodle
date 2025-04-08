using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mini_Moodle.Automapper;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Models.Identity;
using System.Reflection;

namespace Mini_Moodle.ApiServices
{
    static public class ServiceConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services,WebApplicationBuilder builder)
        {
            services.AddControllers();

            services.AddDbContext<Moodle_DbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(typeof(AutomapperProfiles));

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 3;
            })
               .AddEntityFrameworkStores<Moodle_DbContext>()
              .AddDefaultTokenProviders();
        }
    }
}
