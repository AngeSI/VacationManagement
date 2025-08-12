using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System.Reflection;

using VacationDomain.Interfaces;
using VacationDomain.Services;

using VacationPersistancy.Vacation;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(ManageSwaggerOptions);
        builder.Services.AddScoped<IVacationDbService, VacationDbService>();
        builder.Services.AddScoped<IVacationService, VacationService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void ManageSwaggerOptions(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",//On peut imaginer un système de versioning pour l'API basé sur une variable dans la pipeline de CI/CD et en rapport avec les tags sur la branche principale du projet Git
            Title = "Gestion des congés",
            Description = "API pour la gestion des congés"
        });
        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        options.UseInlineDefinitionsForEnums();
    }

}
