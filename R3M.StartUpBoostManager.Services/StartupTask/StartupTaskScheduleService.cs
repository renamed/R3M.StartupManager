using Microsoft.Win32.TaskScheduler;
using R3M.StartUpBoostManager.Model;
using R3M.StartUpBoostManager.Services.Interfaces;

namespace R3M.StartUpBoostManager.Services.StartupTask;

public class StartupTaskScheduleService : IStartupTaskScheduleService
{
    public IEnumerable<StartupProgram> ListAllStartupPrograms()
    {
        using var ts = new TaskService();

        List<StartupProgram> startupPrograms = [];
        foreach (var task in ts.AllTasks)
        {
            if (task.Definition.Triggers.Any(t =>
                t.TriggerType == TaskTriggerType.Logon ||
                t.TriggerType == TaskTriggerType.Boot))
            {
                startupPrograms.Add(new StartupProgram
                {
                    Name = task.Name,
                    Command = task.Definition.Actions.FirstOrDefault()?.ToString(),
                    Source = StartupProgramSource.TaskScheduler
                });
            }
        }
        return startupPrograms;
    }
}
