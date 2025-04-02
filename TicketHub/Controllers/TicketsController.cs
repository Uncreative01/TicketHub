using Microsoft.AspNetCore.Mvc;
using TicketHub.Models;
using TicketHub.Services;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly QueueService _queueService;

    public TicketsController(QueueService queueService)
    {
        _queueService = queueService;
    }

    [HttpPost("purchase")]
    public async Task<IActionResult> PurchaseTicket([FromBody] TicketPurchase request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _queueService.SendMessageToQueueAsync(request);
        return Ok(new { message = "Ticket purchase received and sent to queue." });
    }
}
