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

            return teams.Any() ? this.Ok(teams) : NotFound();
        }

        [HttpPost("create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Post(CreateTeamInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this._teamService.CreateTeamAsync(model);

            return Ok();
        }
    }
}
