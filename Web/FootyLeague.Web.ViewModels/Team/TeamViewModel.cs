namespace FootyLeague.Web.ViewModels.Team
{
    using FootyLeague.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FootyLeague.Data.Models;

    public class TeamViewModel : BaseTeamViewModel
    {
        public string Name { get; set; }

        public int Points { get; set; }
    }
}
