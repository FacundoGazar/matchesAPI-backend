using System.Buffers.Text;
using MatchesAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchesAPI.Data;

public class MatchStoreContext(DbContextOptions<MatchStoreContext> options) 
    : DbContext(options)
{
    public DbSet<Match> Matches => Set<Match>();

    public DbSet<Team> Teams => Set<Team>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>().HasData(
            new {Id = 1, Name = "Boca Juniors"},
            new {Id = 2, Name = "River Plate"},
            new {Id = 3, Name = "Estudiantes"},
            new {Id = 4, Name = "Gimnasia"},
            new {Id = 5, Name = "Racing"},
            new {Id = 6, Name = "Independiente"},
            new {Id = 7, Name = "San Lorenzo"}
        );
    } 
}