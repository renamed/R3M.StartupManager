using Microsoft.Win32;
using R3M.StartUpBoostManager.Model;
using R3M.StartUpBoostManager.Services.Interfaces;
using System.ServiceProcess;

namespace R3M.StartUpBoostManager.Services.StartupWindowsServices;

public class StartupWindowsServicesService : IStartupWindowsServicesService
{
    public IEnumerable<StartupProgram> ListAllStartupPrograms()
    {
        var services = ServiceController.GetServices();

        foreach (var service in services)
        {            
            using var key = Registry.LocalMachine.OpenSubKey(
                $@"SYSTEM\CurrentControlSet\Services\{service.ServiceName}", false);

            var startValue = key?.GetValue("Start");            
            if (startValue is int startType && startType == 2) // 2 == Automatic
            {
                yield return new StartupProgram
                {
                    Name = service.DisplayName,
                    Command = service.ServiceName,
                    Source = StartupProgramSource.WindowsService
                };
            }

        }
    }
}
