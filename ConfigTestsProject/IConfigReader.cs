using System.Collections.Generic;
using ConfigTestsProject.Models;

namespace ConfigTestsProject;

public interface IConfigReader
{
    List<Config> GetBrowsersWithConfiguration();
}