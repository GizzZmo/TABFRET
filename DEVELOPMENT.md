# Development Guide

This guide helps developers set up their environment and understand the development workflow for TABFRET.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Initial Setup](#initial-setup)
- [Development Environment](#development-environment)
- [Building and Running](#building-and-running)
- [Testing](#testing)
- [Debugging](#debugging)
- [Common Development Tasks](#common-development-tasks)
- [Troubleshooting](#troubleshooting)

## Prerequisites

### Required Software

1. **Operating System**
   - Windows 10 (version 1809 or higher) or Windows 11
   - WPF applications only run on Windows

2. **Development Tools**
   - [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) (Community, Professional, or Enterprise)
     - Workload: ".NET desktop development"
     - Individual components:
       - .NET 6.0 Runtime
       - Windows 10 SDK
   - Alternative: [Visual Studio Code](https://code.visualstudio.com/) with C# extension (limited WPF designer support)

3. **.NET SDK**
   - [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
   - Verify installation:
     ```bash
     dotnet --version
     ```

4. **Version Control**
   - [Git](https://git-scm.com/downloads)
   - Verify installation:
     ```bash
     git --version
     ```

### Recommended Tools

1. **NuGet Package Manager**
   - Included with Visual Studio
   - CLI: Pre-installed with .NET SDK

2. **Git Client** (Optional)
   - [GitHub Desktop](https://desktop.github.com/)
   - [GitKraken](https://www.gitkraken.com/)
   - [SourceTree](https://www.sourcetreeapp.com/)

3. **Diff/Merge Tool** (Optional)
   - [WinMerge](https://winmerge.org/)
   - [Beyond Compare](https://www.scootersoftware.com/)
   - [KDiff3](http://kdiff3.sourceforge.net/)

4. **Markdown Editor** (For documentation)
   - [Typora](https://typora.io/)
   - [MarkText](https://marktext.app/)
   - VS Code with Markdown extensions

## Initial Setup

### 1. Clone the Repository

```bash
# Clone the repository
git clone https://github.com/GizzZmo/TABFRET.git

# Navigate to the project directory
cd TABFRET
```

### 2. Restore Dependencies

```bash
# Restore NuGet packages for the entire solution
dotnet restore TABFRET.sln
```

Expected output:
```
Restore succeeded in X.X s for TABFRET
```

### 3. Verify Build

```bash
# Build the solution
dotnet build TABFRET.sln --configuration Debug

# Or for Release build
dotnet build TABFRET.sln --configuration Release
```

Expected output:
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

### 4. Run Tests

```bash
# Run all tests
dotnet test TABFRET.sln
```

## Development Environment

### Visual Studio Setup

1. **Open the Solution**
   - Launch Visual Studio 2022
   - File → Open → Project/Solution
   - Select `TABFRET.sln`

2. **Configure Startup Project**
   - Right-click on `TABFRET` project in Solution Explorer
   - Select "Set as Startup Project"

3. **Recommended Extensions**
   - **ReSharper** (optional, paid): Advanced code analysis
   - **CodeMaid**: Code cleanup and organization
   - **Markdown Editor**: Edit markdown files
   - **Fine Code Coverage**: Visualize test coverage

4. **Editor Settings**
   - Tools → Options → Text Editor → C#
   - Enable:
     - Line numbers
     - Word wrap
     - Navigation bar
   - Code Style → Formatting: Use default C# conventions

### Visual Studio Code Setup

1. **Install Extensions**
   ```
   - C# (Microsoft)
   - C# Extensions (JosKreativ)
   - .NET Core Test Explorer
   - NuGet Package Manager
   - GitLens
   ```

2. **Configure Settings** (`.vscode/settings.json`)
   ```json
   {
     "omnisharp.enableRoslynAnalyzers": true,
     "omnisharp.enableEditorConfigSupport": true,
     "files.exclude": {
       "**/bin": true,
       "**/obj": true
     }
   }
   ```

3. **Build Tasks** (`.vscode/tasks.json`)
   ```json
   {
     "version": "2.0.0",
     "tasks": [
       {
         "label": "build",
         "command": "dotnet",
         "type": "process",
         "args": [
           "build",
           "${workspaceFolder}/TABFRET.sln",
           "/property:GenerateFullPaths=true"
         ]
       }
     ]
   }
   ```

## Building and Running

### Command Line

#### Debug Build
```bash
# Build
dotnet build TABFRET.sln --configuration Debug

# Run
dotnet run --project src/TABFRET.csproj
```

#### Release Build
```bash
# Build
dotnet build TABFRET.sln --configuration Release

# Run
dotnet run --project src/TABFRET.csproj --configuration Release
```

#### Publish for Distribution
```bash
# Create self-contained executable
dotnet publish src/TABFRET.csproj \
  --configuration Release \
  --output ./publish \
  --runtime win-x64 \
  --self-contained true
```

### Visual Studio

1. **Debug Mode** (F5)
   - Runs with debugger attached
   - Breakpoints are active
   - Slower execution

2. **Run Without Debugging** (Ctrl+F5)
   - Faster execution
   - No breakpoints
   - Better for UI testing

3. **Build Configurations**
   - **Debug**: Includes debug symbols, no optimization
   - **Release**: Optimized, smaller size, faster execution

### Understanding Build Output

```
TABFRET/
├── src/
│   ├── bin/
│   │   ├── Debug/
│   │   │   └── net6.0-windows/
│   │   │       ├── TABFRET.exe      # Executable
│   │   │       ├── TABFRET.dll      # Main DLL
│   │   │       ├── TABFRET.pdb      # Debug symbols
│   │   │       └── [dependencies]   # NuGet packages
│   │   └── Release/
│   └── obj/                          # Intermediate build files
```

## Testing

### Running Tests

#### All Tests
```bash
dotnet test TABFRET.sln
```

#### Specific Test Project
```bash
dotnet test tests/TABFRET.Tests/TABFRET.Tests.csproj
```

#### With Detailed Output
```bash
dotnet test TABFRET.sln --verbosity detailed
```

#### With Code Coverage
```bash
# Modern approach (recommended)
dotnet test TABFRET.sln --collect:"XPlat Code Coverage"

# Legacy approach
dotnet test TABFRET.sln /p:CollectCoverage=true
```

### Writing Tests

#### Unit Test Example
```csharp
using Xunit;

namespace TABFRET.Tests
{
    public class TabMapperTests
    {
        [Fact]
        public void MapMidiNotesToTab_ValidNote_ReturnsTabNote()
        {
            // Arrange
            var mapper = new TabMapper();
            var midiNotes = new List<MidiNote>
            {
                new MidiNote { MidiNoteNumber = 64, StartTimeTicks = 0 }
            };

            // Act
            var result = mapper.MapMidiNotesToTab(midiNotes);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].StringNumber); // High E
            Assert.Equal(0, result[0].FretNumber);   // Open string
        }
    }
}
```

### Test Coverage Goals

- **Unit Tests**: >80% coverage for services and utilities
- **Integration Tests**: Key workflows (MIDI load, playback)
- **UI Tests**: Manual testing for visual components

## Debugging

### Visual Studio Debugging

1. **Set Breakpoints**
   - Click in the left margin of code editor
   - Or press F9 on a line

2. **Debug Controls**
   - **F5**: Start debugging
   - **F10**: Step over
   - **F11**: Step into
   - **Shift+F11**: Step out
   - **F9**: Toggle breakpoint
   - **Ctrl+Shift+F9**: Delete all breakpoints

3. **Watch Windows**
   - **Locals**: Variables in current scope
   - **Autos**: Recently used variables
   - **Watch**: Custom expressions
   - **Call Stack**: Function call hierarchy

4. **Immediate Window**
   - Evaluate expressions during debugging
   - Execute code on the fly
   - Access: Debug → Windows → Immediate (Ctrl+Alt+I)

### Common Debugging Scenarios

#### MIDI Parsing Issues
```csharp
// Set breakpoint in MidiParser.ParseMidiFile
public async Task<List<MidiNote>> ParseMidiFile(string filePath)
{
    // Breakpoint here
    Console.WriteLine($"Parsing: {filePath}");
    // Inspect file path and parsed notes
}
```

#### Tab Mapping Issues
```csharp
// Set breakpoint in TabMapper.MapMidiNotesToTab
foreach (var midiNote in midiNotes)
{
    // Breakpoint here
    // Inspect midiNote.MidiNoteNumber
    // Check calculated fret numbers
}
```

#### UI Updates
```csharp
// Set breakpoint in ViewModel property setters
public bool IsMidiLoaded
{
    get => _isMidiLoaded;
    set
    {
        // Breakpoint here
        SetProperty(ref _isMidiLoaded, value);
    }
}
```

### Logging

Add diagnostic output:

```csharp
using System.Diagnostics;

// In your code
Debug.WriteLine($"Loading MIDI file: {filePath}");
Debug.WriteLine($"Parsed {notes.Count} notes");

// View in Output window (Debug → Windows → Output)
```

## Common Development Tasks

### Adding a New Feature

1. **Create a feature branch**
   ```bash
   git checkout -b feature/my-new-feature
   ```

2. **Implement the feature**
   - Add code in appropriate location (Model/View/ViewModel/Service)
   - Follow MVVM pattern
   - Add XML documentation comments

3. **Write tests**
   ```bash
   # Add test class in tests/TABFRET.Tests/
   # Run tests
   dotnet test
   ```

4. **Update documentation**
   - Add comments to code
   - Update README if needed
   - Add to CHANGELOG

5. **Commit and push**
   ```bash
   git add .
   git commit -m "Add: New feature description"
   git push origin feature/my-new-feature
   ```

### Updating Dependencies

```bash
# List outdated packages
dotnet list package --outdated

# Update a specific package
dotnet add src/TABFRET.csproj package NAudio --version 2.2.1

# Update all packages (careful!)
dotnet restore --force-evaluate
```

### Code Cleanup

1. **Format Code**
   - Visual Studio: Edit → Advanced → Format Document (Ctrl+K, Ctrl+D)
   - Command line:
     ```bash
     dotnet format TABFRET.sln
     ```

2. **Remove Unused Usings**
   - Visual Studio: Right-click → Remove and Sort Usings
   - Command line:
     ```bash
     dotnet format TABFRET.sln --include-generated
     ```

### Performance Profiling

1. **Visual Studio Profiler**
   - Debug → Performance Profiler
   - Select: CPU Usage, Memory Usage
   - Start profiling session

2. **Benchmark Tests**
   - Use BenchmarkDotNet for micro-benchmarks
   - Add to tests project

## Troubleshooting

### Build Errors

#### "Could not find SDK"
```bash
# Verify .NET SDK installation
dotnet --info

# Install .NET 6.0 SDK if missing
# Download from: https://dotnet.microsoft.com/download/dotnet/6.0
```

#### "Package restore failed"
```bash
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore packages
dotnet restore TABFRET.sln --force
```

#### "WPF not available"
```
Error: WPF is only supported on Windows
```
**Solution**: Use Windows for WPF development, or consider porting UI to AvaloniaUI for cross-platform.

### Runtime Issues

#### "DLL not found"
```bash
# Rebuild solution
dotnet clean
dotnet build
```

#### "MIDI file not loading"
- Check file path exists
- Verify file is valid MIDI format
- Check console output for error messages
- Test with known-good MIDI file

#### "UI not updating"
- Ensure ViewModel implements INotifyPropertyChanged
- Check PropertyChanged event is raised
- Verify View is bound to correct ViewModel
- Check Dispatcher.Invoke for cross-thread calls

### Development Environment Issues

#### Visual Studio slow to start
- Disable unnecessary extensions
- Clear component cache: `devenv /resetuserdata`
- Clear MEF cache: Delete `%LocalAppData%\Microsoft\VisualStudio\[version]\ComponentModelCache`

#### IntelliSense not working
- Rebuild solution
- Close and reopen solution
- Clear cache: `dotnet restore --force`
- Restart Visual Studio

## Best Practices

### Code Organization

1. **Follow MVVM pattern strictly**
2. **Keep methods small and focused**
3. **Use meaningful names**
4. **Add XML documentation for public APIs**
5. **Handle exceptions appropriately**

### Git Workflow

1. **Create feature branches**
2. **Commit frequently with clear messages**
3. **Pull main before creating PR**
4. **Keep commits atomic and focused**

### Performance

1. **Use async/await for I/O**
2. **Dispose of resources properly**
3. **Profile before optimizing**
4. **Cache expensive calculations**

### Security

1. **Validate file inputs**
2. **Handle exceptions gracefully**
3. **Don't expose sensitive information in logs**
4. **Use safe file operations**

## Resources

### Official Documentation
- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [WPF Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
- [C# Programming Guide](https://docs.microsoft.com/en-us/dotnet/csharp/)

### Libraries
- [Melanchall.DryWetMidi](https://github.com/melanchall/drywetmidi)
- [NAudio](https://github.com/naudio/NAudio)

### Learning Resources
- [WPF Tutorial](https://wpf-tutorial.com/)
- [MVVM Pattern](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/mvvm)

### Community
- [Stack Overflow - WPF](https://stackoverflow.com/questions/tagged/wpf)
- [Reddit - r/dotnet](https://www.reddit.com/r/dotnet/)
- [GitHub Discussions](https://github.com/GizzZmo/TABFRET/discussions)

---

For more information, see [CONTRIBUTING.md](CONTRIBUTING.md) and [ARCHITECTURE.md](ARCHITECTURE.md).
