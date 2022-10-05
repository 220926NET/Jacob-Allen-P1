namespace Models
{
    internal class User
    {
        private bool isManager = false;

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
    }

    internal class UserList
    {
        private Dictionary<string, User> employees = new Dictionary<string, User>();

        public UserList()
        {
            employees.Add("admin", new User("admin", "pass"));
            employees.Add("jallen", new User("jallen", "1234"));
            employees.Add("test", new User("test", "test"));
        }
    }

}