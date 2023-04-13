using System.Configuration;
using System.Reflection;

namespace ConfigTestsProject
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var xmlKey = ConfigurationManager.AppSettings.Get("XmlReflection");
            var jsonKey = ConfigurationManager.AppSettings.Get("JsonReflection");
            
            var assembly1 = Assembly.LoadFrom(@"C:\Projects\CourseProjects\XmlReflection\bin\Debug\XmlReflection.dll");
            var assembly2 = Assembly.LoadFrom(@"C:\Projects\CourseProjects\JsonReflection\bin\Debug\JsonReflection.dll");

            dynamic xmlProvider = assembly1.CreateInstance(xmlKey);
            dynamic jsonProvider = assembly2.CreateInstance(jsonKey);

            var config = xmlProvider.GetConfig();
            xmlProvider.WriteConfig(config);
            
            jsonProvider.SetBrowsersConfigurationInJson(config.Browsers);
            var configFromJson = jsonProvider.GetConfig();
            jsonProvider.WriteConfig(configFromJson);
        }
    }
}