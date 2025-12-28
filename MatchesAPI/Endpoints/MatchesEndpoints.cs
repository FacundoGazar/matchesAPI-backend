using MatchesAPI.Dtos;
using MatchesAPI.Entities;
using MatchesAPI.Data;
using MatchesAPI.Mapping;
using Microsoft.EntityFrameworkCore;

namespace MatchesAPI.Endpoints;

public static class MatchesEndpoints
{
    const string GetMatchEndpointName = "GetMatch";

    public static RouteGroupBuilder MapMatchesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/matches")
                        .WithParameterValidation();

        group.MapGet("/", async (
            MatchStoreContext dbContext,
            DateTime? from,
            DateTime? to,
            int page = 1,
            int pageSize = 1) =>
        {
            var query = dbContext.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .AsNoTracking()
                .AsQueryable();

            if (from.HasValue)
            {
                var fromUtc = DateTime.SpecifyKind(from.Value, DateTimeKind.Utc);
                query = query.Where(m => m.MatchDate >= fromUtc);
            }

            if (to.HasValue)
            {
                var toUtc = DateTime.SpecifyKind(to.Value, DateTimeKind.Utc);
                query = query.Where(m => m.MatchDate <= toUtc);
            }

            var totalCount = await query.CountAsync();

            var matches = await query
                .OrderBy(m => m.Week)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => m.ToDto())
                .ToListAsync();

            return Results.Ok(new
            {
                data = matches,
                page,
                pageSize,
                totalCount,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            });
        });

        group.MapGet("/{id}", async (int id, MatchStoreContext dbContext) => 
        {
            Match? match = await dbContext.Matches
                            .Include(m => m.HomeTeam)
                            .Include(m => m.AwayTeam)
                            .FirstOrDefaultAsync(m => m.Id == id);

            return match is null 
                ? Results.NotFound() 
                : Results.Ok(match.ToDto());
        })
        .WithName(GetMatchEndpointName);
        
        group.MapGet("/mostwins", async (MatchStoreContext dbContext) =>
        {
            var winCounts = await dbContext.Matches
                .Where(m => m.HomeScore != m.AwayScore)
                .Select(m => new
                {
                    WinnerTeamId = m.HomeScore > m.AwayScore ? m.HomeTeamId : m.AwayTeamId
                })
                .GroupBy(m => m.WinnerTeamId)
                .Select(g => new
                {
                    TeamId = g.Key,
                    Wins = g.Count()
                })
                .OrderByDescending(g => g.Wins)
                .ToListAsync();

            return Results.Ok(winCounts);
        });

        return group;
    }
    
}