
public class ReportingService : IReportingService
{
    private readonly IClientService<BaseClient> _clientService;

    public ReportingService(IClientService<BaseClient> client)
    {
        _clientService = client;
    }
    public async Task<Dictionary<string, int>> GetClientTypeAync()
    {
        var clients = await _clientService.GetAllClientAsync();
        return clients
            .GroupBy(c => c.GetClientType())
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public async Task<IEnumerable<BaseClient>> GetTopClientAsync(int count)
    {
        var clients = await _clientService.GetAllClientAsync();
        return clients
            .Where(c => c is VIPClient)
            .Cast<VIPClient>()
            .OrderByDescending(c => c.TotalSpent)
            .Take(count)
            .Cast<BaseClient>();
    }

    public async Task<IEnumerable<IGrouping<string, BaseClient>>> GroupClientsByTypeAsync()
    {
        var clients = await _clientService.GetAllClientAsync();
        return clients.GroupBy(c => c.GetClientType());
    }
}