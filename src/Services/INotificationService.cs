public interface INotificationService
{
    Task SendNotificationAsync(INotifiable client, string message);
    Task BroadcastAsync(IEnumerable<INotifiable> clients, string message);
}