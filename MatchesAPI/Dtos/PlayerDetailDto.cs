namespace MatchesAPI.Dtos;

public record class PlayerDetailDto(
    int PlayerId,
    string PlayerName,
    string PlayerNation,
    int PlayerAge,
    int TeamId,
    string TeamName,
    string Position,
    int Goals,
    int Assists,
    int Played,
    int Starts,
    int Minutes,
    int Yellow,
    int Red,
    decimal Weekly,
    decimal Annual
);
