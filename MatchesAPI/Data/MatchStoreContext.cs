using System.Buffers.Text;
using MatchesAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchesAPI.Data;

public class MatchStoreContext(DbContextOptions<MatchStoreContext> options) 
    : DbContext(options)
{
    public DbSet<Match> Matches => Set<Match>();

    public DbSet<Team> Teams => Set<Team>();
}