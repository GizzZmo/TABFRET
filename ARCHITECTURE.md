# TABFRET Architecture

This document provides an overview of the TABFRET application architecture, design patterns, and project structure.

## Table of Contents

- [Overview](#overview)
- [Architecture Pattern](#architecture-pattern)
- [Project Structure](#project-structure)
- [Core Components](#core-components)
- [Data Flow](#data-flow)
- [Key Technologies](#key-technologies)
- [Design Decisions](#design-decisions)

## Overview

TABFRET is a Windows desktop application built with WPF (Windows Presentation Foundation) that converts MIDI files into guitar tablature and provides real-time visualization on a virtual guitar neck.

### High-Level Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                      TABFRET Application                     │
├─────────────────────────────────────────────────────────────┤
│                                                               │
│   ┌─────────────┐      ┌──────────────┐    ┌─────────────┐ │
│   │    Views    │◄────►│  ViewModels  │◄──►│   Models    │ │
│   │   (XAML)    │      │   (Logic)    │    │   (Data)    │ │
│   └─────────────┘      └──────────────┘    └─────────────┘ │
│                              │                               │
│                              ▼                               │
│                    ┌──────────────────┐                     │
│                    │    Services      │                     │
│                    ├──────────────────┤                     │
│                    │  - MidiParser    │                     │
│                    │  - TabMapper     │                     │
│                    └──────────────────┘                     │
│                              │                               │
│                              ▼                               │
│                    ┌──────────────────┐                     │
│                    │   Utilities      │                     │
│                    ├──────────────────┤                     │
│                    │ - FretboardHelper│                     │
│                    └──────────────────┘                     │
└─────────────────────────────────────────────────────────────┘
```

## Architecture Pattern

TABFRET follows the **MVVM (Model-View-ViewModel)** architectural pattern, which provides clear separation of concerns and makes the application more maintainable and testable.

### MVVM Components

#### Models
- Represent the data structures and business entities
- Pure data containers with minimal logic
- Examples: `MidiNote`, `TabNote`

#### Views
- XAML files that define the user interface
- Minimal code-behind (only UI-related logic)
- Data-bound to ViewModels
- Examples: `MainWindow.xaml`, `GuitarNeckView.xaml`

#### ViewModels
- Bridge between Views and Models
- Contains presentation logic
- Implements `INotifyPropertyChanged` for data binding
- Exposes commands for user interactions
- Example: `MainViewModel`

#### Services
- Contain business logic and external interactions
- Reusable across different ViewModels
- Examples: `MidiParser`, `TabMapper`

## Project Structure

```
TABFRET/
├── src/                           # Source code
│   ├── App.xaml                   # Application entry point
│   ├── App.xaml.cs                # Application code-behind
│   ├── MainWindow.xaml            # Main application window
│   ├── MainWindow.xaml.cs         # Main window code-behind
│   │
│   ├── Models/                    # Data models
│   │   ├── MidiNote.cs            # MIDI note structure
│   │   └── TabNote.cs             # Tablature note structure
│   │
│   ├── Views/                     # UI components
│   │   ├── GuitarNeckView.xaml    # Guitar neck visualization
│   │   └── GuitarNeckView.xaml.cs # Guitar neck code-behind
│   │
│   ├── ViewModels/                # UI logic
│   │   └── MainViewModel.cs       # Main application ViewModel
│   │
│   ├── Services/                  # Business logic
│   │   ├── MidiParser.cs          # MIDI file parsing
│   │   └── TabMapper.cs           # MIDI to tab conversion
│   │
│   ├── Utils/                     # Helper functions
│   │   └── FretboardHelper.cs     # Fretboard calculations
│   │
│   ├── Config/                    # Configuration
│   │   └── settings.json          # User settings
│   │
│   └── TABFRET.csproj             # Project file
│
├── tests/                         # Test projects
│   └── TABFRET.Tests/             # Unit tests
│
├── assets/                        # Images and resources
├── .github/                       # GitHub workflows
├── README.md                      # Project documentation
└── TABFRET.sln                    # Visual Studio solution
```

## Core Components

### 1. Models

#### MidiNote
Represents a single MIDI note event.

**Properties:**
- `MidiNoteNumber` (int): MIDI note number (0-127)
- `StartTimeTicks` (long): Note start time in MIDI ticks
- `DurationTicks` (long): Note duration in MIDI ticks
- `Velocity` (int): Note velocity (0-127)
- `TrackNumber` (int): MIDI track number

#### TabNote
Represents a note on guitar tablature.

**Properties:**
- `StringNumber` (int): Guitar string (1-6)
- `FretNumber` (int): Fret position (0 for open string)
- `StartTimeTicks` (long): Note timing
- `DurationTicks` (long): Note duration
- `OriginalMidiNoteNumber` (int): Reference to original MIDI note

#### GuitarString Enum
Defines standard guitar strings and their MIDI note numbers:
- HighE (1): MIDI 64 (E4)
- B (2): MIDI 59 (B3)
- G (3): MIDI 55 (G3)
- D (4): MIDI 50 (D3)
- A (5): MIDI 45 (A2)
- LowE (6): MIDI 40 (E2)

### 2. ViewModels

#### MainViewModel
The central ViewModel managing application state and user interactions.

**Key Responsibilities:**
- Load and manage MIDI files
- Control playback (Play, Pause, Stop)
- Maintain current playback position
- Expose data to views via observable properties
- Coordinate between services and views

**Properties:**
- `MidiFilePath`: Path to loaded MIDI file
- `IsMidiLoaded`: Whether a MIDI file is loaded
- `CurrentTabNotes`: Observable collection of tab notes
- `CurrentPlaybackPositionTicks`: Current playback position
- `IsPlaying`: Playback state
- `StatusMessage`: Status bar message

**Commands:**
- `LoadMidiCommand`: Load a MIDI file
- `PlayCommand`: Start playback
- `PauseCommand`: Pause playback
- `StopCommand`: Stop playback and reset

### 3. Views

#### MainWindow
The main application window.

**Features:**
- Control buttons (Load, Play, Pause, Stop)
- Guitar neck visualization
- Status bar with file info

**Data Binding:**
- Commands bound to buttons
- Status message displayed in status bar
- MIDI file path shown in status bar

#### GuitarNeckView
Custom user control for guitar neck visualization.

**Responsibilities:**
- Draw guitar neck (strings, frets, markers)
- Visualize notes on the fretboard
- Highlight currently playing notes
- Respond to playback position changes

**Visual Elements:**
- 6 horizontal lines (strings)
- Vertical lines (frets, 0-15)
- Fret markers at positions 3, 5, 7, 9, 12, 15
- Note circles with fret numbers
- Highlighted notes during playback

### 4. Services

#### MidiParser
Parses MIDI files and extracts note information.

**Responsibilities:**
- Read MIDI file format
- Extract note events (Note On/Off)
- Parse timing information
- Return structured note data

**Dependencies:**
- Melanchall.DryWetMidi library

**Note:** Current implementation includes mock data for demonstration. A real implementation uses the DryWetMidi library to parse actual MIDI files.

#### TabMapper
Converts MIDI notes to guitar tablature.

**Algorithm:**
1. For each MIDI note:
   - Check each guitar string
   - Calculate fret number (MIDI note - open string note)
   - Validate fret is within range (0-24)
   - Select lowest fret for simplicity

**Tuning Support:**
- Currently supports standard tuning (EADGBe)
- Extensible to support alternate tunings

**Mapping Strategy:**
- Prioritizes lower frets
- Could be enhanced for:
  - Keeping melodic lines on same string
  - Avoiding awkward stretches in chords
  - Preferring open strings
  - Position-based fingering

### 5. Utilities

#### FretboardHelper
Static helper class for fretboard calculations.

**Functions:**

##### Position Calculations
- `GetXPositionForFret()`: Calculate horizontal fret position
- `GetYPositionForString()`: Calculate vertical string position

##### Musical Conversions
- `MidiToFrequency()`: Convert MIDI note to frequency (Hz)
- `FrequencyToMidi()`: Convert frequency to MIDI note
- `GetMidiNoteName()`: Get note name (e.g., "C#4")

**Constants:**
- A4 frequency: 440 Hz
- A4 MIDI note: 69
- Twelfth root of 2: 1.0594635 (for semitone calculations)

## Data Flow

### Loading a MIDI File

```
User clicks "Load MIDI"
        ↓
MainViewModel.LoadMidiCommand executes
        ↓
MidiParser.ParseMidiFile(path)
        ↓
Returns List<MidiNote>
        ↓
TabMapper.MapMidiNotesToTab(midiNotes)
        ↓
Returns List<TabNote>
        ↓
MainViewModel updates CurrentTabNotes
        ↓
View automatically updates (data binding)
        ↓
GuitarNeckView draws notes on fretboard
```

### Playback Flow

```
User clicks "Play"
        ↓
MainViewModel.PlayCommand executes
        ↓
Starts DispatcherTimer (50ms interval)
        ↓
Timer tick increments CurrentPlaybackPositionTicks
        ↓
PropertyChanged event fires
        ↓
GuitarNeckView.UpdateVisualNotes() called
        ↓
Notes within current time range highlighted
        ↓
Continues until end of song or Stop clicked
```

## Key Technologies

### Frameworks and Libraries

1. **WPF (Windows Presentation Foundation)**
   - UI framework for Windows desktop
   - XAML-based declarative UI
   - Rich data binding support

2. **Melanchall.DryWetMidi** (v6.0.0)
   - Professional MIDI library
   - File parsing and manipulation
   - Playback capabilities

3. **NAudio** (v2.2.1)
   - Audio library for .NET
   - Audio playback support
   - MIDI device interaction

4. **Microsoft.Extensions.DependencyInjection** (v8.0.0)
   - Dependency injection container
   - Service lifetime management

### Target Framework

- **.NET 6.0 (net6.0-windows)**
- Windows-only (WPF requirement)

## Design Decisions

### Why MVVM?

1. **Separation of Concerns**: Clear boundaries between UI, logic, and data
2. **Testability**: ViewModels can be unit tested without UI
3. **Maintainability**: Changes to UI don't affect business logic
4. **WPF Best Practice**: Natural fit for WPF's data binding

### Why WPF?

1. **Rich UI Capabilities**: Advanced graphics for fretboard visualization
2. **Data Binding**: Powerful binding system for MVVM
3. **Desktop Performance**: Native Windows performance
4. **XAML**: Declarative UI definition

### Mock vs Real MIDI Parsing

**Current State**: Mock data for demonstration
**Future**: Real MIDI parsing using DryWetMidi

**Rationale**:
- Mock data shows the architecture clearly
- Easy to test without MIDI files
- Production code would simply replace mock with real parser

### Playback Simulation

**Current**: DispatcherTimer with fixed tick increment
**Future**: Sync with actual MIDI playback timing

**Improvements Needed**:
- Read actual tempo from MIDI
- Calculate ticks per millisecond from BPM
- Synchronize with audio playback
- Handle tempo changes mid-song

### Tab Mapping Strategy

**Current**: Lowest fret preference
**Enhancements Possible**:
- Position-based fingering
- String preference for melodic lines
- Chord optimization
- Alternate fingerings
- Difficulty level adjustment

## Extension Points

### Adding New Features

1. **Custom Tunings**: Extend `TabMapper` with tuning presets
2. **Export to PDF**: Add export service for tablature
3. **Real Audio Playback**: Integrate NAudio for actual MIDI playback
4. **Recording**: Add MIDI input recording
5. **Chord Detection**: Identify and label chord patterns
6. **Practice Modes**: Loop sections, slow down playback

### Plugin Architecture

Future versions could support:
- Custom visualization themes
- Alternative notation systems
- Effect processing
- Third-party integrations

## Performance Considerations

### Current Optimizations

1. **Async Loading**: MIDI parsing is async to avoid UI freeze
2. **Observable Collections**: Efficient UI updates
3. **Dispatcher Invocation**: UI updates on correct thread

### Potential Improvements

1. **Virtualization**: For large MIDI files with many notes
2. **Caching**: Cache calculated positions
3. **Lazy Loading**: Load notes on-demand
4. **Background Processing**: Parse in background thread

## Security Considerations

1. **File Validation**: Validate MIDI files before parsing
2. **Exception Handling**: Graceful handling of malformed files
3. **Resource Limits**: Prevent loading extremely large files
4. **Input Sanitization**: Validate user-provided paths

## Testing Strategy

### Unit Tests

- Test ViewModels in isolation
- Test Services with mock data
- Test Utilities for calculations
- Test Models for data integrity

### Integration Tests

- Test ViewModel + Service integration
- Test MIDI parsing with real files
- Test tab mapping accuracy

### UI Tests

- Manual testing for visual correctness
- Screenshot comparison for regression

## Future Architecture

### Potential Evolutions

> **Note:** These are conceptual ideas for discussion and long-term consideration, not committed roadmap items. Actual implementation would depend on project direction, community feedback, and resource availability.

1. **Microservices**: Separate parsing service
2. **Cloud Storage**: Save/load from cloud
3. **Real-time Collaboration**: Multi-user editing
4. **Mobile Companion**: Tablet/phone support
5. **Web Version**: Blazor-based web app

---

For more details on specific components, see the [Code Guide](docs/CODE_GUIDE.md) or the source code comments.
