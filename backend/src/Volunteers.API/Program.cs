using Volunteers.API.Extentions.DI;
using Volunteers.Infrastructure.Extentions.DI;
using Volunteers.Application.Extentions.DI;
using FluentValidation.AspNetCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Volunteers.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services
            .AddInfrastructure() 
            .AddApplication()
            .AddApi();

        //builder.Services
        //    .AddFluentValidationAutoValidation(configuration =>
        //    {
        //        //configuration.OverrideDefaultResultFactoryWith<>();
        //    });

        var app = builder.Build();
        app.AddWebApp();
    }
}