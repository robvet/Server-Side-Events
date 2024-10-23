using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UpstreamApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UpstreamController : ControllerBase
    {
    private readonly ILogger<UpstreamController> _logger;

        public UpstreamController(ILogger<UpstreamController> logger)
        {
            _logger = logger;
        }

        // GET: api/<UpstreamController>
        // 

        [HttpGet("start-operation")]
        public async Task<IActionResult> StartOperation(CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();

            _logger.LogInformation("Downstream API Called");

            // Async call to downstream API
            var streamResponse = await httpClient.GetAsync("https://localhost:7258/api/downstream/long-running-operation",
                                    HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            var stream = await streamResponse.Content.ReadAsStreamAsync();

            using var reader = new StreamReader(stream);

            // Iterate through stream 
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (!string.IsNullOrEmpty(line))
                {
                   // Handle progress updates here
                   // For demo, just logging it
                   // Handle progress updates here
                    _logger.LogInformation(line);
                }
            }

            _logger.LogInformation("Downstream API Complete");

            return Ok("Operation started, check logs for progress");
        }
    }
}
