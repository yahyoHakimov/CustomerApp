public interface INotifiable
{
    void SendNotification(string message);
    string NotificationMethod { get; }
}