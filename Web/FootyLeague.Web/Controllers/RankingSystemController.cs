namespace FootyLeague.Web.Controllers
{
    using FootyLeague.Services.Data.Contracts;
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
    }
}
