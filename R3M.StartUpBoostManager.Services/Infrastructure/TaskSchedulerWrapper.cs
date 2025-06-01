using Microsoft.Win32.TaskScheduler;
using R3M.StartUpBoostManager.Services.Interfaces;

namespace R3M.StartUpBoostManager.Services.Infrastructure;

public class TaskSchedulerWrapper : ITaskSchedulerWrapper, IDisposable
{
    private readonly TaskService _taskService;

    public TaskSchedulerWrapper()
    {
        _taskService = new TaskService();
    }

    public IEnumerable<Microsoft.Win32.TaskScheduler.Task> AllTasks 
        => _taskService.AllTasks;

    public bool HasStartupTrigger(Microsoft.Win32.TaskScheduler.Task task)
    {
        return task.Definition.Triggers.Any(t =>
                t.TriggerType == TaskTriggerType.Logon ||
                t.TriggerType == TaskTriggerType.Boot);
    }

    public void Dispose()
    {
        _taskService.Dispose();
    }
}
