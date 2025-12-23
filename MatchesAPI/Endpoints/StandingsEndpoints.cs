using MatchesAPI.Dtos;
using MatchesAPI.Entities;
using MatchesAPI.Data;
using MatchesAPI.Mapping;
using Microsoft.EntityFrameworkCore;

namespace MatchesAPI.Endpoints;

public static class StandingsEndpoints
{
    public static RouteGroupBuilder MapStandingsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/standings")
                        .WithParameterValidation();

        group.MapGet("/", async (MatchStoreContext dbContext) =>
        {
            var standings = await dbContext.Standings
                                .Include(s => s.Team)
                                .OrderBy(s => s.Rank)
                                .Select(s => s.ToDto())
                                .AsNoTracking()
                                .ToListAsync();

            return Results.Ok(standings);
        });

        group.MapGet("/id/{teamId}", async(int teamId, MatchStoreContext dbContext) =>
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

        group.MapGet("/rank/{rank}", async(int rank, MatchStoreContext dbContext) =>
        {
            var standing = await dbContext.Standings
                                .Include(s => s.Team)
                                .Where(s => s.Rank == rank)
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