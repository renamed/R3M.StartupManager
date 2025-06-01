using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Xunit;
using R3M.StartUpBoostManager.Model;
using R3M.StartUpBoostManager.Services.Interfaces;
using R3M.StartUpBoostManager.Services.StartupTask;
using Microsoft.Win32.TaskScheduler;
using Task = Microsoft.Win32.TaskScheduler.Task;
using System.Reflection;

namespace R3M.StartUpBoostManager.Services.Tests.StartupTask;

public class StartupTaskScheduleServiceTests
{
    [Fact]
    public void ListAllStartupPrograms_ShouldReturnEmpty_WhenNoTasksExist()
    {
        // Arrange
        var taskSchedulerWrapper = A.Fake<ITaskSchedulerWrapper>();
        
        A.CallTo(() => taskSchedulerWrapper.AllTasks).Returns([]);
        var service = new StartupTaskScheduleService(taskSchedulerWrapper);

        // Act
        var result = service.ListAllStartupPrograms();

        // Assert
        Assert.Empty(result);
    }

}
