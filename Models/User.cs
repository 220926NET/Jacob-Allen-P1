namespace Models;

public class User
{
    public int Id { get; set; }
    public bool IsManager { get; set; } = false;
    public string Username { get; set; }
    public string Password { get; set; }

    public User()
    {
        Username = "a";
        Password = "z";
    }

    public User(string? name, string? pass)
    {
        Username = name ?? "error";
        Password = pass ?? "error";
    }

    public override string ToString()
    {
        return String.Format("|{0,-5}|{1,-15}|{2,-10}|", Id, Username, IsManager.ToString());
    }
}
