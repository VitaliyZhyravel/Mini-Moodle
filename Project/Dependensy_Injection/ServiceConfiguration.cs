using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mini_Moodle.Authentication;
using Mini_Moodle.Automapper;
using Mini_Moodle.Behaviors;
using Mini_Moodle.DatabaseContext;
using Mini_Moodle.FileStorageServices;
using Mini_Moodle.Models.Identity;
using Mini_Moodle.Repositories.Account;
using Mini_Moodle.Repositories.AssignmentServices.Implementation;
using Mini_Moodle.Repositories.AssignmentServices.Interfaces;
using Mini_Moodle.Repositories.CourseService.Implementation;
using Mini_Moodle.Repositories.CourseService.Interfaces;
using Mini_Moodle.Repositories.GradeService.Implementation;
using Mini_Moodle.Repositories.GradeService.Interfaces;
using Mini_Moodle.Repositories.LessonService.Implementation;
using Mini_Moodle.Repositories.LessonService.Interfaces;
using Mini_Moodle.Repositories.SubmissonService.Implementation;
using Mini_Moodle.Repositories.SubmissonService.Interfaces;
using Mini_Moodle.Repositories.UserService.Implementation;
using Mini_Moodle.Repositories.UserService.Interfaces;
using System.Text;

namespace Mini_Moodle.ApiServices
{
    static public class ServiceConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<Moodle_DbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(typeof(AutomapperProfiles));


            //Зчитує всі ендпоінти, атрибути, моделі і тд.
            services.AddEndpointsApiExplorer();

            //Додає Swagger документацію для open api
            services.AddSwaggerGen(options =>
            {
                //Цей код зчитує всі версії API з атрибутів контролерів
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    //Перебираємо всі версіїї та додаємо документацію для кожної з них
                    //Тим самим відмальовується зверху зліва Title та Version
                    options.SwaggerDoc(description.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Mini-MoodleApi",
                        Version = description.ApiVersion.ToString()
                    });
                }
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV"; // Формат групування версій (наприклад, v1, v2)
                options.SubstituteApiVersionInUrl = true; // Замінювати версію в URL
            });

            services.AddApiVersioning(options =>
            {
                //Визначає звідки саме зчитувати версію API (В нашому прикладі вказано UrlSegmentApiVersionReader що означає зчитування з url)
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
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

            services.AddHttpContextAccessor();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IFileStorageService, FileStorageService>();

            services.AddScoped<IAccountCommandRepository, AccountCommandRepository>();

            services.AddScoped<IAssignmentCommandRepository, AssignmentCommandRepository>();
            services.AddScoped<IAssignmentQueryRepository, AssignmentQueryRepository>();

            services.AddScoped<ILessonCommandRepository, LessonCommandRepository>();
            services.AddScoped<ILessonQueryRepository, LessonQueryRepository>();

            services.AddScoped<ICourseCommandRepository, CourseCommandRepository>();
            services.AddScoped<ICourseQueryRepository, CourseQueryRepository>();

            services.AddScoped<ISubmissionCommandRepository, SubmissionCommandRepository>();
            services.AddScoped<ISubmissionQueryRepository, SubmissionQueryRepository>();

            services.AddScoped<IUserQueryRepository, UserQueryRepository>();

            services.AddScoped<IGradeCommandRepository, GradeCommandRepository>();


            services.AddControllers();

            services.AddHttpLogging();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly);
            });

            //Зчитує всі класи валідатори з поточного проекту
            services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly);

            //Додає поведіку до MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddMemoryCache();
        }
    }
}
