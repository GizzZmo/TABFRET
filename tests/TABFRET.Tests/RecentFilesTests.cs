using Xunit;
using TABFRET.ViewModels;
using TABFRET.Services;
using TABFRET.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TABFRET.Tests;

// Mock implementation for testing
public class MockMidiParser : IMidiParser
{
    public Task<IList<MidiNote>> ParseMidiFileAsync(string path)
    {
        return Task.FromResult<IList<MidiNote>>(new List<MidiNote>());
    }
}

public class RecentFilesTests
{
    [Fact]
    public async Task AddsFileToRecentFiles()
    {
        var mockParser = new MockMidiParser();
        var vm = new MainViewModel(mockParser, null!);
        string filePath = "test.mid";
        vm.RecentFiles.Clear();
        
        // Get the method and invoke it asynchronously
        var method = vm.GetType().GetMethod("LoadMidiAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var task = (Task)method?.Invoke(vm, new object[] { filePath })!;
        if (task != null)
        {
            await task;
        }
        
        Assert.Contains(filePath, vm.RecentFiles);
    }
}
