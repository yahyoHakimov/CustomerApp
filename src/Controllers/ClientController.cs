using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService<BaseClient> _clientService;
    private readonly INotificationService _notificationService;
    private readonly IReportingService _reportingService;

    public ClientsController(
        IClientService<BaseClient> clientService,
        INotificationService notificationService,
        IReportingService reportingService)
    {
        _clientService = clientService;
        _notificationService = notificationService;
        _reportingService = reportingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BaseClient>>> GetAllClients()
    {
        var clients = await _clientService.GetAllClientAsync();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseClient>> GetClient(int id)
    {
        var client = await _clientService.GetClientAsync(id);
        if (client == null)
            return NotFound();
        
        return Ok(client);
    }

    [HttpPost]
    public async Task<ActionResult> CreateClient([FromBody] BaseClient client)
    {
        if (!TryValidateClient(client, out string error))
            return BadRequest(error);

        await _clientService.CreateClientAsync(client);
        return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateClient(int id, [FromBody] BaseClient client)
    {
        if (id != client.Id)
            return BadRequest();

        await _clientService.UpdateClientAsync(client);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteClient(int id)
    {
        await _clientService.DeleteClientAsync(id);
        return NoContent();
    }

    [HttpGet("search/{term}")]
    public async Task<ActionResult<IEnumerable<BaseClient>>> SearchClients(string term)
    {
        var clients = await _clientService.SearchClientAsync(term);
        return Ok(clients);
    }

    [HttpPost("{id}/discount")]
    public async Task<ActionResult<decimal>> CalculateDiscount(int id, [FromBody] decimal amount)
    {
        var discount = await _clientService.CalculateDiscountAsync(id, amount);
        return Ok(discount);
    }

    [HttpGet("reports/top/{count}")]
    public async Task<ActionResult> GetTopClients(int count)
    {
        var clients = await _reportingService.GetTopClientAsync(count);
        return Ok(clients);
    }

    [HttpGet("reports/stats")]
    public async Task<ActionResult> GetStats()
    {
        var stats = await _reportingService.GetClientTypeAync();
        return Ok(stats);
    }

    // REF/OUT parameters misoli
    private bool TryValidateClient(BaseClient client, out string error)
    {
        error = string.Empty;
        
        if (string.IsNullOrEmpty(client.Name))
        {
            error = "Name is required";
            return false;
        }
        
        if (client is IClientOperations validatable && !validatable.ValidateClient())
        {
            error = "Client validation failed";
            return false;
        }
        
        return true;
    }

    // REF parameter misoli
    private void UpdateClientProperties(ref BaseClient client, BaseClient newData)
    {
        client.Name = newData.Name;
        client.Email = newData.Email;
        client.Phone = newData.Phone;
    }
}