namespace FootyLeague.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FootyLeague.Data.Models;

    public class TeamSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Teams.Any())
            {
                return;
            }

            await dbContext.Teams.AddAsync(new Team() { Name = "Real Madrid" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Manchester United" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Liverpool" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Bayern Munich" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Paris Saint-Germain" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Juventus" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Chelsea" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Arsenal" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Manchester City" });
            await dbContext.Teams.AddAsync(new Team() { Name = "AC Milan" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Inter Milan" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Atletico Madrid" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Borussia Dortmund" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Tottenham Hotspur" });
            await dbContext.Teams.AddAsync(new Team() { Name = "FC Porto" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Ajax" });
            await dbContext.Teams.AddAsync(new Team() { Name = "RB Leipzig" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Napoli" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Sevilla" });
            await dbContext.Teams.AddAsync(new Team() { Name = "Benfica" });

            await dbContext.SaveChangesAsync();
        }
    }
}
