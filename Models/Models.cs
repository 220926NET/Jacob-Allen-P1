namespace Models
{
    public class User
    {
        public bool isManager = false;

        public string Username {
            get => _username;
            set => _username = value;
        }
        private string _username = null!;

        public string Password {
            get => _password;
            set => _password = value;
        }
        private string _password = null!;

        public User()
        {
            Username = "a";
            Password = "z";
        }
        
        public User(string name, string pass)
        {
            Username = name;
            Password = pass;
        }

        public override string ToString()
        {
            return $"Username: {Username} | Password: {Password} | isManager: {isManager.ToString()}";
        }
    }
}