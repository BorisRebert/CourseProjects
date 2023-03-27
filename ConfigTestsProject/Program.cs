using ConfigTestsProject.ConfigSettings;
using ConfigTestsProject.Models;

namespace ConfigTestsProject
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IRepository reader = new XmlProvider();
            Config config = reader.GetConfig();

            IRepository writer = new JsonProvider();
            writer.WriteConfig(config);
            
            ConfigHelper configHelper = new ConfigHelper();
            JsonProvider jsonProvider = new JsonProvider();
            
            var incorrectBrowsers = configHelper.GetBrowsersWithIncorrectConfiguration(config.Browsers);
            
            jsonProvider.SetBrowsersConfigurationInJson(config.Browsers);
            jsonProvider.GetBrowsersWithConfiguration();
        }
    }
}