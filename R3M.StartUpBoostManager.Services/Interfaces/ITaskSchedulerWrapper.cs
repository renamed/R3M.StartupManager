using Microsoft.Win32.TaskScheduler;
using Task = Microsoft.Win32.TaskScheduler.Task;

namespace R3M.StartUpBoostManager.Services.Interfaces;

public interface ITaskSchedulerWrapper
{
    public IEnumerable<Task> AllTasks { get; }

    bool HasStartupTrigger(Task task);
}
