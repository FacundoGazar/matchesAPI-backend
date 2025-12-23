using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchesAPI.Entities;

[Table("team")]
public class Team
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("players")]
    public int Players { get; set; }

    [Column("age")]
    public decimal Age { get; set; }

    [Column("possession")]
    public decimal Possession { get; set; }

    [Column("penalty_kicks")]
    public int PenaltyKicks { get; set; }

    [Column("penalty_kick_attempts")]
    public int PenaltyKickAttempts { get; set; }

    [Column("expected_goals")]
    public decimal ExpectedGoals { get; set; }

    [Column("expected_assists")]
    public decimal ExpectedAssists { get; set; }

    public Standing Standing { get; set; } = null!;
}
