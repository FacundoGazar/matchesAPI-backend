using System.Buffers.Text;
using MatchesAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchesAPI.Data;

public class MatchStoreContext(DbContextOptions<MatchStoreContext> options) 
    : DbContext(options)
{
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Team> Teams => Set<Team>();

    public DbSet<Standing> Standings => Set<Standing>();

    public DbSet<Player> Players => Set<Player>();

    public DbSet<PlayerDetail> PlayerDetails => Set<PlayerDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Standing>(entity =>
        {
            entity.HasKey(s => s.TeamId);

            entity.HasOne(s => s.Team)
                .WithOne(t => t.Standing)
                .HasForeignKey<Standing>(s => s.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }


}