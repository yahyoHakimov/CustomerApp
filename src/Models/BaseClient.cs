public abstract class BaseClient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; }

    // Abstract method - har bir mijoz turi o'zini implement qiladi
    public abstract decimal DiscountCalculate(decimal amount);

    // Virtual method - override qilish mumkin
    public virtual string GetDisplayName()
    {
        return $"Mijoz raqami {Id} va uning ismi {Name}";
    }

    // Interface implementation uchun
    public abstract string GetClientType();
}