namespace FootyLeague.Services.Data.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FootyLeague.Data.Models;
    using FootyLeague.Services.Data.Contracts;
    using FootyLeague.Web.ViewModels.Team;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class TeamServiceTests : BaseServiceTests
    {
        private const int TestId = 1;

        private const string TestName = "Test";
        private const string TestNameTeam = "Team";
        private const string TestNameNull = null;

        private ITeamService TeamServiceMoq => this.ServiceProvider.GetRequiredService<ITeamService>();

        [Fact]
        public async Task CreateTeamAsyncSuccessfully()
        {
            var teamTestWithName = new CreateTeamInputModel()
            {
                Name = TestName,
            };
            var teamTestNullName = new CreateTeamInputModel()
            {
                Name = TestNameNull,
            };

            await this.TeamServiceMoq.CreateTeamAsync(teamTestWithName);
            await this.TeamServiceMoq.CreateTeamAsync(teamTestWithName);
            await this.TeamServiceMoq.CreateTeamAsync(teamTestNullName);

            var list = await this.DbContext.Teams.ToListAsync();

            Assert.Single(list);
            Assert.Equal(TestName, list.FirstOrDefault(x => x.Name == TestName).Name);
        }

        [Fact]
        public async Task GetForUpdateAsyncSuccessfully()
        {
            var teamTestWithName = new Team()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.Teams.AddAsync(teamTestWithName);
            await this.DbContext.SaveChangesAsync();
            var output = await this.TeamServiceMoq.GetTeamAsync<Team>(TestId);

            Assert.Equal(TestName, output.Name);
        }

        [Fact]
        public async Task UpdateAsyncSuccessfully()
        {
            var teamTestWithName = new Team()
            {
                Name = TestName,
            };

            var teamTestTeam = new EditTeamInputModel()
            {
                Name = TestNameTeam,
            };

            await this.DbContext.Teams.AddAsync(teamTestWithName);
            await this.DbContext.SaveChangesAsync();
            await this.TeamServiceMoq.UpdateAsync(TestId, teamTestTeam);

            Assert.Equal(TestNameTeam, teamTestWithName.Name);
        }

        [Fact]
        public async Task DeleteSuccessfully()
        {
            var teamTestWithName = new Team()
            {
                Name = TestName,
            };
            await this.DbContext.Teams.AddAsync(teamTestWithName);
            await this.DbContext.SaveChangesAsync();
            await this.TeamServiceMoq.Delete(TestId);

            Assert.Equal(TestName, teamTestWithName.Name);
            Assert.True(teamTestWithName.IsDeleted);
        }

        [Fact]
        public async Task RestoreSuccessfully()
        {
            var teamTestWithName = new Team()
            {
                Name = TestName,
                IsDeleted = true,
            };
            await this.DbContext.Teams.AddAsync(teamTestWithName);
            await this.DbContext.SaveChangesAsync();
            await this.TeamServiceMoq.Restore(1);

            Assert.Equal(TestName, teamTestWithName.Name);
            Assert.False(teamTestWithName.IsDeleted);
        }

        [Fact]
        public async Task GetTeamsAsyncSuccessfully()
        {
            var teamTestWithName = new Team()
            {
                Name = TestName,
                IsDeleted = false,
            };
            await this.DbContext.Teams.AddAsync(teamTestWithName);
            var teamTestTeam = new Team()
            {
                Name = TestNameTeam,
                IsDeleted = false,
            };

            await this.DbContext.Teams.AddAsync(teamTestWithName);
            await this.DbContext.Teams.AddAsync(teamTestTeam);
            await this.DbContext.SaveChangesAsync();

            var list = await this.TeamServiceMoq.GetAllTeamsAsync<Team>();

            Assert.Equal(2, list.Count());
        }

        [Fact]
        public async Task GetAllTeamsAsync_ReturnsAllTeamsOrderedByPoints()
        {
            var teams = new List<Team>
            {
                new Team { Id = 1, Name = "TeamA", Points = 10 },
                new Team { Id = 2, Name = "TeamB", Points = 5 },
                new Team { Id = 3, Name = "TeamC", Points = 15 },
            };
            foreach (var team in teams)
            {
                await this.DbContext.Teams.AddAsync(team);
            }

            await this.DbContext.SaveChangesAsync();

            var result = await this.TeamServiceMoq.GetAllTeamsAsync<Team>();

            Assert.Equal(3, result.Count());
            Assert.Equal("TeamB", result.ElementAt(0).Name); // Teams should be ordered by points
            Assert.Equal("TeamA", result.ElementAt(1).Name);
            Assert.Equal("TeamC", result.ElementAt(2).Name);
        }
    }
}
