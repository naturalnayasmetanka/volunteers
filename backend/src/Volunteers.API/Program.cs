using Serilog;
using Volunteers.API.Extentions.DI;

namespace Volunteers.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddSharedDI(builder)
            .AddSerilog()
            .AddAccountsDI()
            .AddVolunteerDI()
            .AddSpeciesDI()
            .AddApi(builder);

        var app = builder.Build();
        app.AddWebApp();
    }
}