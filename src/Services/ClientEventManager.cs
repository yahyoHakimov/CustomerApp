public delegate void ClientEventHandler(BaseClient client, string action);
public class ClientEventManager
{
    public static event ClientEventHandler ClientChanged; // STATIC!
    public static event Action<string> LogEvent;          // STATIC!

    public static void OnClientChanged(BaseClient client, string action)
    {
        ClientChanged?.Invoke(client, action);
        LogEvent?.Invoke($"{action} - Client{client.Name} at {DateTime.Now}");
    }
}