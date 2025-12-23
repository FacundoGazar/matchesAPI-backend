using MatchesAPI.Dtos;
using MatchesAPI.Entities;

namespace MatchesAPI.Mapping;

public static class TeamMapping
{
    public static TeamDto ToDto(this Team team)
    {
        return new (
                team.Id,
                team.Name,
                team.Players,
                team.Age,
                team.Possession,
                team.PenaltyKicks,
                team.PenaltyKickAttempts,
                team.ExpectedGoals,
                team.ExpectedAssists,
                team.Standing.Rank
            );
    }
}