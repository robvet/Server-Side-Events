using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DownstreamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownstreamController : ControllerBase
    {
        [HttpGet("long-running-operation")]
        public async Task<IActionResult> LongRunningOperation(CancellationToken cancellationToken)
        {
            Response.ContentType = "text/event-stream";  // Set the content type for SSE

            await using var writer = new StreamWriter(Response.Body);

            for (int i = 1; i <= 25; i++)
            {
                // Simulate some processing work
                await Task.Delay(500, cancellationToken); // Long running work simulation

                // Send progress update as an SSE event
                await writer.WriteLineAsync($"data: Progress: {i}%\n");
                await writer.FlushAsync();
            }

            // Close the SSE stream
            await writer.WriteLineAsync("data: Completed\n");
            await writer.FlushAsync();

            return new EmptyResult();  // Stream ends after completing
        }
    }
}
