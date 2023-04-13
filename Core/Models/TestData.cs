using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Models
{
    public class TestData
    {
        private string test;

        public string Test
        {
            get
            {
                return test;
            }
            set
            {
                if (IsTestValidate(value))
                {
                    test = GetValidateTestName(value);
                }
                else
                {
                    throw new ArgumentException("Test name is not correct");
                }
            }
        }

        public TestData()
        {
        }

        private bool IsTestValidate(string value)
        {
            var pattern = @"(?=.*[test]_(?=.*\d{1,4}\.\d{1,3}))";
            
            return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);
        }

        private string GetValidateTestName(string value)
        {
            var target = "{0:d4}";
            var target1 = "{0:d3}";
            
            var resultString = string.Join(".", Regex.Matches(value, @"\d+").OfType<Match>().Select(m => m.Value));

            var numberValue1 = Convert.ToInt16(resultString.Split('.').First());
            var numberValue2 = Convert.ToInt16(resultString.Split('.').Last());
                    
            var valueResult1 = string.Format(target, numberValue1);
            var valueResult2 = string.Format(target1, numberValue2);

            var finalValue = string.Concat(valueResult1, ".", valueResult2);

            return value.Replace(resultString, finalValue);
        }

        public override string ToString()
        {
            if (Test != null)
            {
                return $", tests = {Test}";
            }

            return null;
        }
    }
}