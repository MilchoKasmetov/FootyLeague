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
    using FootyLeague.Web.ViewModels.Match;
    using FootyLeague.Web.ViewModels.Team;
    using Microsoft.EntityFrameworkCore;

    public class MatchService : IMatchService
    {
        private readonly IDeletableEntityRepository<Match> matchRepository;
        private readonly IDeletableEntityRepository<Team> teamRepository;

        public MatchService(IDeletableEntityRepository<Match> matchRepository, IDeletableEntityRepository<Team> teamRepository)
        {
            this.matchRepository = matchRepository;
            this.teamRepository = teamRepository;
        }

        public async Task<IEnumerable<T>> GetAllMatchesAsync<T>()
        {
            IQueryable<Match> query = this.matchRepository.All().OrderBy(x => x.Date);

            return query.To<T>().ToList();
        }

        public async Task CreateMatchAsync(CreateMatchInputModel model)
        {
            var hometeam = await this.teamRepository.All().FirstOrDefaultAsync(x => x.Id == model.HomeTeam.Id);
            var awayteam = await this.teamRepository.All().FirstOrDefaultAsync(x => x.Id == model.AwayTeam.Id);

            if (hometeam == null || awayteam == null)
            {
                return;
            }

            var match = new Match()
            {
                HomeTeam = hometeam,
                AwayTeam = awayteam,
                IsPlayed = model.IsPlayed,
                AwayTeamScore = model.AwayTeamScore,
                HomeTeamScore = model.HomeTeamScore,
                Date = DateTime.UtcNow,

            };

            var findMatch = await this.matchRepository.All().FirstOrDefaultAsync(x => x == match);

            if (findMatch == null)
            {
                await this.matchRepository.AddAsync(match);
                await this.matchRepository.SaveChangesAsync();
            }
        }

        public async Task<T> GetMatchAsync<T>(int id)
        {
            IQueryable<Match> query = this.matchRepository.All().Where(x => x.Id == id);

            return await query.To<T>().FirstOrDefaultAsync();
        }

        public async Task Delete(int id)
        {
            var match = await this.matchRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.matchRepository.Delete(match);
            await this.matchRepository.SaveChangesAsync();
        }

        public async Task Restore(int id)
        {
            var match = await this.matchRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            this.matchRepository.Undelete(match);
            await this.matchRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, EditMatchInputModel input)
        {
            var match = await this.matchRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            if (match == null)
            {
                return;
            }

            var homeTeam = await this.teamRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == input.HomeTeam.Id);
            if (homeTeam == null)
            {
                return;
            }

            var awayTeam = await this.teamRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == input.AwayTeam.Id);
            if (awayTeam == null)
            {
                return;
            }

            match.HomeTeamScore = input.HomeTeamScore;
            match.AwayTeamScore = input.AwayTeamScore;
            match.HomeTeam = homeTeam;
            match.AwayTeam = awayTeam;
            match.IsPlayed = input.IsPlayed;

            await this.matchRepository.SaveChangesAsync();
        }
    }
}
