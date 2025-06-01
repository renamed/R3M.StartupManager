namespace R3M.StartUpBoostManager.Model;

public class StartupProgram
{
    public required string Name { get; set; }
    public string? Command { get; set; }
    public required StartupProgramSource Source { get; set; }
}
