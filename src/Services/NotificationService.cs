
public class NotificationService : INotificationService
{
    public async Task SendNotificationAsync(INotifiable client, string message)
    {
        client.SendNotification(message);
        await Task.CompletedTask;
    }

    public async Task BroadcastAsync(IEnumerable<INotifiable> clients, string message)
    {
        var tasks = clients.Select(client => SendNotificationAsync(client, message));
        await Task.WhenAll(tasks);
    }
}