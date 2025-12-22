using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchesAPI.Entities;

[Table("match")]
public class Match
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("match_date", TypeName = "timestamp without time zone")]
    public required DateTime MatchDate { get; set; }

    [Column("round")]
    public required string Round { get; set; }

    [Column("home_team_id")]
    public int HomeTeamId { get; set; }

    [Column("home_score")]
    public int HomeScore { get; set; }

    public Team HomeTeam { get; set; } = null!;

    [Column("away_team_id")]
    public int AwayTeamId { get; set; }

    [Column("away_score")]
    public int AwayScore { get; set; }

    public Team AwayTeam { get; set; } = null!;
}
