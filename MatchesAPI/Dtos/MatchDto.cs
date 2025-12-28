namespace MatchesAPI.Dtos;

public record class MatchDto(
    int Id,
    int HomeTeamId,
    string HomeTeam,
    int HomeScore,
    int AwayTeamId,
    string AwayTeam,
    int AwayScore,
    DateTime MatchDate,
    int Week,
    string Day,
    TimeOnly MatchTime,
    long Attendance,
    string Venue,
    string Referee
);