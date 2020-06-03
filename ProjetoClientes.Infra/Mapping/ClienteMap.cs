using ProjetoClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjetoClientes.Infra.Mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.Nome).HasColumnType("varchar(30)").HasColumnName("Nome").IsRequired();
            builder.Property(p => p.Cpf).HasColumnType("varchar(11)").HasColumnName("Cpf").IsRequired();
            builder.Property(p => p.DataNascimento).HasColumnType("datetime").HasColumnName("DataNascimento").IsRequired();
            builder.HasMany(p => p.enderecos);
            builder.ToTable("Cliente");
        }
    }
}