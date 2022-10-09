using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pit.Api.Models;

namespace Pit.Api.Database.Mapping
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Nome).HasColumnName("nome");
            builder.Property(x => x.Descricao).HasColumnName("descricao");
            builder.Property(x => x.CodigoBarras).HasColumnName("codigobarras");
            builder.Property(x => x.Valor).HasColumnName("valor");
            builder.Property(x => x.DataHoraCriado).HasColumnName("datahoracriado");
            builder.Property(x => x.Ativo).HasColumnName("ativo");
        }
    }
}
