using FootyLeague.Data.Common.Repositories;
using FootyLeague.Data.Models;
using FootyLeague.Services.Data.Contracts;
using FootyLeague.Web.ViewModels.Match;
using FootyLeague.Web.ViewModels.Team;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Match = FootyLeague.Data.Models.Match;

namespace FootyLeague.Services.Data.Tests
{
    public class MatchServiceTests : BaseServiceTests
    {
        private const int TestId = 1;

        private const string TestName = "Test";
        private const string TestNameTeam = "Team";
        private const string TestNameNull = null;

        private IMatchService MatchServiceMoq => this.ServiceProvider.GetRequiredService<IMatchService>();

        [Fact]
        public async Task GetAllMatchesAsync_ShouldReturnAllMatches()
        {

            var teams = await this.CreateTestTeamsAsync();

            var matches = await this.CreateTestMatchesAsync(teams);

            var result = await this.MatchServiceMoq.GetAllMatchesAsync<Match>();

            Assert.Equal(2, result.Count());
            Assert.Equal(1, result.First().Id);
            Assert.Equal(2, result.Last().Id);
        }

        [Fact]
        public async Task CreateMatchAsync_ShouldCreateMatch_WhenValidInputProvided()
        {
            var teams = await this.CreateTestTeamsAsync();

            var homeTeam = teams.ElementAt(0);
            var awayTeam = teams.ElementAt(1);
            var model = new CreateMatchInputModel
            {
                HomeTeam = new CreateMatchTeamViewModel { Id = homeTeam.Id },
                AwayTeam = new CreateMatchTeamViewModel { Id = awayTeam.Id },
                IsPlayed = false,
                AwayTeamScore = 0,
                HomeTeamScore = 0,
            };

            await this.MatchServiceMoq.CreateMatchAsync(model);

            var result = await this.MatchServiceMoq.GetAllMatchesAsync<Match>();

            Assert.Single(result);
            Assert.Equal(1, result.First().Id);
        }

        [Fact]
        public async Task Delete_ShouldDeleteMatch_WhenValidIdProvided()
        {
            var teams = await this.CreateTestTeamsAsync();
            var matchId = 1;

            var match = await this.CreateTestMatchAsync(teams, matchId);

            await this.MatchServiceMoq.Delete(matchId);

            var isDeleted = this.DbContext.Find<Match>(matchId).IsDeleted;

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task GetMatchAsync_ShouldReturnMatch_WhenValidIdProvided()
        {
            var teams = await this.CreateTestTeamsAsync();
            var matchId = 1;

            var match = await this.CreateTestMatchAsync(teams, matchId);

            var result = await this.MatchServiceMoq.GetMatchAsync<Match>(matchId);

            Assert.NotNull(result);
            Assert.Equal(matchId, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateMatch_WhenValidInputProvided()
        {
            var teams = await this.CreateTestTeamsAsync();
            var matchId = 1;

            var match = await this.CreateTestMatchAsync(teams, matchId);
            var inputModel = new EditMatchInputModel
            {
                HomeTeam = new CreateMatchTeamViewModel { Id = 1 },
                AwayTeam = new CreateMatchTeamViewModel { Id = 2 },
                IsPlayed = true,
                HomeTeamScore = 2,
                AwayTeamScore = 1,
            };

            await this.MatchServiceMoq.UpdateAsync(matchId, inputModel);

            // Assert
            Assert.True(match.IsPlayed);
            Assert.Equal(2, match.HomeTeamScore);
            Assert.Equal(1, match.AwayTeamScore);
        }

        [Fact]
        public async Task RestoreSuccessfully()
        {
            var teams = await this.CreateTestTeamsAsync();
            var matchId = 1;

            var match = await this.CreateTestMatchAsync(teams, matchId);

            match.IsDeleted = true;

            await this.DbContext.SaveChangesAsync();
            await this.MatchServiceMoq.Restore(matchId);

            Assert.False(match.IsDeleted);
        }

        public async Task<List<Team>> CreateTestTeamsAsync()
        {
            var teams = new List<Team>
            {
                new Team { Id = 1, Name = "TeamA", Points = 10 },
                new Team { Id = 2, Name = "TeamB", Points = 5 },
            };

            foreach (var team in teams)
            {
                await this.DbContext.Teams.AddAsync(team);
            }

            await this.DbContext.SaveChangesAsync();

            return teams;
        }

        public async Task<List<Match>> CreateTestMatchesAsync(List<Team> teams)
        {
            var matches = new List<Match>
            {
                new Match { Id = 1, Date = DateTime.UtcNow.AddDays(-2), AwayTeam = teams.ElementAt(0), HomeTeam = teams.ElementAt(1) },
                new Match { Id = 2, Date = DateTime.UtcNow.AddDays(-1), AwayTeam = teams.ElementAt(1), HomeTeam = teams.ElementAt(0) },
            };

            foreach (var match in matches)
            {
                await this.DbContext.Matches.AddAsync(match);
            }

            await this.DbContext.SaveChangesAsync();

            return matches;
        }

        public async Task<Match> CreateTestMatchAsync(List<Team> teams, int matchId)
        {
            var homeTeam = teams.ElementAt(0);
            var awayTeam = teams.ElementAt(1);
            var match = new Match
            {
                Id = matchId,
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                IsPlayed = false,
                AwayTeamScore = 0,
                HomeTeamScore = 0,
            };
            await this.DbContext.AddAsync(match);
            await this.DbContext.SaveChangesAsync();

            return match;
        }
    }
}
