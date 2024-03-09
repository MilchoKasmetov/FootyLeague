namespace FootyLeague.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using FootyLeague.Data.Common.Models;

    public class Team : BaseDeletableModel<int>
    {
        public Team()
        {
            this.Matches = new HashSet<Match>();
            this.HomeMatches = new HashSet<Match>();
            this.AwayMatches = new HashSet<Match>();

        }

        [Required]
        [StringLength(100)]
        [MinLength(3)]
        public string Name { get; set; }

        public int Points => this.CalculatePoints();

        public ICollection<Match> Matches { get; set; }

        public ICollection<Match> HomeMatches { get; set; }

        public ICollection<Match> AwayMatches { get; set; }

        public int Wins => this.Matches.Count(match => IsTeamWinner(match, this));

        public int Draws => this.Matches.Count(match => IsDraw(match));

        public int Losses => this.Matches.Count(match => !IsTeamWinner(match, this) && !IsDraw(match));

        private static bool IsDraw(Match match)
        {
            return match.HomeTeamScore == match.AwayTeamScore;
        }

        private static bool IsTeamWinner(Match match, Team team)
        {
            if (match.HomeTeamId == team.Id && match.HomeTeamScore > match.AwayTeamScore)
            {
                return true;
            }

            if (match.AwayTeamId == team.Id && match.AwayTeamScore > match.HomeTeamScore)
            {
                return true;
            }

            return false;
        }

        private int CalculatePoints()
        {
            return (this.Wins * 3) + (this.Draws * 1);
        }
    }
}
