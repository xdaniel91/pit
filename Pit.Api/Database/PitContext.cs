using Microsoft.EntityFrameworkCore;
using Pit.Api.Database.Mapping;
using Pit.Api.Models;

namespace Pit.Database;

public class PitContext : DbContext
{
    public DbSet<Produto> produto { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Host=host.docker.internal;Port=49153;Database=postgres;Username=postgres;Password=postgrespw");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ProdutoMapping());
    }
}