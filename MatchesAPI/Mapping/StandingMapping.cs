using MatchesAPI.Dtos;
using MatchesAPI.Entities;

namespace MatchesAPI.Mapping;

public static class StandingMapping
{
    public static StandingDto ToDto(this Standing standing)
    {
        return new (
                standing.Rank,
                standing.TeamId,
                standing.Team.Name,
                standing.Win,
                standing.Loss,
                standing.Draw,
                standing.Conceded,
                standing.Points
            );
    }
}