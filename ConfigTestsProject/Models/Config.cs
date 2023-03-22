using System.Collections.Generic;

namespace ConfigTestsProject.Models
{
    public class Config
    {
        public string Name { get; set; }

        public List<User> Users { get; set; }
        
        public Config(){}

        public override string ToString() => $"Browser: {Name}\n {string.Join("\n", Users)}";
    }
}