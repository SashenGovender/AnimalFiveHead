using Database.Entity.AnimalFiveHead.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Entity.AnimalFiveHead.Mappings
{
  public class PlayerSessionInformationMap : IEntityTypeConfiguration<PlayerSessionInformation>
  {
    public void Configure(EntityTypeBuilder<PlayerSessionInformation> builder)
    {
      builder.ToTable("tb_PlayerSessionInformation");

      builder.HasKey(e => new { e.SessionId, e.PlayerId })
          .HasName("PK_PlayerSessionInformation");

      builder.Property(e => e.SessionId)
        .IsRequired()
        .HasColumnName("SessionId")
        .HasColumnType("uniqueidentifier");

      builder.Property(e => e.PlayerId)
        .IsRequired()
        .HasColumnName("PlayerId")
        .HasColumnType("int");

      builder.Property(e => e.Score)
        .IsRequired()
        .HasColumnName("Score")
        .HasColumnType("int");

      builder.Property(e => e.Cards)
        .IsRequired()
        .HasColumnName("Cards")
        .HasColumnType("nvarchar(MAX)");

      builder.Property(e => e.CardIds)
        .IsRequired()
        .HasColumnName("CardIds")
        .HasColumnType("nvarchar(MAX)");

      builder.Property(e => e.GameSession)
        .IsRequired()
        .HasColumnName("GameSession")
        .HasColumnType("nchar(10)")
        .HasMaxLength(10);

      builder.Property(e => e.GameResult)
        .IsRequired()
        .HasColumnName("GameResult")
        .HasColumnType("nvarchar(50)")
        .HasMaxLength(50);

      builder.Property(e => e.DateTimeAdded)
      .IsRequired()
      .HasColumnName("DateTimeAdded")
      .HasColumnType("datetime2(7)")
      .HasMaxLength(10);
      // .HasDefaultValueSql("(getdate())");


      builder.Property(e => e.DateTimeUpdated)
      .HasColumnName("DateTimeUpdated")
      .HasColumnType("datetime2(7)")
      .HasMaxLength(10);
      // .HasDefaultValueSql("(getdate())");
    }
  }
}
