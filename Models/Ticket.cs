namespace Models;

public class Ticket
{
    enum Status { Pending, Approved, Denied };

    public int Id { get; set; }
    public int UserId { get; set; }
    public string CurrentStatus { get; set; } = Status.Pending.ToString();
    string Description { get; set; } = "";
    decimal Amount { get; set; }
    DateOnly DateSubmitted { get; set; }
}