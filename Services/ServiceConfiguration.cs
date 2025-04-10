using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mini_Moodle.Authentication;
using Mini_Moodle.Automapper;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.Models.Identity;
using System.Reflection;
using System.Text;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JwtToken:issuer"],
                        ValidAudience = builder.Configuration["JwtToken:audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:key"]))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["Some-Token"];

                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddHttpContextAccessor();
        }
    }
}
