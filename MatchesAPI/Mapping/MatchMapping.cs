using MatchesAPI.Dtos;
using MatchesAPI.Entities;

namespace MatchesAPI.Mapping;

public static class MatchMapping
{
    public static Match ToEntity(this CreateMatchDto match)
    {
        return new Match()
            {
                HomeTeamId = match.HomeTeamId,
                AwayTeamId = match.AwayTeamId,
                MatchDate = match.MatchDate
            };
    }
    
    public static MatchDto ToDto(this Match match)
    {
        return new (
                match.Id,
                match.HomeTeam!.Name,
                match.AwayTeam!.Name,
                match.MatchDate
            );
    }
}