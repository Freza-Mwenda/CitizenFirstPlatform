using System.Text.Json.Serialization;
using CitizenFirstUssd.Extensions;
using CitizenFirstUssd.Services;
using Serilog;
using Serilog.Events;
using UssdStateMachine;

try
{
    Log.Information("Citizen First API starting.....");
    
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog(conf =>
    {
        conf.MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Error);
        conf.WriteTo.Console();
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddUssdMenus();
    builder.Services.AddDbContext(builder.Configuration);
    builder.Services.AddScoped<KycService>();
    builder.Services.AddScoped<UserService>();

    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.Information("API Stopped");
}