using System.Collections.Generic;
using Browser = Core.Models.Browser;

namespace ConfigTestsProject.ConfigSettings
{
    public class ConfigHelper
    {
        public List<Browser> GetBrowsersWithIncorrectConfiguration(List<Browser> browsers)
        {
            var checkedBrowsers = new List<Browser>();

            foreach (var browser in browsers)
            {
                foreach (var user in browser.Users)
                {
                    if ((user.Role != "admin" && string.IsNullOrEmpty(user.Login) && string.IsNullOrEmpty(user.Password) && user.Tests.Count < 1) ||
                        (user.Role == "admin" && user.Tests.Count <= 2))
                    {
                        checkedBrowsers.Add(browser);
                    }
                }
            }
            return checkedBrowsers;
        }
    }   
}