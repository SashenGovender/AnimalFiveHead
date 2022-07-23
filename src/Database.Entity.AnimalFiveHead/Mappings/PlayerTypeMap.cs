using Database.Entity.AnimalFiveHead.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Entity.AnimalFiveHead.Mappings
{
  public class PlayerTypeMap : IEntityTypeConfiguration<PlayerType>
  {
    public void Configure(EntityTypeBuilder<PlayerType> builder)
    {
      builder.ToTable("tb_PlayerType");

      builder.HasKey(e => e.PlayerId);

      builder.Property(e => e.PlayerId)
        .IsRequired()
        .HasColumnName("PlayerId")
        .HasColumnType("int")
        .ValueGeneratedOnAdd();

      builder.Property(e => e.PlayerName)
        .IsRequired()
        .HasColumnName("PlayerName")
        .HasColumnType("varchar(20)")
        .HasMaxLength(20);
    }
  }
}
