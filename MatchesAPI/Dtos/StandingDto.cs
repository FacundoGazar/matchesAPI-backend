namespace MatchesAPI.Dtos;

public record class StandingDto(
    int Rank,
    int TeamId,
    String TeamName,
    int Win,
    int Loss,
    int Draw,
    int Conceded,
    int Points
);