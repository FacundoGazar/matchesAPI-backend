namespace MatchesAPI.Dtos;

public record class TeamDto(
    int Id,
    String Name,
    int Players,
    decimal Age,
    decimal Possession,
    int PenaltyKicks,
    int PenaltyKickAttempts,
    decimal ExpectedGoals,
    decimal ExpectedAssists,
    int Rank
);