using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JsonReflection.JsonModels
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
            var pattern = @"^(?=.*[A-Z])(?=.*\d.*\d)[A-Za-z\d_+]+$";

            return Regex.IsMatch(value, pattern);
        }

        public override string ToString() => $"{Role}: login = {Login}, password = {Password} {string.Join(" ", Tests)}";
    }
}