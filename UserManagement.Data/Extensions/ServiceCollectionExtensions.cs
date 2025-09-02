using UserManagement.Data.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
        => services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IAuditLogRepository, AuditLogRepository>();
}
