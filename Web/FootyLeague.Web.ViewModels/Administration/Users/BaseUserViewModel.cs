namespace FootyLeague.API.ViewModels.Administration.Users
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using FootyLeague.Data.Models;
    using FootyLeague.Services.Mapping;

    public abstract class BaseUserViewModel : IMapFrom<ApplicationUser>
    {
        protected BaseUserViewModel()
        {
            this.Roles = new HashSet<UserRoleViewModel>();
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<UserRoleViewModel> Roles { get; set; }


    }
}
