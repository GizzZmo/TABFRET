using Xunit;
using TABFRET.ViewModels;
using System.Collections.ObjectModel;

namespace TABFRET.Tests;

public class RecentFilesTests
{
    [Fact]
    public void AddsFileToRecentFiles()
    {
        var vm = new MainViewModel(null, null);
        string filePath = "test.mid";
        vm.RecentFiles.Clear();
        vm.GetType().GetMethod("LoadMidiAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(vm, new object[] { filePath });
        Assert.Contains(filePath, vm.RecentFiles);
    }
}
