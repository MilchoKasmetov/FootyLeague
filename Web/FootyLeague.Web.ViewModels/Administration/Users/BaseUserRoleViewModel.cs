namespace FootyLeague.API.ViewModels.Administration.Users
{
    using System;

    using AutoMapper;
    using FootyLeague.Data.Models;
    using FootyLeague.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public abstract class BaseUserRoleViewModel : IMapFrom<IdentityUserRole<string>>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public string RoleId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationRole, BaseUserRoleViewModel>()
    .ForMember(x => x.Name, options =>
    {
        options.MapFrom(p => p.Name);


    });

        }
    }
}
