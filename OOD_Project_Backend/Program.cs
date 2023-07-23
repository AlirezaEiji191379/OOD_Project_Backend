using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Common.DependencyInjection;
using OOD_Project_Backend.Core.Common.Middlewares;

WebApplicationBuilder builder = AddServices(args);

UseMiddlewares(builder);

static void UseMiddlewares(WebApplicationBuilder builder)
{
    var app = builder.Build();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<SecurityMiddleware>();
    app.MapControllers();

    app.Run();
}
static WebApplicationBuilder AddServices(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddOODServices(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddScoped<SecurityMiddleware>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificPort",
            builder =>
            {
                builder.WithOrigins("http://*:8080")
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
            });
    });
    
    return builder;
}