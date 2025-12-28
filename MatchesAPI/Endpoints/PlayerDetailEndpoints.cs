using MatchesAPI.Dtos;
using MatchesAPI.Entities;
using MatchesAPI.Data;
using MatchesAPI.Mapping;
using Microsoft.EntityFrameworkCore;

namespace MatchesAPI.Endpoints;

public static class PlayerDetailEndpoints
{
    public static RouteGroupBuilder MapPlayerDetailEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/playerdetails")
                        .WithParameterValidation();

        group.MapGet("/", async (
            MatchStoreContext dbContext,
            int? teamId,
            int page = 1,
            int pageSize = 1) =>
        {
            var query = dbContext.PlayerDetails
                .Include(pd => pd.Player)
                .Include(pd => pd.Team)
                .AsNoTracking()
                .AsQueryable();

            if (teamId.HasValue)
            {
                query = query.Where(pd => pd.TeamId == teamId.Value);
            }

            var totalCount = await query.CountAsync();

            var playerDetails = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(pd => pd.ToDto())
                .ToListAsync();

            return Results.Ok(new
            {
                data = playerDetails,
                page,
                pageSize,
                totalCount,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            });
        });


        group.MapGet("/{id}", async (int id, MatchStoreContext dbContext) =>
        {
            var playerDetail = await dbContext.PlayerDetails
                                    .Include(pd => pd.Player)
                                    .Include(pd => pd.Team)
                                    .Where(pd => pd.PlayerId == id)
                                    .Select(pd => pd.ToDto())
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();
            
            return playerDetail is null
                ? Results.NotFound()
                : Results.Ok(playerDetail);
        });

        return group;
    }
    
}