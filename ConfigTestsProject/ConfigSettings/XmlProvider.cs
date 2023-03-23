using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ConfigTestsProject.Models;

namespace ConfigTestsProject.ConfigSettings
{
    public class XmlProvider : IRepository
    {
        private XDocument XDocument;

        public XmlProvider()
        {
            XDocument = XDocument.Load(PathHelper.GetPathToXmlFile());
        }

        public Config GetConfig() => new Config() { Browsers = GetBrowsersWithConfiguration() };

        public void WriteConfig(Config config)
        {
            foreach (var item in config.Browsers)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public List<Browser> GetBrowsersName()
        {
            var browserList = (from xml in XDocument.Element("config")?.Elements("browser")
                select new Browser()
                {
                    Name = xml.Attribute("name")?.Value,
                }).ToList();

            return browserList;
        }

        public List<Browser> GetBrowsersWithConfiguration()
        {
            var browsersList = GetBrowsersName();

            foreach (var browser in browsersList)
            {
                var users = GetUsers(browser.Name);
                browser.Users = users;

                foreach (var user in users)
                {
                    user.Tests = GetTests(user.Role, browser.Name);
                }
            }

            return browsersList;
        }

        public List<User> GetUsers(string browser)
        {
            return (from xml in XDocument.Root.Elements("browser").Elements("user")
                where xml.Parent.Attribute("name").Value == browser
                select new User()
                {
                    Role = xml.Attribute("role")?.Value,
                    Login = xml.Element("login")?.Value,
                    Password = xml.Element("password")?.Value
                }).ToList();
        }

        public List<TestData> GetTests(string user, string browser)
        {
            return (from xml in XDocument.Root.Elements("browser").Elements("user")
                    .Where(xml => xml.Parent.Attribute("name").Value == browser && xml.Attribute("role")?.Value == user)
                    .Elements("test")
                select new TestData()
                {
                    Test = xml?.Value
                }).ToList();
        }
    }
}

