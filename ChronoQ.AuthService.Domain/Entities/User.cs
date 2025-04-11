namespace ChronoQ.AuthService.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string PhoneNumber { get; set; } = null!;
    public string Role { get; set; } = "User";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}