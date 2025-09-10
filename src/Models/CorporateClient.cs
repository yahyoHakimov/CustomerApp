public class CorporateClient : BaseClient, INotifiable, IClientOperations
{
    public string CompanyName { get; set; }
    public string TaxNumber { get; set; }
    public string NotificationMethod => "Email";

    public override decimal DiscountCalculate(decimal amount)
    {
        return amount * 0.15m; // 15% chegirma
    }

    public override string GetClientType()
    {
        return "Corporate";
    }

    public void SendNotification(string message)
    {
        Console.WriteLine($"Email yuborildi {Email} ga: {message}");
    }

    public bool ValidateClient()
    {
        return !string.IsNullOrEmpty(TaxNumber) && !string.IsNullOrEmpty(CompanyName);
    }

    public string GetClientDetails()
    {
        return $"Corporate: {CompanyName}, Tax: {TaxNumber}";
    }
}