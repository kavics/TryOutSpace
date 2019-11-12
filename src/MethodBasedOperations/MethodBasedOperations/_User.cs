using System.Linq;

namespace MethodBasedOperations
{
    public class User //UNDONE: MOCK: Delete this class
    {
        public static User Current { get; set; } = new User(1, new[] {"Administrators"});

        public string[] Roles { get; }
        public int Id { get; }

        public User(int id, string[] roles)
        {
            Id = id;
            Roles = roles;
        }
    }
}
