using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ConfigTestsProject.Models;

namespace ConfigTestsProject.ConfigSettings;

public class XmlConfig : IConfig
{
    private XDocument XDocument;

    public XmlConfig()
    {
        XDocument = XDocument.Load(PathHelper.GetPathToXmlFile());
    }

    public List<Config> GetBrowsers()
    {
        var browserList = (from xml in XDocument.Element("config")?.Elements("browser")
            select new Config()
            {
                Name = xml.Attribute("name")?.Value,
            }).ToList();

        return browserList;
    }

    public List<Config> GetBrowsersWithConfiguration()
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
    
    public List<Config> GetIncorrectConfigurationBrowsers(List<Config> browsers)
    {
        var checkedBrowsers = new List<Config>();

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
    
    public void PrintBrowsersConfiguration(List<Config> browsers)
    {
        foreach (var browser in browsers)
        {
            Console.WriteLine(browser.ToString());
        }
    }
    
    public void PrintNamesIncorrectConfigurationBrowsers(List<Config> browsers)
    {
        foreach (var browser in browsers)
        {
            Console.WriteLine(browser.Name);
        }
    }
}