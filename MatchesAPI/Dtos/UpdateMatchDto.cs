using System.ComponentModel.DataAnnotations;

namespace MatchesAPI.Dtos;

public record class UpdateMatchDto(
    [Required] int HomeTeamId,
    [Required] int AwayTeamId,
    [Required] DateTime MatchDate,
    [Required] string Round,
    [Required] int HomeScore,
    [Required] int AwayScore
);