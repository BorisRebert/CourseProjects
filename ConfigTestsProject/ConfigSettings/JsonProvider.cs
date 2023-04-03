using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ConfigTestsProject.ConfigSettings;
using ConfigTestsProject.Models;

namespace ConfigTestsProject
{
    public class JsonProvider : IRepository
    {
        public Config GetConfig() => new Config() { Browsers = GetBrowsersWithConfiguration() };
        
        public void WriteConfig(Config config)
        {
            foreach (var item in config.Browsers)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public List<Browser> GetBrowsersWithConfiguration()
        {
            var browsers = new XmlProvider().GetBrowsersName();
            var listBrowsers = new List<Browser>();

            foreach (var browser in browsers)
            {
                using (FileStream fs = new FileStream(@$"{PathHelper.GetPathToProjectFolder()}\Config\{browser.Name}.json", FileMode.OpenOrCreate))
                {
                    var br = JsonSerializer.DeserializeAsync<Browser>(fs).Result;
                    listBrowsers.Add(br);
                }
            }
            return listBrowsers;
        }
        
        public void SetBrowsersConfigurationInJson(List<Browser> browsers)
        {
            foreach (var browser in browsers)
            {
                using (FileStream fs = new FileStream(@$"{PathHelper.GetPathToProjectFolder()}\Config\{browser.Name}.json", FileMode.Create))
                {
                    JsonSerializer.Serialize<Browser>(fs, browser);
                }
            }
        }
    }   
}