using Backend.Data;
using Backend.Sevices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookServices, BookServices>();
builder.Services.AddScoped<IAuthorServices, AuthorServices>();
builder.Services.AddScoped<IImageServices, ImageServices>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi("v2");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["appsetting:token"]!)
            ),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["appsetting:issuer"],
        ValidateAudience = false,
        ValidAudience = builder.Configuration["appsetting:audience"],

        ValidateLifetime = true

    };
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:mydb"]);
});
builder.Services.AddDbContext<ImageDBContext>(options => {
    options.UseSqlServer(builder.Configuration["ConnectionStrings:myImageDB"]);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
    poli => poli
        .WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()); //cookie need this
});
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();


app.Run();
