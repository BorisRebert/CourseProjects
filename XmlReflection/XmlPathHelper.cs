using System.IO;
using System.Reflection;

namespace XmlReflection
{
    public class XmlPathHelper
    {
        public static string GetLocation() => Assembly.GetExecutingAssembly().Location;

        public static string GetPathToDebugFolder() => Path.GetDirectoryName(GetLocation());
        
        public static string GetPathToBinFolder() => Path.GetDirectoryName(GetPathToDebugFolder());
        
        public static string GetPathToXmlReflectionFolder() => Path.GetDirectoryName(GetPathToBinFolder());
        
        public static string GetPathToXmlFile() => Path.Combine(GetPathToXmlReflectionFolder(), @"config.xml");
    }
}

