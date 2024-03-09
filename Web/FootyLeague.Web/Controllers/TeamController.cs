namespace FootyLeague.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FootyLeague.Services.Data.Contracts;
    using FootyLeague.Web.ViewModels.Team;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            this._teamService = teamService;
        }

        [HttpGet("team")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            var teams = await this._teamService.GetAllTeamsAsync<TeamViewModel>();

            return teams.Any() ? this.Ok(teams) : this.NotFound();
        }

        [HttpPost("create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Create(CreateTeamInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this._teamService.CreateTeamAsync(model);

            return this.Ok();
        }

        [HttpPost("update_all_teams_stats")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAllTeamsStats()
        {
            await this._teamService.UpdateAllTeamsStats();

            return this.Ok();
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            var team = await this._teamService.GetTeamAsync<TeamViewModel>(id);

            return team != null ? this.Ok(team) : this.NotFound();
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
           await this._teamService.Delete(id);

           return this.Ok();
        }

        [HttpPost("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Restore(int id)
        {
            await this._teamService.Restore(id);

            return this.Ok();
        }
    }
}
