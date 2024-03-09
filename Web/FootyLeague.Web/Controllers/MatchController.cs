using FootyLeague.Services.Data.Contracts;
using FootyLeague.Web.ViewModels.Match;
using FootyLeague.Web.ViewModels.Team;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FootyLeague.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly ITeamService _teamService;

        public MatchController(IMatchService matchService,ITeamService teamService)
        {
            this._matchService = matchService;
            this._teamService = teamService;
        }

        [HttpGet("match")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            var matches = await this._matchService.GetAllMatchesAsync<MatchViewModel>();

            return matches.Any() ? this.Ok(matches) : NotFound();
        }

        [HttpPost("create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Create(CreateMatchInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this._matchService.CreateMatchAsync(model);
            await this._teamService.UpdateAllTeamsStats();

            return Ok();
        }

    }
}
