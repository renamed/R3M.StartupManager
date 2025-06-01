using R3M.StartUpBoostManager.Model;

namespace R3M.StartUpBoostManager.Services.Interfaces;

public interface IStartupService
{
    IEnumerable<StartupProgram> ListAllStartupPrograms();
}
