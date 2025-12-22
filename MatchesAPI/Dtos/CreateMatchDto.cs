using System.ComponentModel.DataAnnotations;

namespace MatchesAPI.Dtos;

public record class CreateMatchDto(
    [Required] int HomeTeamId,
    [Required] int AwayTeamId,
    [Required] DateTime MatchDate,
    [Required][Range(0, 300000)] int Attendance
);