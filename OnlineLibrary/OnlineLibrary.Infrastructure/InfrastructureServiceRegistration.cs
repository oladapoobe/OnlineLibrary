using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineLibrary.Application.Contracts.Persistence;
using OnlineLibrary.Infrastructure.Persistence;
using OnlineLibrary.Infrastructure.Repositories;

namespace OnlineLibrary.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryContext>(options =>
        options.UseInMemoryDatabase("OnlineLibraryInMemoryDB"));

            //services.AddDbContext<LibraryContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("OnlineLibraryConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IJwtTokenHandler>(sp => new JwtTokenHandler(configuration));
     


            return services;
        }
    }
}
