using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Infrastructure.Data;

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
            // Usamos a mesma string que está no seu docker-compose/appsettings
            // O host 'localhost' é usado aqui porque você está rodando o comando na sua máquina
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=gastodb;Username=devuser;Password=devpassword");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
