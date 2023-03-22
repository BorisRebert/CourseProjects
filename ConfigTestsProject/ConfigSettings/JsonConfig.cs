using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ConfigTestsProject.Models;

namespace ConfigTestsProject
{
    public class JsonConfig : IConfig
    {
        public List<Config> GetBrowsersWithConfiguration()
        {
            List<Config> browsers = new List<Config>();
            
            using (FileStream fs = new FileStream(PathHelper.GetPathToXmlFile(), FileMode.OpenOrCreate))
            {
                browsers = JsonSerializer.DeserializeAsync<List<Config>>(fs).Result as List<Config>;
            }

            return browsers;
        }
        
        public void SerializationBrowsersConfiguration(List<Config> browsers)
        {
            foreach (var browser in browsers)
            {
                using (FileStream fs = new FileStream(@$"{PathHelper.GetPathToProjectFolder()}\Config\{browser.Name}.json", FileMode.OpenOrCreate))
                {
                    JsonSerializer.SerializeAsync<Config>(fs, browser);
                }
            }
        }
        
        public void PrintBrowsersConfiguration(List<Config> browsers)
        {
            foreach (var browser in browsers)
            {
                Console.WriteLine(browser.ToString());
            }
        }
    }   
}