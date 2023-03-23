using ConfigTestsProject.ConfigSettings;

namespace ConfigTestsProject
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            XmlConfig xmlConfig = new XmlConfig();
            JsonConfig jsonConfig = new JsonConfig();
            
            IConfig reader = new XmlConfig();
            var browsers = reader.GetBrowsersWithConfiguration();

            var incorrectBrowsers = xmlConfig.GetIncorrectConfigurationBrowsers(browsers);

            IConfig writer = new JsonConfig();
            writer.PrintBrowsersConfiguration(browsers);
            xmlConfig.PrintNamesIncorrectConfigurationBrowsers(incorrectBrowsers);
            
            jsonConfig.SerializationBrowsersConfiguration(browsers);
        }
    }
}