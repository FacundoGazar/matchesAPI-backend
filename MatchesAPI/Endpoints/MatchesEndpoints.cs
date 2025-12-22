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

        return group;
    }
}