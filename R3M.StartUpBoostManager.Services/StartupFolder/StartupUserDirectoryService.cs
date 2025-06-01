using R3M.StartUpBoostManager.Model;
using R3M.StartUpBoostManager.Services.Interfaces;

namespace R3M.StartUpBoostManager.Services.StartupFolder;

public class StartupUserDirectoryService : StartupDirectoryService, IStartupUserDirectoryService
{
    public override string GetFolderPath()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.Startup);
    }
    public override StartupProgramSource StartupProgramSource 
        => StartupProgramSource.CurrentUserStartupFolder;
}
