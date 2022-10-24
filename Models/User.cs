using System.ComponentModel.DataAnnotations;
namespace Models;

public class User
{
    public int Id { get; set; }
    public bool IsManager { get; set; } = false;
    [Required]
    [StringLength(12)]
    public string? Username { get; set; }
    [Required]
    [StringLength(12)]
    public string? Password { get; set; }

    public User()
    {
    }

    public User(string? name, string? pass)
    {
        Username = name ?? "error";
        Password = pass ?? "error";
    }

    public void PrintHeader()
    {
        Console.WriteLine(String.Format("|{0,-5}|{1,-15}|{2,-10}|","ID","Username","IsManager"));
        Console.WriteLine(new String('-', 34));
    }

    public override string ToString()
    {
        return String.Format("|{0,-5}|{1,-15}|{2,-10}|", Id, Username, IsManager.ToString());
    }
}
