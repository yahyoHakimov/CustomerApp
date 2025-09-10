
using Microsoft.VisualBasic;

public class ClientService<T> : IClientSercive<T> where T : BaseClient
{
    private readonly IRepository<T> _repository;

    public ClientService(IRepository<T> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    public async Task<decimal> CalculateDiscountAsync(int clientId, decimal amount)
    {
        var client = await _repository.GetByIdAsync(clientId);
        return  client?.DiscountCalculate(amount) ?? 0;
    }

    public async Task CreateClientAsync(T client)
    {
        await _repository.AddAsync(client);
        ClientEventManager.OnClientChanged(client, "CREATED");
    }


    public async Task UpdateClientAsync(T client)
    {
        var updating = await _repository.GetByIdAsync(client.Id);

        if (updating != null)
        {
            await _repository.UpdateAsync(updating);
            ClientEventManager.OnClientChanged(updating, "UPDATED");
        }
    }

    public async Task DeleteClientAsync(int id)
    {
        var deleting = await _repository.GetByIdAsync(id);
        if (deleting != null)
        {
            _repository.DeleteAsync(id);
            ClientEventManager.OnClientChanged(deleting, "DELETED");
        }

    }

    public async Task<IEnumerable<T>> GetAllClientAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<T> GetClientAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<T>> SearchClientAsync(string searchItem)
    {
        var clients = await _repository.GetAllAsync();

        return clients.Where(c =>
        c.Name.Contains(searchItem, StringComparison.OrdinalIgnoreCase) ||
        c.Name.Contains(searchItem, StringComparison.OrdinalIgnoreCase)).OrderBy(c => c.Name);
    }
}