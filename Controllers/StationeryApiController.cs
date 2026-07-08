using Microsoft.AspNetCore.Mvc;
using StationeryStore.Mvc.Services;

namespace StationeryStore.Mvc.Controllers;

[ApiController]
[Route("api/stationery")]
public class StationeryApiController : ControllerBase
{
    private readonly IStationeryService _service;

    private readonly ILogger<StationeryApiController> _logger;

    public StationeryApiController(
        IStationeryService service,
        ILogger<StationeryApiController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item =
            await _service.GetDetailAsync(id);

        if (item == null)
        {
            _logger.LogWarning(
                "API Stationery not found. Id={Id}",
                id);

            var problem =
                new ProblemDetails
                {
                    Title = "Stationery Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Detail =
                        $"No stationery found with id = {id}",
                    Instance =
                        HttpContext.Request.Path
                };

            problem.Extensions["errorCode"] =
                "STATIONERY_NOT_FOUND";

            problem.Extensions["traceId"] =
                HttpContext.TraceIdentifier;

            return NotFound(problem);
        }

        _logger.LogInformation(
            "API Get Stationery Success. Id={Id}, Code={Code}",
            id,
            item.Code);

        return Ok(item);
    }

}
