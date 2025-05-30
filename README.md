# MIDI to Guitar Tabulature Visualizer

This Windows application converts MIDI files into guitar tablature and visually displays the notes in real time on a virtual guitar neck.

## Features

- Load and parse MIDI files
- Map MIDI notes to guitar strings/frets (tab)
- Visualize notes on an interactive guitar neck
- Playback sync between MIDI and visualization
- User-friendly UI for loading files, playback, and settings

## Tech Stack

- **Language:** C# (.NET, WPF)
- **MIDI Parsing:** [NAudio](https://github.com/naudio/NAudio) and/or [DryWetMIDI](https://github.com/melanchall/drywetmidi)
- **Graphics/UI:** WPF Canvas

- MainViewModel loads a MIDI file, parses it, maps notes to tab, and exposes TabNotes (and MidiNotes) as observable collections.
The UI (MainWindow.xaml) binds to MainViewModel and shows a button to load MIDI, the file path, a fretboard visualization (GuitarNeckView), and a data grid of MIDI notes.
GuitarNeckView visualizes strings, frets, and tab notes dynamically, updating when the MIDI or tab data changes.

## Repository Structure

```
/MidiGuitarTabApp
│
├── MidiGuitarTabApp.sln           # Solution file
├── README.md
└── src/
    ├── App.xaml                   # Application entry point
    ├── MainWindow.xaml            # Main UI window
    ├── MainWindow.xaml.cs
    ├── Models/
    │   └── MidiNote.cs            # MIDI note and tab mapping model
    ├── Services/
    │   ├── MidiParser.cs          # Handles MIDI file parsing
    │   └── TabMapper.cs           # Maps MIDI notes to guitar tabs
    ├── ViewModels/
    │   └── MainViewModel.cs       # Main MVVM logic
    ├── Views/
    │   ├── GuitarNeckView.xaml    # Guitar neck visualization UI
    │   └── GuitarNeckView.xaml.cs
    └── Utils/
        └── FretboardHelper.cs     # Helper functions for fretboard logic
```

## Getting Started

1. Clone the repository.
2. Open `MidiGuitarTabApp.sln` in Visual Studio.
3. Restore NuGet packages (NAudio or DryWetMIDI).
4. Build and run!

## Roadmap

- [ ] MIDI file input and parsing
- [ ] Guitar neck rendering
- [ ] Tab mapping algorithm
- [ ] Playback and visualization sync
- [ ] Advanced features (real-time MIDI input, export tab, etc.)
      

## License

MIT
