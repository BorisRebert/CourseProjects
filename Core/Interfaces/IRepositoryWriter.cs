using Core.Models;

namespace Core.Interfaces
{
    public interface IRepositoryWriter
    {
        void WriteConfig(Config config);
    }
}