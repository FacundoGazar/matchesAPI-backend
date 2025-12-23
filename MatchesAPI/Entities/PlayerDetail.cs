using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchesAPI.Entities;

[Table("player_detail")]
public class PlayerDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("player_id")]
    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    [Column("team_id")]
    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;

    [Column("position")]
    public string Position { get; set; } = null!;

    [Column("played")]
    public int Played { get; set; }

    [Column("starts")]
    public int Starts { get; set; }

    [Column("minutes")]
    public int Minutes { get; set; }

    [Column("goals")]
    public int Goals { get; set; }

    [Column("assists")]
    public int Assists { get; set; }

    [Column("yellow")]
    public int Yellow { get; set; }

    [Column("red")]
    public int Red { get; set; }

    [Column("age")]
    public int Age { get; set; }
    
    [Column("weekly")]
    public decimal Weekly { get; set; }

    [Column("annual")]
    public decimal Annual { get; set; }
}
