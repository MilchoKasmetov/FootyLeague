namespace FootyLeague.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FootyLeague.API.ViewModels.Administration.Users;
    using FootyLeague.Data.Models;

    public interface IAdminService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();

        Task<UserViewModel> GetUserAsync(string id);

        Task<IEnumerable<string>> FilterRolesThatExistsAsync(IEnumerable<string> roles);

        Task<IEnumerable<string>> FilterRolesThatAreNotAlreadySetAsync(IEnumerable<string> roles, ApplicationUser user);
    }
}
