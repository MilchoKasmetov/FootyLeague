using FootyLeague.Data.Common.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootyLeague.Data.Models
{
    public class Match : BaseDeletableModel<int>
    {
        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public DateTime Date { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }
    }
}
