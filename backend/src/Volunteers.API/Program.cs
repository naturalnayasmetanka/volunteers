using Volunteers.API.Extentions.DI;
using Volunteers.Application.Extentions.DI;
using Volunteers.Infrastructure.Extentions.DI;

namespace Volunteers.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services
            .AddInfrastructure()
            .AddApplication()
            .AddApi(builder);

        var app = builder.Build();
        app.AddWebApp();
    }
}