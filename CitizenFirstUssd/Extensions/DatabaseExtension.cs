using CitizenFirstUssd.Database;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CitizenFirstUssd.Extensions;

public static class DatabaseExtensions
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var patumbaConnectionString = configuration.GetConnectionString("Postgres");
        
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(patumbaConnectionString).EnableUnmappedTypes();
        dataSourceBuilder.EnableDynamicJson();
        var dataSource = dataSourceBuilder.Build();
        
        services.AddDbContext<CitizensFirstDatabaseContext>(options =>
        {
            options.UseNpgsql(dataSource, opt => opt
                    .EnableRetryOnFailure()
                    .MigrationsHistoryTable("__EFMigrationsHistory", "public"))
                .UseSnakeCaseNamingConvention();;
        });
    }
}