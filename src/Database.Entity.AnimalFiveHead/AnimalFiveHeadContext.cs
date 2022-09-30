using Database.Entity.AnimalFiveHead.Mappings;
using Database.Entity.AnimalFiveHead.Models;
using Microsoft.EntityFrameworkCore;

// Scaffold-DbContext Data Source=SASHENPC\\SQLEXPRESS;Initial Catalog=AnimalFiveHead;User ID=sa;Password=password1234$;Connect Timeout=30;" Microsoft.EntityFrameworkCore.SqlServer
// Add-Migration firstEntityUpdate
// Update-Database
namespace Database.Entity.AnimalFiveHead
{
  public partial class AnimalFiveHeadContext : DbContext
  {
    public AnimalFiveHeadContext()
    {
    }

    public AnimalFiveHeadContext(DbContextOptions<AnimalFiveHeadContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GameSessionState> GameSessionStates { get; set; } = null!;

    public virtual DbSet<PlayerSessionInformation> PlayerSessionInformations { get; set; } = null!;

    public virtual DbSet<PlayerType> PlayerTypes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new GameSessionStateMap());
      modelBuilder.ApplyConfiguration(new PlayerSessionInformationMap());
      modelBuilder.ApplyConfiguration(new PlayerTypeMap());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => _ = optionsBuilder.UseSqlServer("Data Source=SASHENPC\\SQLEXPRESS;Initial Catalog=AnimalFiveHead;User ID=sa;Password=password1234$;Connect Timeout=30;");
  }
}
