using System.ComponentModel.DataAnnotations;

namespace Models;

public class Ticket
{
    enum Status { Pending, Approved, Denied };

    public int Id { get; set; }
    public int UserId { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public decimal? Amount { get; set; }
    public DateTime DateSubmitted { get; set; }
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

    public void PrintHeader()
    {
        Console.WriteLine(String.Format("|{0,-5}|{1,-10}|{2,-20}|{3,-20}|{4,-25}|{5,-15}|", "Id", "UserId", "Description", "Amount", "DateSubmitted", "CurrentStatus"));
        Console.WriteLine(new String('-',92));
    }

    public override string ToString()
    {
        return String.Format("|{0,-5}|{1,-10}|{2,-20}|${3,-19:N2}|{4,-25}|{5,-15}|", Id, UserId, Description, Amount, DateSubmitted, CurrentStatus);
    }
}