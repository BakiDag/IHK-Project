using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccessEfCore.DataContext
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WochenberichtDBContext>
    {
        public WochenberichtDBContext CreateDbContext(string[] args)
        {
            string config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false).Build()
                .GetConnectionString("DefaultConnection");

            var optionsBuilder= new DbContextOptionsBuilder<WochenberichtDBContext>();

            optionsBuilder.UseSqlServer(config, b => b.MigrationsAssembly(typeof(WochenberichtDBContext).Assembly.FullName));

            return new WochenberichtDBContext(optionsBuilder.Options);

        }
    }
}
