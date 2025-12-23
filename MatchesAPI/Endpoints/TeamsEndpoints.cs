using MatchesAPI.Dtos;
using MatchesAPI.Entities;
using MatchesAPI.Data;
using MatchesAPI.Mapping;
using Microsoft.EntityFrameworkCore;

namespace MatchesAPI.Endpoints;

public static class TeamsEndpoints
{
    public static RouteGroupBuilder MapTeamsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/teams")
                        .WithParameterValidation();

        group.MapGet("/", async (MatchStoreContext dbContext) =>
        {
            var teams = await dbContext.Teams
                                .Include(t => t.Standing)
                                .OrderBy(t => t.Name)
                                .Select(t => t.ToDto())
                                .AsNoTracking()
                                .ToListAsync();

            return Results.Ok(teams);
        });

        return group;
    }
    
}