using Microsoft.Win32.TaskScheduler;
using R3M.StartUpBoostManager.Model;
using R3M.StartUpBoostManager.Services.Interfaces;

namespace R3M.StartUpBoostManager.Services.StartupTask;

public class StartupTaskScheduleService : IStartupTaskScheduleService
{
    private readonly ITaskSchedulerWrapper _taskSchedulerWrapper;

    public StartupTaskScheduleService(ITaskSchedulerWrapper taskSchedulerWrapper)
    {
        _taskSchedulerWrapper = taskSchedulerWrapper;        
    }

    public IEnumerable<StartupProgram> ListAllStartupPrograms()
    {
        List<StartupProgram> startupPrograms = [];
        foreach (var task in _taskSchedulerWrapper.AllTasks)
        {
            if (_taskSchedulerWrapper.HasStartupTrigger(task))            
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
