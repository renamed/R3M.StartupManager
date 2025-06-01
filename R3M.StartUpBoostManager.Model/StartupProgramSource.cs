namespace R3M.StartUpBoostManager.Model;

public enum StartupProgramSource
{
    AllUsersStartupFolder,
    CurrentUserStartupFolder,
    AllUsersRegistry,    
    CurrentUserRegistry,
    WindowsService,
    TaskScheduler
}
