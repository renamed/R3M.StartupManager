using Microsoft.Win32;
using R3M.StartUpBoostManager.Model;
using R3M.StartUpBoostManager.Services.StartupRegistry;

namespace R3M.StartUpBoostManager.Services.Tests.StartupRegistry;

public class StartupAllUserRegistryServiceTests
{
    private readonly StartupAllUserRegistryService _sut;

    public StartupAllUserRegistryServiceTests()
    {
        _sut = new StartupAllUserRegistryService();
    }

    [Fact]
    public void StartupProgramSource_ShouldReturnAllUsersRegistry()
    {
        // Act
        var result = _sut.StartupProgramSource;

        // Assert
        result.Should().Be(StartupProgramSource.AllUsersRegistry);
    }

    [Fact]
    public void GetRegistryKey_ShouldReturnLocalMachineRunKey()
    {
        // Act
        var result = _sut.GetRegistryKey();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().EndWith(StartupAllUserRegistryService.ALL_USER_REGISTRY);
    }

    [Fact]
    public void ListAllStartupPrograms_WhenRegistryKeyExists_ShouldReturnAllPrograms()
    {
        // Arrange
        var testPrograms = new Dictionary<string, string>
        {
            { "TestApp1", "C:\\test1.exe" },
            { "TestApp2", "C:\\test2.exe" }
        };

        using var key = Registry.LocalMachine.CreateSubKey(StartupAllUserRegistryService.ALL_USER_REGISTRY);
        foreach (var program in testPrograms)
        {
            key.SetValue(program.Key, program.Value);
        }

        try
        {
            // Act
            var result = _sut.ListAllStartupPrograms().ToList();

            // Assert
            result.Should().HaveCount(testPrograms.Count);
            foreach (var program in result)
            {
                program.Command.Should().Be(testPrograms[program.Name]);
                program.Source.Should().Be(StartupProgramSource.AllUsersRegistry);
            }
        }
        finally
        {
            // Cleanup
            foreach (var program in testPrograms)
            {
                key.DeleteValue(program.Key, false);
            }
        }
    }

    [Fact]
    public void ListAllStartupPrograms_WhenRegistryKeyDoesNotExist_ShouldReturnEmptyCollection()
    {
        // Arrange
        var nonExistentPath = "Software\\NonExistent\\Path";
        var service = new TestStartupRegistryService(nonExistentPath);

        // Act
        var result = service.ListAllStartupPrograms();

        // Assert
        result.Should().BeEmpty();
    }

    private class TestStartupRegistryService : StartupRegistryService
    {
        private readonly string _registryPath;

        public TestStartupRegistryService(string registryPath)
        {
            _registryPath = registryPath;
        }

        public override RegistryKey? GetRegistryKey()
        {
            return Registry.LocalMachine.OpenSubKey(_registryPath, false);
        }

        public override StartupProgramSource StartupProgramSource => StartupProgramSource.AllUsersRegistry;
    }
}
