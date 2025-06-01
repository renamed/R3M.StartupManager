using R3M.StartUpBoostManager.Model;
using R3M.StartUpBoostManager.Services.Interfaces;

namespace R3M.StartUpBoostManager.Services.StartupFolder;

public class StartupCommonDirectoryService : StartupDirectoryService, IStartupCommonDirectoryService
{
    public override StartupProgramSource StartupProgramSource
        => StartupProgramSource.AllUsersStartupFolder;

    public override string GetFolderPath()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
    }
}
