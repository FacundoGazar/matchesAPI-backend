using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchesAPI.Entities;

[Table("standings")]
public class Standing
{
    [Key]
    [Column("team_id")]
    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;

    [Column("rank")]
    public int Rank { get; set; }

    [Column("win")]
    public int Win { get; set; }

    [Column("loss")]
    public int Loss { get; set; }

    [Column("draw")]
    public int Draw { get; set; }

    [Column("conceded")]
    public int Conceded { get; set; }
    
    [Column("points")]
    public int Points { get; set; }
}
