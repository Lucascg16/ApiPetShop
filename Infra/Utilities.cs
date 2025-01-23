using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Infra
{
    public static class Utilities
    {
        public static void MigrationInicialization(this IApplicationBuilder app)
        {
            try
            {
                using var serviceScope = app.ApplicationServices.CreateScope();
                var serviceDb = serviceScope.ServiceProvider.GetService<DbConnectionContext>();
                serviceDb?.Database.Migrate();
            }
            catch
            {
                return;
            }
        }
    }
}
