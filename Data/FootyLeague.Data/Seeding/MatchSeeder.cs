namespace FootyLeague.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;

    using FootyLeague.Data.Models;

    public class MatchSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Matches.Any())
            {
                return;
            }

            var teams = dbContext.Teams.ToList();

            for (int i = 0; i < teams.Count; i++)
            {
                for (int j = i + 1; j < teams.Count; j++)
                {
                    var match = new Match
                    {
                        HomeTeam = teams[i],
                        AwayTeam = teams[j],
                        Date = DateTime.Now,
                        IsPlayed = false,
                        HomeTeamScore = i,
                        AwayTeamScore = j + 1,
                    };
                    await dbContext.Matches.AddAsync(match);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
