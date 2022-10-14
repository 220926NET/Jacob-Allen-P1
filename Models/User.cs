namespace Models;

public class User
{
    public int Id { get; set; }
    public bool IsManager { get; set; } = false;

    public string Username
    {
        get => _username;
        set => _username = value;
    }
    private string _username = null!;

    public string Password
    {
        get => _password;
        set => _password = value;
    }
    private string _password = null!;

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
        return $"UserId: {Id} | Username: {Username} | Password: {Password} | isManager: {IsManager.ToString()}";
    }
}
