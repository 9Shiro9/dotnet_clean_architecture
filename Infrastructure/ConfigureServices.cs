//using Domain.Interfaces;
//using Infrastructure.Common;
//using Infrastructure.Identity;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        //public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        //{

        //    var connectionString = configuration.GetConnectionString("DefaultConnection");

        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseSqlServer(connectionString));


        //    services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
        //    .AddEntityFrameworkStores<ApplicationDbContext>()
        //    .AddDefaultUI()
        //    .AddDefaultTokenProviders();
        //    return services;
        //}

        //public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        //{
        //    services.AddScoped(typeof(IRepository<>),typeof(EfRepository<>));
        //    return services;
        //}
    }
}
