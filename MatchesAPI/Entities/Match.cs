namespace MatchesAPI.Entities;

public class Match
{
    public int Id { get; set; }

    public required DateTime MatchDate { get; set; }

    public required int Attendance { get; set; }

    public int HomeTeamId { get; set; }

    public Team HomeTeam { get; set; } = null!;

    public int AwayTeamId { get; set; }

    public Team AwayTeam { get; set; } = null!;
}
