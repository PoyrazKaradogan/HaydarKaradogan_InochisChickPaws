using Microsoft.EntityFrameworkCore;
using Inochis.Data.Concrete.Contexts;

namespace Inochis.UI.Extensions
{
    public static class HostServiceExtension
    {
        public static IHost UpdateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var InochisDbContext = scope.ServiceProvider.GetRequiredService<InochisDbContext>())
                {
                    try
                    {
                        var pendingMigrationCount = InochisDbContext.Database.GetPendingMigrations().Count();
                        if (pendingMigrationCount>0) 
                            InochisDbContext.Database.Migrate();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                }
            }
            return host;
        }
    }
}
