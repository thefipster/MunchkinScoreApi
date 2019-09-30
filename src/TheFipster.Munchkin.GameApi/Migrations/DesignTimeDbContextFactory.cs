using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TheFipster.Munchkin.GameApi.Migrations
{
    [ExcludeFromCodeCoverage]
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<IdentityDbContext>();
            builder.UseSqlite(
                "Data Source=users.sqlite",
                sqliteOptions => sqliteOptions
                    .MigrationsAssembly("TheFipster.Munchkin.GameApi"));

            return new IdentityDbContext(builder.Options);
        }
    }
}
