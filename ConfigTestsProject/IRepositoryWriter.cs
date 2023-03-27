using ConfigTestsProject.Models;

namespace ConfigTestsProject;

public interface IRepositoryWriter
{
    void WriteConfig(Config config);
}