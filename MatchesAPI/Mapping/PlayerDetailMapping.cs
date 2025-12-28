using MatchesAPI.Dtos;
using MatchesAPI.Entities;

namespace MatchesAPI.Mapping;

public static class PlayerDetailMapping
{
    public static PlayerDetailDto ToDto(this PlayerDetail playerDetail)
    {
        return new (
                playerDetail.PlayerId,
                playerDetail.Player.Name,
                playerDetail.Player.Nation,
                playerDetail.Player.Age,
                playerDetail.TeamId,
                playerDetail.Team.Name,
                playerDetail.Position,
                playerDetail.Goals,
                playerDetail.Assists,
                playerDetail.Played,
                playerDetail.Starts,
                playerDetail.Minutes,
                playerDetail.Yellow,
                playerDetail.Red,
                playerDetail.Weekly,
                playerDetail.Annual
            );
    }
}