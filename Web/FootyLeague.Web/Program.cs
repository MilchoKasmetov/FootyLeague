namespace FootyLeague.Web
{
    using System.Reflection;

    using FootyLeague.Data;
    using FootyLeague.Data.Common;
    using FootyLeague.Data.Common.Repositories;
    using FootyLeague.Data.Models;
    using FootyLeague.Data.Repositories;
    using FootyLeague.Data.Seeding;
    using FootyLeague.Services.Data;
    using FootyLeague.Services.Mapping;
    using FootyLeague.Services.Messaging;
    using FootyLeague.Web.Infrastructure;
    using FootyLeague.Web.ViewModels;
    using FootyLeague.Web.ViewModels.Team;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureServices(builder.Configuration);

            var app = builder.Build();
            Configure(app);
            app.Run();
        }

        private static void Configure(WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(TeamViewModel).GetTypeInfo().Assembly);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

        }
    }
}
