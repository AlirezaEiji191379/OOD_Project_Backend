using Microsoft.Extensions.FileProviders;
using OOD_Project_Backend.Core.Common.DependencyInjection;
using OOD_Project_Backend.Core.Common.Middlewares;
using OOD_Project_Backend.Core.DataAccess;

WebApplicationBuilder builder = AddServices(args);

UseMiddlewares(builder);

static void UseMiddlewares(WebApplicationBuilder builder)
{
    var app = builder.Build();
    Migrator.Migrate(app);
    app.UseCors("AllowAnyUrl");
    app.UseStaticFiles(new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "./Resources")),
        RequestPath = new PathString("")
    });
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<SecurityMiddleware>();
    app.MapControllers();

    app.Run("http://*:5000");
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
        options.AddPolicy("AllowAnyUrl",
            builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
            });
    });
    
    return builder;
}