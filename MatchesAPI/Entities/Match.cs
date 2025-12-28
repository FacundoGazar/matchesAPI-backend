using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchesAPI.Entities;

[Table("fixture")]
public class Match
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("week")]
    public int Week { get; set; }

    [Column("day")]
    public string Day { get; set; } = null!;

    [Column("match_date")]
    public DateTime MatchDate { get; set; }

    [Column("match_time")]
    public TimeOnly MatchTime { get; set; }

    [Column("home_team_id")]
    public int HomeTeamId { get; set; }
    public Team HomeTeam { get; set; } = null!;

    [Column("away_team_id")]
    public int AwayTeamId { get; set; }
    public Team AwayTeam { get; set; } = null!;

    [Column("home_score")]
    public int HomeScore { get; set; }

    [Column("away_score")]
    public int AwayScore { get; set; }

    [Column("attendance")]
    public long Attendance { get; set; }

    [Column("venue")]
    public string Venue { get; set; } = null!;
    
    [Column("referee")]
    public string Referee { get; set; } = null!;
}
