using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoClientes.Domain.Entities;

namespace ProjetoClientes.Infra.Mapping
{
    public class EnderecoMap: IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.Logradouro).HasColumnType("varchar(50)").HasColumnName("Logradouro").IsRequired();
            builder.Property(p => p.Bairro).HasColumnType("varchar(40)").HasColumnName("Bairro").IsRequired();
            builder.Property(p => p.Cidade).HasColumnType("varchar(40)").HasColumnName("Cidade").IsRequired();
            builder.Property(p => p.Estado).HasColumnType("varchar(2)").HasColumnName("Estado").IsRequired();
            builder.Property(p => p.ClienteId).HasColumnType("int").HasColumnName("ClienteId").IsRequired();
            builder.HasOne(p => p.Cliente);
            builder.ToTable("Endereco");
        }
    }
}