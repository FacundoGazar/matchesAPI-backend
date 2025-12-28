using MatchesAPI.Dtos;
using MatchesAPI.Entities;

namespace MatchesAPI.Mapping;

public static class MatchMapping
{
    public static MatchDto ToDto(this Match match)
    {
        return new (
                match.Id,
                match.HomeTeamId,
                match.HomeTeam!.Name,
                match.HomeScore,
                match.AwayTeamId,
                match.AwayTeam!.Name,
                match.AwayScore,
                match.MatchDate,
                match.Week,
                match.Day,
                match.MatchTime,
                match.Attendance,
                match.Venue,
                match.Referee
            );
    }
}