namespace FootyLeague.Web.Controllers
{
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;
        private readonly HealthCheckService _service;

        public HealthCheckController(ILogger<HealthCheckController> logger, HealthCheckService service)
        {
            this._logger = logger;
            this._service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var report = await this._service.CheckHealthAsync();

            this._logger.LogInformation($"Get Health Information: {report}");

            return report.Status == HealthStatus.Healthy ? this.Ok(report) : this.StatusCode((int)HttpStatusCode.ServiceUnavailable, report);
        }

    }
}
