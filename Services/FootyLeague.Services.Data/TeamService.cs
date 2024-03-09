namespace FootyLeague.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FootyLeague.Data.Common.Repositories;
    using FootyLeague.Data.Models;
    using FootyLeague.Services.Data.Contracts;
    using FootyLeague.Services.Mapping;

    public class TeamService : ITeamService
    {
        private readonly IDeletableEntityRepository<Team> teamRepository;

        public TeamService(IDeletableEntityRepository<Team> teamRepository)
        {
                this.teamRepository = teamRepository;
        }

        public async Task<IEnumerable<T>> GetAllTeamsAsync<T>()
        {
            IQueryable<Team> query =
               this.teamRepository.All().OrderBy(x => x.Points);

            return query.To<T>().ToList();
        }
    }
}
