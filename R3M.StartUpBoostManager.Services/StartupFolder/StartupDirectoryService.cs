using R3M.StartUpBoostManager.Model;
using R3M.StartUpBoostManager.Services.Interfaces;

namespace R3M.StartUpBoostManager.Services.StartupFolder;

public abstract class StartupDirectoryService : IStartupDirectoryService
{
    public IEnumerable<StartupProgram> ListAllStartupPrograms()
    {        
        var path = GetFolderPath();
        if (!Directory.Exists(path))
        {
            yield break;
        }

        foreach (var file in Directory.GetFiles(path, "*.lnk"))
        {
            yield return new StartupProgram
            {
                Name = Path.GetFileNameWithoutExtension(file),
                Command = file,
                Source = StartupProgramSource
            };
        }
    }

    public abstract string GetFolderPath();
    public abstract StartupProgramSource StartupProgramSource { get; }
}
