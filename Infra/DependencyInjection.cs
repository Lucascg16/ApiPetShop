using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("A string de conexão não foi encontrada, favor verificar o appsettings.");
            services.AddDbContext<DbConection>(opt => opt.UseSqlServer(connectionString));

            //Services

            //Repositories

            return services;
        }

    }
}
