using Backend.Data;
using Backend.Interface.Service;
using Backend.Mapper;
using Backend.Sevices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Backend.Extensions
{
    public static class ServicesConfig
    {
        public static IServiceCollection ServicesConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddOpenApi("v2");

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });


            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAll",
                poli => poli
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBookServices, BookServices>();
            services.AddScoped<IAuthorServices, AuthorServices>();
            services.AddScoped<IImageServices, ImageServices>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICartServices, CartServices>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["appsetting:token"]!)
                        ),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["appsetting:issuer"],
                    ValidateAudience = false,
                    ValidAudience = configuration["appsetting:audience"],

                    ValidateLifetime = true

                };
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:mydb"]);
            });
            services.AddDbContext<ImageDBContext>(options => {
                options.UseSqlServer(configuration["ConnectionStrings:myImageDB"]);
            });


            return services;
        }
    }
}
