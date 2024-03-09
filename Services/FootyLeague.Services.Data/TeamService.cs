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
    using FootyLeague.Web.ViewModels.Team;
    using Microsoft.EntityFrameworkCore;

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

        public async Task UpdateTeamScore()
        {
            var teams = await this.teamRepository.All().ToListAsync();

            foreach (var team in teams)
            {
                team.CalculatePoints();
            }

            await this.teamRepository.SaveChangesAsync();
        }

        public async Task CreateTeamAsync(CreateTeamInputModel model)
        {
            var team = new Team()
            {
                Name = model.Name,
            };

            var teams = await this.teamRepository.All().ToListAsync();

            if (!teams.Any(x => x.Name == model.Name) && model.Name != null)
            {
                await this.teamRepository.AddAsync(team);
                await this.teamRepository.SaveChangesAsync();
            }
        }
    }
}
