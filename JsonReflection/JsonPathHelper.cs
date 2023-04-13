using System.IO;
using System.Reflection;

namespace JsonReflection
{
    public class JsonPathHelper
    {
        public static string GetLocation() => Assembly.GetExecutingAssembly().Location;

        public static string GetPathToDebugFolder() => Path.GetDirectoryName(GetLocation());
        
        public static string GetPathToBinFolder() => Path.GetDirectoryName(GetPathToDebugFolder());

        public static string GetPathToJsonReflectionFolder() => Path.GetDirectoryName(GetPathToBinFolder());
        
        public static string GetPathToProjectFolder() => Path.GetDirectoryName(GetPathToJsonReflectionFolder());
        
        public static string GetPathToConfigFolder() => Path.Combine(GetPathToProjectFolder(), @"ConfigTestsProject\Config");
    }
}

