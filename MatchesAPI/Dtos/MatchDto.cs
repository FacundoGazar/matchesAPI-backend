namespace MatchesAPI.Dtos;

public record class MatchDto(
    int Id,
    string HomeTeam,
    string AwayTeam,
    DateTime MatchDate,
    decimal Attendance
);