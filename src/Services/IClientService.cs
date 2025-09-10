public interface IClientService<T> where T : BaseClient
{
    Task<IEnumerable<T>> GetAllClientAsync();
    Task<T> GetClientAsync(int id);
    Task CreateClientAsync(T client);
    Task UpdateClientAsync(T client);
    Task DeleteClientAsync(int id);
    Task<IEnumerable<T>> SearchClientAsync(string searchItem);
    Task<decimal> CalculateDiscountAsync(int clientId, decimal amount);
}