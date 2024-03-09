namespace FootyLeague.Web.ViewModels.Match
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FootyLeague.Services.Mapping;
    using FootyLeague.Data.Models;
    using FootyLeague.Web.ViewModels.Team;

    public abstract class BaseMatchInputModel : IMapFrom<Match>
    {
        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public CreateMatchTeamViewModel HomeTeam { get; set; }

        public CreateMatchTeamViewModel AwayTeam { get; set; }

        public bool IsPlayed { get; set; }
    }
}
