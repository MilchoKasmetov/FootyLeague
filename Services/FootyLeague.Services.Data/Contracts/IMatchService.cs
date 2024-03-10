namespace FootyLeague.Services.Data.Contracts
{
    using FootyLeague.Web.ViewModels.Match;
    using FootyLeague.Web.ViewModels.Team;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMatchService
    {
        Task<IEnumerable<T>> GetAllMatchesAsync<T>();

        Task CreateMatchAsync(CreateMatchInputModel model);

        Task<T> GetMatchAsync<T>(int id);

        Task Delete(int id);

        Task Restore(int id);

        Task UpdateAsync(int id, EditMatchInputModel input);
    }
}
