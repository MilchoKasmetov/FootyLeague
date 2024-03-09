using FootyLeague.Data.Common.Repositories;
using FootyLeague.Data.Models;
using FootyLeague.Services.Data.Contracts;
using FootyLeague.Services.Mapping;
using FootyLeague.Web.ViewModels.Match;
using FootyLeague.Web.ViewModels.Team;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootyLeague.Services.Data
{
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
    }
}
