using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestReportViewer.Data.PostgreSQL;

public static class Extensions
{
    public static IServiceCollection AddPostgreSQL(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<TestExecutionsContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("TestExecutionContext")))
            .AddTransient<IStorage, DbStorage>();
    }
}
