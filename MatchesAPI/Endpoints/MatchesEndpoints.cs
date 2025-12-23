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

        group.MapGet("/", async (MatchStoreContext dbContext) => 
            await dbContext.Matches
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Select(m => m.ToDto())
                    .AsNoTracking()
                    .ToListAsync());

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

        group.MapPost("/", async (CreateMatchDto newMatch, MatchStoreContext dbContext) =>
        {
            Match match = newMatch.ToEntity();
            match.HomeTeam = dbContext.Teams.Find(newMatch.HomeTeamId);
            match.AwayTeam = dbContext.Teams.Find(newMatch.AwayTeamId);

            if (match.HomeTeam is null || match.AwayTeam is null
                || match.HomeTeamId == match.AwayTeamId)
            {
                return Results.BadRequest("Invalid team IDs provided.");
            }

            dbContext.Matches.Add(match);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                GetMatchEndpointName,
                new { id = match.Id }, 
                match.ToDto());
        })
        .WithParameterValidation();

        group.MapPut("/{id}", async (int id, UpdateMatchDto updatedMatch, MatchStoreContext dbContext) =>
        {
            Match? match = await dbContext.Matches.FindAsync(id);

            if (match is null)
            {
                return Results.NotFound();
            }
        
            dbContext.Entry(match)
                        .CurrentValues
                        .SetValues(updatedMatch);

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithParameterValidation();

        group.MapDelete("/{id}", async (int id, MatchStoreContext dbContext) =>
        {
            await dbContext.Matches.Where(m => m.Id == id)
                            .ExecuteDeleteAsync();
            
            return Results.NoContent();
        });

        group.MapGet("/{startDate}:{endDate}", async (DateTime startDate, DateTime endDate, MatchStoreContext dbContext) =>
        {
            var matches = await dbContext.Matches
                                .Include(m => m.HomeTeam)
                                .Include(m => m.AwayTeam)
                                .Where(m => m.MatchDate >= startDate && m.MatchDate <= endDate)
                                .Select(m => m.ToDto())
                                .AsNoTracking()
                                .ToListAsync();

            return Results.Ok(matches);
        });

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

        group.MapGet("/standing", async (MatchStoreContext dbContext) =>
        {
            var standings = await dbContext.Standings
                                .Include(s => s.Team)
                                .OrderBy(s => s.Rank)
                                .Select(s => s.ToDto())
                                .AsNoTracking()
                                .ToListAsync();

            return Results.Ok(standings);
        });

        group.MapGet("/standing/{teamId}", async(int teamId, MatchStoreContext dbContext) =>
        {
            var standing = await dbContext.Standings
                                .Include(s => s.Team)
                                .Where(s => s.TeamId == teamId)
                                .Select(s => s.ToDto())
                                .AsNoTracking()
                                .FirstOrDefaultAsync();

            return standing is null
                ? Results.NotFound()
                : Results.Ok(standing);
        });

        return group;
    }
    
}