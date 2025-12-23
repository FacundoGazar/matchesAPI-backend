namespace MatchesAPI.Dtos;

public record class MatchDto(
    int Id,
    string HomeTeam,
    int HomeScore,
    string AwayTeam,
    int AwayScore,
    DateTime MatchDate
);