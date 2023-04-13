using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Interfaces;
using JsonReflection.JsonModels;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace JsonReflection
{
    public class JsonProvider : IRepository
    {
        public Core.Models.Config GetConfig() => GetConfigFromJsonToConfig(GetJsonConfig());

        public void WriteConfig(Core.Models.Config config)
        {
            foreach (var item in config.Browsers)
            {
                Console.WriteLine(item.ToString());
            }
        }
        
        public Config GetJsonConfig() => new Config() { Browsers = GetBrowsersWithConfiguration() };
        
        public List<Browser> GetBrowsersWithConfiguration()
        {
            var listBrowsers = new List<Browser>();
            var listFiles = Directory.GetFiles(JsonPathHelper.GetPathToConfigFolder());
            
            foreach (var browser in listFiles)
            {
                using (FileStream fs = new FileStream(@$"{JsonPathHelper.GetPathToProjectFolder()}\ConfigTestsProject\Config\{Path.GetFileName(browser)}", FileMode.OpenOrCreate))
                {
                    var br = JsonSerializer.DeserializeAsync<Browser>(fs).Result;
                    listBrowsers.Add(br);
                }
            }
            
            return listBrowsers;
        }
        
        public void SetBrowsersConfigurationInJson(List<Core.Models.Browser> browsers)
        {
            foreach (var browser in browsers)
            {
                using (FileStream fs = new FileStream(@$"{JsonPathHelper.GetPathToProjectFolder()}\ConfigTestsProject\Config\{browser.Name}.json", FileMode.Create))
                {
                    JsonSerializer.Serialize<Core.Models.Browser>(fs, browser);
                }
            }
        }

        private Core.Models.Config GetConfigFromJsonToConfig(Config jsonConfig)
        {
            var config = new Core.Models.Config();

            config.Browsers = (from json in jsonConfig.Browsers
                select new Core.Models.Browser()
                {
                    Name = json.Name,
                    Users = json.Users.Select(x => new Core.Models.User()
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