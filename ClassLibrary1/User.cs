using System.Collections.Generic;

namespace SocialNetwork.Core
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<int> Friends { get; set; } = new();

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
