using ConfigTestsProject.Models;

namespace ConfigTestsProject;

public interface IRepositoryReader
{
    public Config GetConfig();
}