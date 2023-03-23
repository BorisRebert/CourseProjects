using System.Collections.Generic;

namespace ConfigTestsProject.Models
{
    public class Browser
    {
        public string Name { get; set; }
        
        public List<User> Users { get; set; }
        
        public Browser(){}
        
        public override string ToString() => $"Browser: {Name}\n {string.Join("\n", Users)}";
    }   
}