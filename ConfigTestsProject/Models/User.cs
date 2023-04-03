using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
                if (IsPasswordValidate(value))
                {
                    password = value;
                }
                else
                {
                    throw new ArgumentException("Password is not correct");
                }
            }
        }

        public List<TestData> Tests { get; set; }

        public User(){}

        private bool IsPasswordValidate(string value)
        {
            var pattern = @"(?=.*[0-9]{2,})(?=.*[+]{0,})(?=.*[_]{0,})(?=.*[a-z]{0,})(?=.*[A-Z]{1,})";

            return Regex.IsMatch(value, pattern);
        }

        public override string ToString() => $"{Role}: login = {Login}, password = {Password} {string.Join(" ", Tests)}";
    }
}