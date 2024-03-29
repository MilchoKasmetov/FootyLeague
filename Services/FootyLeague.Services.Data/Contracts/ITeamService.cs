﻿namespace FootyLeague.Services.Data.Contracts
{
    using FootyLeague.Web.ViewModels.Team;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITeamService
    {
        Task<IEnumerable<T>> GetAllTeamsAsync<T>();

        Task UpdateAllTeamsStats();

        Task CreateTeamAsync(CreateTeamInputModel model);

        Task<T> GetTeamAsync<T>(int id);

        Task Delete(int id);

        Task Restore(int id);

        Task UpdateAsync(int id, EditTeamInputModel input);
    }
}
