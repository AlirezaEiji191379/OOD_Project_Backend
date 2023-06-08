using OOD_Project_Backend.Core.Common.DependencyInjection;

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

    //app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
static WebApplicationBuilder AddServices(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddOODServices(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    return builder;
}