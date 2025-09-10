public class IndividualClient : BaseClient, INotifiable, IClientOperations
{
    public string PassportNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string NotificationMethod => "SMS";

    public override decimal DiscountCalculate(decimal amount)
    {
        return amount * 0.05m; // 5% chegirma
    }

    public override string GetClientType()
    {
        return "Individual";
    }

    public void SendNotification(string message)
    {
        Console.WriteLine($"SMS yuborildi {Phone} ga: {message}");
    }

    public bool ValidateClient()
    {
        return !string.IsNullOrEmpty(PassportNumber) && !string.IsNullOrEmpty(Name);
    }

    public string GetClientDetails()
    {
        return $"Individual: {Name}, Passport: {PassportNumber}";
    }
}