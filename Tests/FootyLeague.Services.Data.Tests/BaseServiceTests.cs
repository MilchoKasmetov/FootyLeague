namespace FootyLeague.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using FootyLeague.Data;
    using FootyLeague.Data.Common.Repositories;
    using FootyLeague.Data.Models;
    using FootyLeague.Data.Repositories;
    using FootyLeague.Services.Data.Contracts;
    using FootyLeague.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class BaseServiceTests : IDisposable
    {
        protected BaseServiceTests()
        {
            var services = this.SetServices();

            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected ApplicationDbContext DbContext { get; set; }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(
                opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddIdentityCore<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                            .AddRoles<ApplicationRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddTransient<IMatchService, MatchService>();
            services.AddTransient<ITeamService, TeamService
                >();

            var context = new DefaultHttpContext();
            services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor { HttpContext = context });

            AutoMapperConfig.RegisterMappings(typeof(Team).GetTypeInfo().Assembly);

            return services;
        }

        public void Dispose()
        {
            this.DbContext.Database.EnsureDeleted();
            this.SetServices();
        }
    }
}
