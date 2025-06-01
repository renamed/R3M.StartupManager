using Microsoft.Win32;
using R3M.StartUpBoostManager.Model;
using R3M.StartUpBoostManager.Services.Interfaces;

namespace R3M.StartUpBoostManager.Services.StartupRegistry;

public class StartupAllUserRegistryService : 
    StartupRegistryService, IStartupAllUserRegistryService
{
    public const string ALL_USER_REGISTRY = @"Software\Microsoft\Windows\CurrentVersion\Run";
    public override RegistryKey? GetRegistryKey()
    {
        return Registry.LocalMachine.OpenSubKey(ALL_USER_REGISTRY, false);
    }

    public override StartupProgramSource StartupProgramSource 
        => StartupProgramSource.AllUsersRegistry;
}
