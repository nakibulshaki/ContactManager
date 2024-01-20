using ContactManager.DAL.Contexts;
using ContactManager.BAL;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.App.Extentions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IContactAppService, ContactAppService>();
        return services;
    }
}
