using ApiPetShop.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ApiPetShop.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("A string de conexão não foi encontrada, favor verificar o appsettings.");
            services.AddDbContext<DbConnectionContext>(opt => opt.UseSqlServer(DecryptConnectionPassword(connectionString)));

            //Services
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IVetServices, VetServices>();
            services.AddScoped<IPetServices, PetServices>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IScheduleService, ScheduleService>();

            //Repositories
            services.AddScoped<IUserRepository, UserRespository>();
            services.AddScoped<IVetRepository, VetRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IVacineRepository, VacineRepository>();


            return services;
        }

        private static string DecryptConnectionPassword(string connectionstring)
        {
            ICryptoService _crypt = new CryptoService();
            var builder = new DbConnectionStringBuilder
            {
                ConnectionString = connectionstring
            };

            var decryptPass = _crypt.Decrypt(builder["Password"].ToString() ?? "");
            builder["Password"] = decryptPass;

            return builder.ConnectionString;
        }
    }
}
