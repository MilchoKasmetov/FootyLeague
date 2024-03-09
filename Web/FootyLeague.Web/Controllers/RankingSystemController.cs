namespace FootyLeague.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FootyLeague.Services.Data.Contracts;
    using FootyLeague.Web.ViewModels.Team;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class RankingSystemController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public RankingSystemController(ITeamService teamService)
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

            if (!teams.Any())
            {
                return NotFound();
            }

            return Ok(teams);
        }
    }
}
