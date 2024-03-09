using FootyLeague.Data.Common.Repositories;
using FootyLeague.Data.Models;
using FootyLeague.Services.Data.Contracts;
using FootyLeague.Services.Mapping;
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

        public MatchService(IDeletableEntityRepository<Match> matchRepository)
        {
            this.matchRepository = matchRepository;
        }

        public async Task<IEnumerable<T>> GetAllMatchesAsync<T>()
        {
            IQueryable<Match> query = this.matchRepository.All().OrderBy(x => x.Date);

            return query.To<T>().ToList();
        }
    }
}
