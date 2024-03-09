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
        private readonly IDeletableEntityRepository<Match> matchRepository;

        public TeamService(IDeletableEntityRepository<Team> teamRepository,IDeletableEntityRepository<Match> matchRepository)
        {
            this.teamRepository = teamRepository;
            this.matchRepository = matchRepository;
        }

        public async Task<IEnumerable<T>> GetAllTeamsAsync<T>()
        {
            IQueryable<Team> query =
               this.teamRepository.All().OrderBy(x => x.Points);

            return query.To<T>().ToList();
        }

        public async Task UpdateAllTeamsStats()
        {
            var matches = await this.matchRepository.All().ToListAsync();
            var teams = await this.teamRepository.All().ToListAsync();

            foreach (var match in matches)
            {
                var homeTeam = teams.FirstOrDefault(t => t.Id == match.HomeTeamId);
                var awayTeam = teams.FirstOrDefault(t => t.Id == match.AwayTeamId);

                if (homeTeam != null && awayTeam != null)
                {
                    this.UpdateTeamStats(homeTeam, awayTeam, match);
                }
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

        private void UpdateTeamStats(Team homeTeam, Team awayTeam, Match match)
        {
            homeTeam.Matches.Add(match);
            homeTeam.HomeMatches.Add(match);
            awayTeam.Matches.Add(match);
            awayTeam.AwayMatches.Add(match);

            if (!match.IsPlayed)
            {
                if (match.HomeTeamScore > match.AwayTeamScore)
                {
                    homeTeam.Wins++;
                    awayTeam.Losses++;
                }
                else if (match.HomeTeamScore == match.AwayTeamScore)
                {
                    homeTeam.Draws++;
                    awayTeam.Draws++;
                }
                else
                {
                    homeTeam.Losses++;
                    awayTeam.Wins++;
                }

                match.IsPlayed = true;
            }
        }

        public async Task<T> GetTeamAsync<T>(int id)
        {
            IQueryable<Team> query = this.teamRepository.All().Where(x => x.Id == id);

            return await query.To<T>().FirstOrDefaultAsync();
        }

        public async Task Delete(int id)
        {
            var team = await this.teamRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.teamRepository.Delete(team);
            await this.teamRepository.SaveChangesAsync();
        }

        public async Task Restore(int id)
        {
            var team = await this.teamRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            this.teamRepository.Undelete(team);
            await this.teamRepository.SaveChangesAsync();
        }
    }
}
