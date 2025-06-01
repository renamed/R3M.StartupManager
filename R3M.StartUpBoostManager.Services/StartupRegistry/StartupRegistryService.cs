using Microsoft.Win32;
using R3M.StartUpBoostManager.Model;
using R3M.StartUpBoostManager.Services.Interfaces;

namespace R3M.StartUpBoostManager.Services.StartupRegistry;

public abstract class StartupRegistryService : IStartupRegistryService
{
    public IEnumerable<StartupProgram> ListAllStartupPrograms()
    {
        using var key = GetRegistryKey();
        if (key is null)
        {
            yield break;
        }

        foreach (var name in key.GetValueNames())
        {
            var command = key.GetValue(name)?.ToString();
            yield return new StartupProgram
            {
                Name = name,
                Command = command,
                Source = StartupProgramSource
            };
        }
    }

    public abstract RegistryKey? GetRegistryKey();
    public abstract StartupProgramSource StartupProgramSource { get; }
}
