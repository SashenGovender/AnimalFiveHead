using Database.Entity.AnimalFiveHead.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Entity.AnimalFiveHead.Mappings
{
  public class GameSessionStateMap : IEntityTypeConfiguration<GameSessionState>
  {
    public void Configure(EntityTypeBuilder<GameSessionState> builder)
    {
      builder.ToTable("tb_GameSessionState");

      builder.HasKey(e => e.GameStateId);

      builder.Property(e => e.GameStateId)
                .IsRequired()
        .HasColumnName("GameStateId")
        .HasColumnType("int");

      builder.Property(e => e.GameStateName)
        .IsRequired()
        .HasColumnName("GameStateName")
        .HasColumnType("varchar(20)")
        .HasMaxLength(20);
    }
  }
}
