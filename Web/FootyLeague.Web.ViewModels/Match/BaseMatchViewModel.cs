namespace FootyLeague.Web.ViewModels.Match
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FootyLeague.Data.Models;
    using FootyLeague.Services.Mapping;

    public abstract class BaseMatchViewModel : IMapFrom<Match>
    {
        public int MatchId { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        public bool IsPlayed { get; set; }
    }
}
