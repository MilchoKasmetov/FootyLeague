namespace FootyLeague.Services.Data.Contracts
{
    using FootyLeague.Web.ViewModels.Match;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMatchService
    {
        Task<IEnumerable<T>> GetAllMatchesAsync<T>();

        Task CreateMatchAsync(CreateMatchInputModel model);
    }
}
