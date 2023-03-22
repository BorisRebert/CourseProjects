using System.Collections.Generic;
using ConfigTestsProject.Models;

namespace ConfigTestsProject;

public interface IConfigWriter 
{
    void PrintBrowsersConfiguration(List<Config> browsers);
}