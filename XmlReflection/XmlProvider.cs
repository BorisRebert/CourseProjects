using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Core.Interfaces;
using XmlReflection.XmlModels;

namespace XmlReflection
{
    public class XmlProvider : IRepository
    {
        private XDocument XDocument;

        public XmlProvider()
        {
            XDocument = XDocument.Load(XmlPathHelper.GetPathToXmlFile());
        }
        
        public Core.Models.Config GetConfig() => GetConfigFromXmlToConfig(GetXmlConfig());

        public void WriteConfig(Core.Models.Config config)
        {
            foreach (var item in config.Browsers)
            {
                Console.WriteLine(item.ToString());
            }
        }
        
        public Config GetXmlConfig() => new Config() { Browsers = GetBrowsersWithConfiguration() };

        public List<Browser> GetBrowsers()
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
            var browsersList = GetBrowsers();
            
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
        
        private Core.Models.Config GetConfigFromXmlToConfig(Config xmlConfig)
        {
            var config = new Core.Models.Config();

            config.Browsers = (from xml in xmlConfig.Browsers
                select new Core.Models.Browser()
                {
                    Name = xml.Name,
                    Users = xml.Users.Select(x => new Core.Models.User()
                    {
                        Role = x.Role,
                        Password = x.Password,
                        Login = x.Login,
                        Tests = x.Tests.Select(t => new Core.Models.TestData()
                        {
                            Test = t.Test
                        }).ToList()
                    }).ToList()
                }).ToList();
            
            return config;
        }
    }
}

