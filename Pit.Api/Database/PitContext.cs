using Microsoft.EntityFrameworkCore;
using Pit.Api.Database.Mapping;
using Pit.Api.Models;

namespace Pit.Database;

public class PitContext : DbContext
{
    public DbSet<Produto> produto { get; set; }

    public PitContext(DbContextOptions<PitContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ProdutoMapping());
    }
}