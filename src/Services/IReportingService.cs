interface IReportingService
{
    Task<IEnumerable<BaseClient>> GetTopClientAsync(int count);
    Task<Dictionary<string, int>> GetClientTypeAync();
    Task<IEnumerable<IGrouping<string, BaseClient>>> GroupClientsByTypeAsync();
}