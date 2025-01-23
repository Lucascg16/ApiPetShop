using ApiPetShop.Domain;
using ApiPetShop.Infra.Services;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("A string de conexão não foi encontrada, favor verificar o appsettings.");
            services.AddDbContext<DbConnectionContext>(opt => opt.UseSqlServer(connectionString));

            //Services
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IVetServices, VetServices>();
            services.AddScoped<IPetServices, PetServices>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICryptoService, CryptoService>();

            //Repositories
            services.AddScoped<IUserRepository, UserRespository>();
            services.AddScoped<IVetRepository, VetRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IVacineRepository, VacineRepository>();


            return services;
        }

    }
}
