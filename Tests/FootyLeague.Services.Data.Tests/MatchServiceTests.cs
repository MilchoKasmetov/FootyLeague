using FootyLeague.Data.Common.Repositories;
using FootyLeague.Data.Models;
using FootyLeague.Services.Data.Contracts;
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

 

    }
}
