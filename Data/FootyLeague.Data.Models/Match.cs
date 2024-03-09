namespace FootyLeague.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FootyLeague.Data.Common.Models;

    public class Match : BaseDeletableModel<int>
    {
        [Required]
        public int MatchId { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        public int HomeTeamScore { get; set; }

        [Required]
        public int AwayTeamScore { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Team HomeTeam { get; set; }

        [Required]
        public Team AwayTeam { get; set; }

        [Required]
        public bool IsPlayed { get; set; }
    }
}
