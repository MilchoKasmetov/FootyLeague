namespace FootyLeague.Web.ViewModels.Team
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using FootyLeague.Data.Models;
    using FootyLeague.Services.Mapping;

    public abstract class BaseTeamViewModel : IMapFrom<Team>
    {
        public int Id { get; set; }
    }
}
