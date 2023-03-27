using System.IO;
using System.Reflection;

namespace ConfigTestsProject
{
    public class PathHelper
    {
        public static string GetLocation() => Assembly.GetExecutingAssembly().Location;

        public static string GetPathToDebugFolder() => Path.GetDirectoryName(GetLocation());
        
        public static string GetPathToBinFolder() => Path.GetDirectoryName(GetPathToDebugFolder());

        public static string GetPathToProjectFolder() => Path.GetDirectoryName(GetPathToBinFolder());

        public static string GetPathToXmlFile() => Path.Combine(GetPathToProjectFolder(), @"Config\config.xml");
    }
}

