public class VIPClient : BaseClient, INotifiable, IClientOperations
{
    public string VIPLevel { get; set; }
    public decimal TotalSpent { get; set; }
    public string NotificationMethod => "Personal Manager";

    public override decimal DiscountCalculate(decimal amount)
    {
        return VIPLevel switch
        {
            "Gold" => amount * 0.20m,
            "Platinum" => amount * 0.30m,
            "Diamond" => amount * 0.40m,
            _ => amount * 0.10m
        };
    }

    public override string GetClientType()
    {
        return "VIP";
    }

    public override string GetDisplayName()
    {
        return $"VIP Mijoz: {Name} ({VIPLevel} darajasi)";
    }

    public void SendNotification(string message)
    {
        Console.WriteLine($"Shaxsiy menejer orqali xabar: {Name} uchun - {message}");
    }

    public bool ValidateClient()
    {
        return !string.IsNullOrEmpty(VIPLevel) && TotalSpent > 0;
    }

    public string GetClientDetails()
    {
        return $"VIP: {Name}, Level: {VIPLevel}, Spent: {TotalSpent:C}";
    }
}