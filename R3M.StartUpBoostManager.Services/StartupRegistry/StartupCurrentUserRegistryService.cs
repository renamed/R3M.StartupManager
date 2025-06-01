using Microsoft.Win32;
using R3M.StartUpBoostManager.Model;
using R3M.StartUpBoostManager.Services.Interfaces;

namespace R3M.StartUpBoostManager.Services.StartupRegistry;

public class StartupCurrentUserRegistryService : 
    StartupRegistryService, IStartupCurrentUserRegistryService
{
    public const string CURRENT_USER_REGISTRY = @"Software\Microsoft\Windows\CurrentVersion\Run";

    public override RegistryKey? GetRegistryKey()
    {
        return Registry.CurrentUser.OpenSubKey(CURRENT_USER_REGISTRY, false);
    }

    public override StartupProgramSource StartupProgramSource 
        => StartupProgramSource.CurrentUserRegistry;
}
