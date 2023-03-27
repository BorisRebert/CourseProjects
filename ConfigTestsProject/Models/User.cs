using System.Collections.Generic;

namespace ConfigTestsProject.Models
{
    public class User
    {
        public string Role { get; set; }
        
        public string Login { get; set; }

        private string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (value == null)
                {
                    password = "password";
                }
                else
                {
                    password = value;
                }
            }
        }

        public List<TestData> Tests { get; set; }
        
        public User(){}

        public override string ToString() => $"{Role}: login = {Login}, password = {Password} {string.Join(", ", Tests)}";
    }
}