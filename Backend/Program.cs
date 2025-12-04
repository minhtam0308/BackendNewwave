
//using Backend.Extensions;
using Backend.Exceptions;
using Backend.Extensions;
using Backend.Middlware;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
services.ServicesConfigs(builder.Configuration);


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

//app.UseAuthMiddleware();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();


app.MapControllers();

app.Run();
