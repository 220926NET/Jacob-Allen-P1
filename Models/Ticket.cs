namespace Models;

public class Ticket
{
    enum Status { Pending, Approved, Denied };

    public int Id { get; set; }
    public int UserId { get; set; }
    public string Description { get; set; } = "";
    public decimal Amount { get; set; }
    public DateOnly DateSubmitted { get; set; }
    public string CurrentStatus { get; set; } = Status.Pending.ToString();


    public Ticket()
    {

    }
    public Ticket (int userid, string description, decimal amount)
    {
        UserId = userid;
        Description = description;
        Amount = amount;
    }

    public override string ToString()
    {
        return $"{Id} {UserId} {Description} {Amount} {DateSubmitted} {CurrentStatus}";
    }
}