# TABFRET Code Guide

This guide provides detailed information about the TABFRET codebase, explaining each file, class, and component in the project.

> **Note:** This is extracted from the original HowTo.txt file and converted to markdown for better readability. For high-level architecture, see [ARCHITECTURE.md](ARCHITECTURE.md).

## Table of Contents

- [Project Overview](#project-overview)
- [Project Structure](#project-structure)
- [Core Files](#core-files)
- [Models](#models)
- [Views](#views)
- [ViewModels](#viewmodels)
- [Services](#services)
- [Utilities](#utilities)
- [Configuration](#configuration)

---

## Project Overview

TABFRET follows the **MVVM (Model-View-ViewModel)** architectural pattern for clear separation of concerns.

### Technology Stack

- **Framework**: WPF (Windows Presentation Foundation)
- **Language**: C# 
- **Target**: .NET 6.0 (net6.0-windows)
- **Pattern**: MVVM

### Key Concepts

1. **Models**: Data structures (MidiNote, TabNote)
2. **Views**: XAML UI files (MainWindow.xaml, GuitarNeckView.xaml)
3. **ViewModels**: UI logic and commands (MainViewModel)
4. **Services**: Business logic (MidiParser, TabMapper)
5. **Utils**: Helper functions (FretboardHelper)

---

## Project Structure

```
TABFRET/
├── TABFRET.sln                    # Visual Studio Solution file
├── src/
│   ├── TABFRET.csproj             # C# Project file
│   ├── App.xaml                   # Application entry point
│   ├── App.xaml.cs                # Application code-behind
│   ├── MainWindow.xaml            # Main UI window
│   ├── MainWindow.xaml.cs         # Main window code-behind
│   │
│   ├── Models/                    # Data models
│   │   ├── MidiNote.cs            # MIDI note structure
│   │   └── TabNote.cs             # Tab note structure
│   │
│   ├── Views/                     # UI Components
│   │   ├── GuitarNeckView.xaml    # Guitar neck visualization
│   │   └── GuitarNeckView.xaml.cs # Guitar neck code-behind
│   │
│   ├── ViewModels/                # UI logic
│   │   └── MainViewModel.cs       # Main ViewModel
│   │
│   ├── Services/                  # Business logic
│   │   ├── MidiParser.cs          # MIDI file parsing
│   │   └── TabMapper.cs           # MIDI to tab conversion
│   │
│   ├── Utils/                     # Helper functions
│   │   └── FretboardHelper.cs     # Fretboard calculations
│   │
│   └── Config/                    # Configuration
│       └── settings.json          # User settings
│
├── tests/                         # Unit tests
│   └── TABFRET.Tests/
│
└── README.md                      # Project documentation
```

---

## Core Files

### App.xaml

**Location**: `src/App.xaml`

**Purpose**: Application definition file - the entry point for the WPF application.

**Key Elements**:
- Defines application-wide resources
- Specifies startup URI (MainWindow.xaml)
- Can define global styles and resources

**Code**:
```xml
<Application x:Class="TABFRET.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:TABFRET"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <!-- Global resources can be defined here -->
    </Application.Resources>
</Application>
```

**Customization Points**:
- Add global styles in `Application.Resources`
- Add merged resource dictionaries
- Handle application-level events

---

### App.xaml.cs

**Location**: `src/App.xaml.cs`

**Purpose**: Code-behind for App.xaml. Handles application-level events and initialization.

**Code**:
```csharp
using System.Configuration;
using System.Data;
using System.Windows;

namespace TABFRET
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Application-level initialization can go here
    }
}
```

**When to Use**:
- Application startup logic
- Global exception handling
- Resource initialization
- Dependency injection setup

---

### MainWindow.xaml

**Location**: `src/MainWindow.xaml`

**Purpose**: Defines the main user interface window layout and structure.

**Key Features**:
- Control buttons (Load, Play, Pause, Stop)
- Guitar neck visualization area
- Status bar
- Data binding to MainViewModel

**Structure**:
```xml
<Window x:Class="TABFRET.MainWindow"
        Title="TABFRET: MIDI to Guitar Tab Visualizer" 
        Height="600" Width="900"
        MinHeight="450" MinWidth="700">
    
    <Window.DataContext>
        <local:MainViewModel/>
    </Window.DataContext>
    
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>    <!-- Control bar -->
            <RowDefinition Height="*"/>       <!-- Fretboard -->
            <RowDefinition Height="Auto"/>    <!-- Status bar -->
        </Grid.RowDefinitions>

        <!-- Control buttons -->
        <StackPanel Grid.Row="0" Orientation="Horizontal" ...>
            <Button Content="Load MIDI" Command="{Binding LoadMidiCommand}" />
            <Button Content="Play" Command="{Binding PlayCommand}" />
            <Button Content="Pause" Command="{Binding PauseCommand}" />
            <Button Content="Stop" Command="{Binding StopCommand}" />
        </StackPanel>

        <!-- Guitar neck visualization -->
        <local:GuitarNeckView Grid.Row="1" ... />

        <!-- Status bar -->
        <StatusBar Grid.Row="2" ...>
            <TextBlock Text="{Binding StatusMessage}" />
            <TextBlock Text="{Binding MidiFilePath}" />
        </StatusBar>
    </Grid>
</Window>
```

**Data Bindings**:
- Commands → ViewModel commands
- StatusMessage → Status text
- MidiFilePath → Current file

---

### MainWindow.xaml.cs

**Location**: `src/MainWindow.xaml.cs`

**Purpose**: Code-behind for MainWindow. Minimal in MVVM pattern.

**Code**:
```csharp
using System.Windows;

namespace TABFRET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // DataContext is set in XAML
        }
    }
}
```

**Note**: In MVVM, most logic belongs in the ViewModel, not here.

---

## Models

### MidiNote.cs

**Location**: `src/Models/MidiNote.cs`

**Purpose**: Represents a single MIDI note event extracted from a MIDI file.

**Structure**:
```csharp
namespace TABFRET
{
    /// <summary>
    /// Defines the structure for representing a single MIDI note
    /// </summary>
    public struct MidiNote
    {
        public int MidiNoteNumber { get; set; }     // 0-127 (60 = Middle C)
        public long StartTimeTicks { get; set; }    // Start time in MIDI ticks
        public long DurationTicks { get; set; }     // Duration in MIDI ticks
        public int Velocity { get; set; }           // How hard (0-127)
        public int TrackNumber { get; set; }        // Which MIDI track

        public override string ToString()
        {
            return $"Note: {MidiNoteNumber}, Start: {StartTimeTicks}, " +
                   $"Duration: {DurationTicks}, Track: {TrackNumber}, " +
                   $"Velocity: {Velocity}";
        }
    }
}
```

**Properties Explained**:

| Property | Type | Range | Description |
|----------|------|-------|-------------|
| MidiNoteNumber | int | 0-127 | MIDI note number (60 = Middle C/C4) |
| StartTimeTicks | long | 0+ | When the note starts (in MIDI ticks) |
| DurationTicks | long | 0+ | How long the note lasts (in MIDI ticks) |
| Velocity | int | 0-127 | How hard the note was played |
| TrackNumber | int | 0+ | Which MIDI track contains this note |

**MIDI Note Numbers** (Examples):
- 40 = E2 (Low E string, open)
- 45 = A2 (A string, open)
- 50 = D3 (D string, open)
- 55 = G3 (G string, open)
- 59 = B3 (B string, open)
- 64 = E4 (High E string, open)

---

### TabNote.cs

**Location**: `src/Models/TabNote.cs`

**Purpose**: Represents a note on guitar tablature after conversion from MIDI.

**Enums**:
```csharp
public enum GuitarString
{
    HighE = 1,  // MIDI Note 64 (E4)
    B = 2,      // MIDI Note 59 (B3)
    G = 3,      // MIDI Note 55 (G3)
    D = 4,      // MIDI Note 50 (D3)
    A = 5,      // MIDI Note 45 (A2)
    LowE = 6    // MIDI Note 40 (E2)
}
```

**Structure**:
```csharp
public struct TabNote
{
    public int StringNumber { get; set; }           // 1 (High E) to 6 (Low E)
    public int FretNumber { get; set; }             // 0 for open string
    public long StartTimeTicks { get; set; }        // From MIDI
    public long DurationTicks { get; set; }         // From MIDI
    public int OriginalMidiNoteNumber { get; set; } // Reference

    public override string ToString()
    {
        return $"String: {StringNumber}, Fret: {FretNumber}, " +
               $"Start: {StartTimeTicks}";
    }
}
```

**Standard Tuning (EADGBe)**:
| String | Number | Note | MIDI | Frequency |
|--------|--------|------|------|-----------|
| High E | 1 | E4 | 64 | 329.63 Hz |
| B | 2 | B3 | 59 | 246.94 Hz |
| G | 3 | G3 | 55 | 196.00 Hz |
| D | 4 | D3 | 50 | 146.83 Hz |
| A | 5 | A2 | 45 | 110.00 Hz |
| Low E | 6 | E2 | 40 | 82.41 Hz |

---

## Views

### GuitarNeckView.xaml

**Location**: `src/Views/GuitarNeckView.xaml`

**Purpose**: XAML definition for the guitar neck visualization.

**Code**:
```xml
<UserControl x:Class="TABFRET.GuitarNeckView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:TABFRET">
    <Grid>
        <Canvas x:Name="FretboardCanvas" 
                Background="LightGray"
                HorizontalAlignment="Stretch" 
                VerticalAlignment="Stretch"/>
    </Grid>
</UserControl>
```

**Key Element**: `FretboardCanvas` - Where all drawing happens

---

### GuitarNeckView.xaml.cs

**Location**: `src/Views/GuitarNeckView.xaml.cs`

**Purpose**: Code-behind that handles drawing the guitar neck and notes.

**Key Methods**:

#### Constructor
```csharp
public GuitarNeckView()
{
    InitializeComponent();
    this.Loaded += GuitarNeckView_Loaded;
    this.SizeChanged += GuitarNeckView_SizeChanged;
}
```

#### DrawGuitarNeck()
Draws static elements: strings, frets, fret markers.

```csharp
private void DrawGuitarNeck()
{
    FretboardCanvas.Children.Clear();
    
    // Draw strings (6 horizontal lines)
    for (int i = 1; i <= 6; i++)
    {
        double yPos = FretboardHelper.GetYPositionForString(i, canvasHeight);
        // Draw line at yPos
    }
    
    // Draw frets (vertical lines, 0-15)
    for (int i = 0; i <= 15; i++)
    {
        double xPos = FretboardHelper.GetXPositionForFret(i, canvasWidth, 15);
        // Draw line at xPos
    }
    
    // Draw fret markers (dots at 3, 5, 7, 9, 12, 15)
    DrawFretMarkers(FretboardCanvas, canvasWidth, canvasHeight);
}
```

#### UpdateVisualNotes()
Draws note circles based on current tablature data.

```csharp
private void UpdateVisualNotes()
{
    // Remove old note visuals
    // For each note in CurrentTabNotes:
    //   Calculate position
    //   Create ellipse (circle)
    //   Highlight if currently playing
    //   Add to canvas
}
```

**Drawing Logic**:
1. Calculate X position (fret position)
2. Calculate Y position (string position)
3. Create ellipse (circle)
4. Set color (blue or red if playing)
5. Add fret number text
6. Add to canvas

---

## ViewModels

### MainViewModel.cs

**Location**: `src/ViewModels/MainViewModel.cs`

**Purpose**: Central ViewModel managing UI state and user interactions.

**Class Definition**:
```csharp
public class MainViewModel : INotifyPropertyChanged
{
    // Services
    private readonly MidiParser _midiParser;
    private readonly TabMapper _tabMapper;
    private DispatcherTimer _playbackTimer;

    // Properties (with INotifyPropertyChanged)
    public string MidiFilePath { get; set; }
    public bool IsMidiLoaded { get; set; }
    public ObservableCollection<TabNote> CurrentTabNotes { get; set; }
    public long CurrentPlaybackPositionTicks { get; set; }
    public bool IsPlaying { get; set; }
    public string StatusMessage { get; set; }

    // Commands
    public ICommand LoadMidiCommand { get; }
    public ICommand PlayCommand { get; }
    public ICommand PauseCommand { get; }
    public ICommand StopCommand { get; }
}
```

**Key Methods**:

#### LoadMidiFile()
```csharp
private async Task LoadMidiFile()
{
    // 1. Show file dialog (or use mock path)
    // 2. Update status: "Loading..."
    // 3. Parse MIDI file → List<MidiNote>
    // 4. Map to tablature → List<TabNote>
    // 5. Update CurrentTabNotes
    // 6. Set IsMidiLoaded = true
    // 7. Update status with note count
}
```

#### Play()
```csharp
private void Play()
{
    IsPlaying = true;
    _playbackTimer.Start();
    StatusMessage = "Playing...";
}
```

#### Pause()
```csharp
private void Pause()
{
    IsPlaying = false;
    _playbackTimer.Stop();
    StatusMessage = "Paused.";
}
```

#### Stop()
```csharp
private void Stop()
{
    IsPlaying = false;
    _playbackTimer.Stop();
    CurrentPlaybackPositionTicks = 0;
    StatusMessage = "Stopped.";
}
```

**INotifyPropertyChanged Implementation**:
```csharp
public event PropertyChangedEventHandler PropertyChanged;

protected bool SetProperty<T>(ref T storage, T value, 
    [CallerMemberName] string propertyName = null)
{
    if (Equals(storage, value)) return false;
    storage = value;
    OnPropertyChanged(propertyName);
    return true;
}

protected void OnPropertyChanged(string propertyName)
{
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
```

**RelayCommand Implementation**:
```csharp
public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter) => 
        _canExecute == null || _canExecute(parameter);
    
    public void Execute(object parameter) => _execute(parameter);

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}
```

---

## Services

### MidiParser.cs

**Location**: `src/Services/MidiParser.cs`

**Purpose**: Parses MIDI files and extracts note data.

**Note**: Current implementation uses mock data. Production code would use Melanchall.DryWetMidi library.

**Method**:
```csharp
public async Task<List<MidiNote>> ParseMidiFile(string filePath)
{
    // MOCK IMPLEMENTATION:
    // - Generate sample notes for demonstration
    // - Simulate async file reading with Task.Delay
    
    // REAL IMPLEMENTATION (future):
    // using Melanchall.DryWetMidi;
    // var midiFile = MidiFile.Read(filePath);
    // var notes = midiFile.GetNotes();
    // return ConvertToMidiNotes(notes);
}
```

**Mock Data Generated**:
- C Major Chord (C4, E4, G4)
- D Minor Chord (D4, F4, A4)
- Simple melody (C5, B4, A4, G4)

**Real Implementation** (commented example):
```csharp
// using Melanchall.DryWetMidi;
// using Melanchall.DryWetMidi.Core;
// using Melanchall.DryWetMidi.Interaction;

public async Task<List<MidiNote>> ParseMidiFile(string filePath)
{
    var midiFile = MidiFile.Read(filePath);
    var notes = midiFile.GetNotes();
    
    return notes.Select(n => new MidiNote
    {
        MidiNoteNumber = n.NoteNumber,
        StartTimeTicks = (long)n.Time,
        DurationTicks = (long)n.Length,
        Velocity = n.Velocity,
        TrackNumber = 0 // Simplified
    }).ToList();
}
```

---

### TabMapper.cs

**Location**: `src/Services/TabMapper.cs`

**Purpose**: Converts MIDI notes into guitar tablature.

**Constants**:
```csharp
private static readonly Dictionary<GuitarString, int> StandardTuningMidiNotes = 
    new Dictionary<GuitarString, int>
{
    { GuitarString.HighE, 64 },  // E4
    { GuitarString.B, 59 },      // B3
    { GuitarString.G, 55 },      // G3
    { GuitarString.D, 50 },      // D3
    { GuitarString.A, 45 },      // A2
    { GuitarString.LowE, 40 }    // E2
};

private const int MaxFret = 24;
```

**Main Method**:
```csharp
public List<TabNote> MapMidiNotesToTab(List<MidiNote> midiNotes, 
    Dictionary<GuitarString, int> tuning = null)
{
    var tabNotes = new List<TabNote>();
    var currentTuning = tuning ?? StandardTuningMidiNotes;

    foreach (var midiNote in midiNotes.OrderBy(n => n.StartTimeTicks))
    {
        TabNote? bestTabNote = null;
        int lowestFret = int.MaxValue;

        // Try each string
        foreach (var stringEntry in currentTuning.OrderBy(s => (int)s.Key))
        {
            int stringNumber = (int)stringEntry.Key;
            int openStringNote = stringEntry.Value;
            int fret = midiNote.MidiNoteNumber - openStringNote;

            if (fret >= 0 && fret <= MaxFret && fret < lowestFret)
            {
                lowestFret = fret;
                bestTabNote = new TabNote
                {
                    StringNumber = stringNumber,
                    FretNumber = fret,
                    StartTimeTicks = midiNote.StartTimeTicks,
                    DurationTicks = midiNote.DurationTicks,
                    OriginalMidiNoteNumber = midiNote.MidiNoteNumber
                };
            }
        }

        if (bestTabNote.HasValue)
        {
            tabNotes.Add(bestTabNote.Value);
        }
    }

    return tabNotes;
}
```

**Algorithm**:
1. For each MIDI note:
   - Check all 6 guitar strings
   - Calculate fret = MIDI note - open string note
   - If fret is valid (0-24), this is a possible position
   - Choose the lowest fret number
2. Return list of tab notes

**Example**:
- MIDI Note 67 (G4)
- String 1 (E4, 64): fret = 67 - 64 = 3 ✓
- String 2 (B3, 59): fret = 67 - 59 = 8 
- String 3 (G3, 55): fret = 67 - 55 = 12
- **Choose String 1, Fret 3** (lowest)

---

## Utilities

### FretboardHelper.cs

**Location**: `src/Utils/FretboardHelper.cs`

**Purpose**: Static helper functions for fretboard calculations and musical conversions.

**Constants**:
```csharp
private const double A4_FREQUENCY = 440.0;
private const int A4_MIDINOTE = 69;
private const double TWELFTH_ROOT_OF_2 = 1.0594635; // 2^(1/12)
```

**Key Methods**:

#### GetXPositionForFret()
```csharp
public static double GetXPositionForFret(int fretNumber, double canvasWidth, 
    int totalFretsToDisplay)
{
    // Calculates horizontal position for a fret
    // Uses proportional spacing for visual clarity
    double proportionalPosition = (double)fretNumber / totalFretsToDisplay;
    return canvasWidth * (proportionalPosition * 0.95 + (fretNumber > 0 ? 0.05 : 0));
}
```

#### GetYPositionForString()
```csharp
public static double GetYPositionForString(int stringNumber, double canvasHeight)
{
    // Distributes strings evenly across canvas height
    double stringSpacing = canvasHeight / 7; // 6 strings + padding
    return stringSpacing * stringNumber;
}
```

#### MidiToFrequency()
```csharp
public static double MidiToFrequency(int midiNoteNumber)
{
    // Convert MIDI note number to frequency in Hz
    // Formula: f = 440 * 2^((n-69)/12)
    return A4_FREQUENCY * Math.Pow(TWELFTH_ROOT_OF_2, midiNoteNumber - A4_MIDINOTE);
}
```

#### FrequencyToMidi()
```csharp
public static int FrequencyToMidi(double frequency)
{
    // Convert frequency to nearest MIDI note number
    return (int)Math.Round(A4_MIDINOTE + (12 * Math.Log(frequency / A4_FREQUENCY, 2)));
}
```

#### GetMidiNoteName()
```csharp
public static string GetMidiNoteName(int midiNoteNumber)
{
    string[] noteNames = { "C", "C#", "D", "D#", "E", "F", 
                          "F#", "G", "G#", "A", "A#", "B" };
    int octave = (midiNoteNumber / 12) - 1;
    int noteIndex = midiNoteNumber % 12;
    return $"{noteNames[noteIndex]}{octave}";
}
```

**Examples**:
```csharp
// MIDI 60 → "C4" (Middle C)
// MIDI 64 → "E4" (High E string, open)
// MIDI 440 Hz → MIDI 69 (A4)
```

---

## Configuration

### settings.json

**Location**: `src/Config/settings.json`

**Purpose**: User-configurable settings for the application.

**Structure**:
```json
{
  "GeneralSettings": {
    "DefaultMidiFolder": "",
    "AutoLoadLastMidi": false
  },
  "VisualSettings": {
    "NoteHighlightColor": "#FFFF0000",
    "FretboardBackgroundColor": "#FFD3D3D3",
    "ShowFretNumbers": true
  },
  "PlaybackSettings": {
    "PlaybackSpeedMultiplier": 1.0,
    "MetronomeEnabled": false
  },
  "GuitarTuning": {
    "DefaultTuning": "StandardEADGBe"
  }
}
```

**How to Use** (future implementation):
```csharp
// Load settings
var json = File.ReadAllText("Config/settings.json");
var settings = JsonSerializer.Deserialize<AppSettings>(json);

// Apply settings
NoteColor = settings.VisualSettings.NoteHighlightColor;
```

---

## Data Flow Examples

### Loading a MIDI File

```
User Action:
├── Click "Load MIDI" button
│
View Layer (MainWindow.xaml):
├── Button Command binding: LoadMidiCommand
│
ViewModel Layer (MainViewModel):
├── LoadMidiCommand.Execute()
├── LoadMidiFile() method
│   ├── Show file dialog
│   ├── Set StatusMessage = "Loading..."
│   ├── Call MidiParser.ParseMidiFile(path)
│   │
Service Layer (MidiParser):
│   ├── Read MIDI file
│   ├── Extract note events
│   ├── Return List<MidiNote>
│   │
ViewModel Layer (MainViewModel):
│   ├── Call TabMapper.MapMidiNotesToTab(midiNotes)
│   │
Service Layer (TabMapper):
│   ├── For each MIDI note:
│   │   ├── Check each guitar string
│   │   ├── Calculate fret numbers
│   │   ├── Choose best position
│   ├── Return List<TabNote>
│   │
ViewModel Layer (MainViewModel):
│   ├── Update CurrentTabNotes collection
│   ├── Set IsMidiLoaded = true
│   ├── StatusMessage = "Loaded X notes"
│   ├── Fire PropertyChanged events
│
View Layer (GuitarNeckView):
├── Receive CurrentTabNotes update
├── Call UpdateVisualNotes()
├── Draw note circles on fretboard
│
Result: User sees notes on fretboard
```

### Playback

```
User Action:
├── Click "Play" button
│
ViewModel (MainViewModel):
├── PlayCommand.Execute()
├── Play() method
├── IsPlaying = true
├── _playbackTimer.Start()
│
Timer Tick (every 50ms):
├── PlaybackTimer_Tick()
├── CurrentPlaybackPositionTicks += increment
├── Fire PropertyChanged("CurrentPlaybackPositionTicks")
│
View (GuitarNeckView):
├── Receive PropertyChanged event
├── Call UpdateVisualNotes()
├── Check which notes are in time range
├── Highlight notes with red color
│
Result: Notes light up as song plays
```

---

## Extension Points

### Adding a New Feature

**Example: Add Speed Control**

1. **Add property to ViewModel**:
```csharp
private double _playbackSpeed = 1.0;
public double PlaybackSpeed
{
    get => _playbackSpeed;
    set => SetProperty(ref _playbackSpeed, value);
}
```

2. **Add slider to MainWindow.xaml**:
```xml
<Slider Value="{Binding PlaybackSpeed}" 
        Minimum="0.25" Maximum="2.0" />
```

3. **Use in playback calculation**:
```csharp
private void PlaybackTimer_Tick(object sender, EventArgs e)
{
    long increment = (long)(ticksPerMs * PlaybackSpeed);
    CurrentPlaybackPositionTicks += increment;
}
```

### Adding a New Service

**Example: Add ExportService**

1. **Create service class**:
```csharp
public class ExportService
{
    public void ExportToPdf(List<TabNote> notes, string filename)
    {
        // Implementation
    }
}
```

2. **Add to ViewModel**:
```csharp
private readonly ExportService _exportService;

public ICommand ExportCommand { get; }

private void Export()
{
    _exportService.ExportToPdf(CurrentTabNotes.ToList(), "output.pdf");
}
```

3. **Add button to UI**:
```xml
<Button Content="Export PDF" Command="{Binding ExportCommand}" />
```

---

## Testing Guidelines

### Unit Test Example

```csharp
[TestClass]
public class TabMapperTests
{
    [TestMethod]
    public void MapMidiNotesToTab_HighEOpen_ReturnsCorrectTab()
    {
        // Arrange
        var mapper = new TabMapper();
        var midiNotes = new List<MidiNote>
        {
            new MidiNote { MidiNoteNumber = 64, StartTimeTicks = 0 } // E4
        };

        // Act
        var result = mapper.MapMidiNotesToTab(midiNotes);

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(1, result[0].StringNumber); // High E
        Assert.AreEqual(0, result[0].FretNumber);   // Open
    }
}
```

---

## Best Practices

### MVVM Guidelines

1. **Views should be thin**: Minimal code-behind
2. **ViewModels contain logic**: UI state and commands
3. **Models are pure data**: No UI knowledge
4. **Services are reusable**: Independent of UI

### Code Style

1. **Use async/await** for I/O operations
2. **Implement INotifyPropertyChanged** for bindable properties
3. **Use ObservableCollection** for bindable lists
4. **Commands for user actions**: Not event handlers
5. **XML documentation** for public APIs

### Performance

1. **Avoid UI thread blocking**: Use async methods
2. **Batch UI updates**: Use Dispatcher.BeginInvoke
3. **Cache calculations**: Store fret positions
4. **Limit redrawing**: Only update changed elements

---

## Further Reading

- [WPF Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
- [MVVM Pattern](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/mvvm)
- [DryWetMidi Library](https://github.com/melanchall/drywetmidi)
- [NAudio Library](https://github.com/naudio/NAudio)

---

*For architecture overview, see [ARCHITECTURE.md](ARCHITECTURE.md)*  
*For development setup, see [DEVELOPMENT.md](DEVELOPMENT.md)*  
*For contributing, see [CONTRIBUTING.md](CONTRIBUTING.md)*
