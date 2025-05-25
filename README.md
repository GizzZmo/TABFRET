# MidiGuitarTab

A Windows desktop application that displays MIDI files as guitar tablature and visualizes notes on a virtual guitar neck.

## Features

- Load and parse MIDI files
- Map MIDI notes to guitar strings and frets (tablature)
- Visualize notes in real time on a virtual guitar neck
- Playback synchronization between MIDI and visualization

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022+ (or JetBrains Rider)
- [NAudio](https://github.com/naudio/NAudio) or [DryWetMIDI](https://github.com/melanchall/drywetmidi) (see below)

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/YOUR-USERNAME/MidiGuitarTab.git
    cd MidiGuitarTab
    ```

2. Open `MidiGuitarTab.sln` in Visual Studio.

3. Restore NuGet packages and build the solution.

### Usage

- Launch the app.
- Click "Load MIDI File" to select and parse a MIDI file.
- Watch the notes appear as tablature and on the virtual guitar neck during playback.

## Project Structure

- `Models/` - Data models (e.g., MidiNote)
- `Services/` - MIDI parsing and mapping logic
- `ViewModels/` - MVVM logic
- `Views/` - UI components
- `Resources/` - Images and static assets

## Roadmap

- [ ] Real-time MIDI input
- [ ] Custom tunings and capo support
- [ ] Export tablature

## License

MIT

## Acknowledgements

- [NAudio](https://github.com/naudio/NAudio)
- [Melanchall DryWetMIDI](https://github.com/melanchall/drywetmidi)


/MidiGuitarTab
├── MidiGuitarTab.sln
├── MidiGuitarTab/
│   ├── App.xaml
│   ├── App.xaml.cs
│   ├── MainWindow.xaml
│   ├── MainWindow.xaml.cs
│   ├── Models/
│   │   └── MidiNote.cs
│   ├── Services/
│   │   └── MidiParser.cs
│   ├── ViewModels/
│   │   └── MainViewModel.cs
│   ├── Views/
│   │   └── GuitarNeckView.xaml
│   │   └── GuitarNeckView.xaml.cs
│   └── Resources/
│       └── guitar_neck.png
├── README.md
├── .gitignore
