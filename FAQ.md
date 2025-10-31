# Frequently Asked Questions (FAQ)

Common questions and answers about TABFRET.

## Table of Contents

- [General Questions](#general-questions)
- [Installation & Setup](#installation--setup)
- [Using TABFRET](#using-tabfret)
- [MIDI Files](#midi-files)
- [Features & Functionality](#features--functionality)
- [Troubleshooting](#troubleshooting)
- [Development & Contributing](#development--contributing)

---

## General Questions

### What is TABFRET?

TABFRET (MIDI Guitar Tab) is a Windows desktop application that converts MIDI files into guitar tablature and visualizes them on a virtual guitar fretboard in real-time.

### Is TABFRET free?

Yes! TABFRET is completely free and open-source under the MIT License.

### What platforms does TABFRET support?

Currently, TABFRET only runs on Windows (Windows 10 version 1809 or later, and Windows 11) because it uses WPF (Windows Presentation Foundation).

**Cross-platform support** is being considered for future versions, possibly using Avalonia UI.

### Who created TABFRET?

TABFRET was created and is maintained by [GizzZmo](https://github.com/GizzZmo) with contributions from the open-source community.

### Can I use TABFRET for commercial purposes?

Yes, the MIT License allows both personal and commercial use. See the [LICENSE](LICENSE) file for details.

---

## Installation & Setup

### What do I need to run TABFRET?

- **Operating System**: Windows 10 (1809+) or Windows 11
- **.NET Runtime**: .NET 6.0 Desktop Runtime or later
- **RAM**: 2 GB minimum (4 GB recommended)
- **Disk Space**: 500 MB

### How do I install TABFRET?

1. Download the latest release ZIP from [GitHub Releases](https://github.com/GizzZmo/TABFRET/releases)
2. Extract the ZIP to a folder
3. Install [.NET 6.0 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/6.0) if needed
4. Run `TABFRET.exe`

See the [User Guide](USER_GUIDE.md#installation) for detailed instructions.

### Do I need Visual Studio to use TABFRET?

No! Visual Studio is only needed if you want to modify the source code. Regular users just need the .NET Runtime.

### Windows says the app is unrecognized. Is it safe?

Yes, TABFRET is safe. This warning appears because the application isn't digitally signed with a code signing certificate. Click "More info" then "Run anyway" to proceed.

---

## Using TABFRET

### How do I load a MIDI file?

1. Click the green "Load MIDI" button
2. Browse to your MIDI file (.mid or .midi)
3. Click "Open"
4. TABFRET will parse and display the file

### What are the controls?

| Button | Function |
|--------|----------|
| Load MIDI (Green) | Open a MIDI file |
| Play (Blue) | Start/resume playback |
| Pause (Yellow) | Pause playback |
| Stop (Red) | Stop and return to beginning |

### How do I read the fretboard?

- **Horizontal lines** = Guitar strings (6 strings, High E at top)
- **Vertical lines** = Frets (0 = nut, numbers mark fret positions)
- **Blue circles** = Notes to play
- **Red circles** = Currently playing notes
- **Number in circle** = Which fret to press (0 = open string)
- **White dots** = Fret markers (like on a real guitar)

See the [User Guide](USER_GUIDE.md#understanding-the-display) for detailed explanation.

### Can I slow down playback?

Not in the current version. **Speed control** is planned for version 2.0.

### Can I hear audio playback?

Not yet. The current version is visual-only. **Audio playback** is planned for version 2.0.

### Can I export the tablature?

Export functionality is not yet available but is planned for a future release. You can take screenshots for now.

---

## MIDI Files

### Where can I get MIDI files?

Free MIDI file resources:
- [FreeMIDI.org](https://freemidi.org)
- [BitMidi](https://bitmidi.com)
- [Midiworld.com](http://www.midiworld.com)

You can also create MIDI files using:
- DAW software (FL Studio, Ableton, etc.)
- Guitar Pro (export to MIDI)
- MIDI keyboards
- Online MIDI editors

### What MIDI file types are supported?

TABFRET supports standard MIDI files:
- **Format**: Type 0 (single track) and Type 1 (multiple tracks)
- **Extension**: `.mid` or `.midi`

### Why does my MIDI file load with no notes?

Possible reasons:
1. **Drum-only track**: Drums don't have pitch information
2. **Notes outside guitar range**: Very high or low notes may not display
3. **Empty file**: File only contains metadata, no actual notes
4. **Corrupted file**: Try opening in another MIDI player to verify

### Can TABFRET handle multiple instrument MIDI files?

Yes, TABFRET will parse all notes from the file. However, complex multi-instrument files may be cluttered. For best results, use:
- Single-instrument tracks
- Guitar-specific MIDI files
- Simplified arrangements

### Why do the notes look wrong?

TABFRET uses an algorithm that:
- Maps notes to **lowest available fret**
- Uses **standard tuning** (EADGBe)
- Chooses one valid position per note

The same note can be played in multiple positions on guitar. TABFRET picks one, but you're free to play it differently.

### Can I use non-guitar MIDI files?

Yes, but results vary. TABFRET converts any melodic MIDI notes to guitar positions. Piano, violin, or other instrument MIDI files will work, but:
- May use notes outside comfortable guitar range
- May have too many simultaneous notes
- May not be ergonomic for guitar playing

---

## Features & Functionality

### Does TABFRET support alternate tunings?

Not currently. The app uses standard tuning (EADGBe) only. **Alternate tuning support** is planned for version 2.0.

### Can I change the colors?

Color customization isn't available in the current UI. This feature is planned for a future release.

**Future Feature**: You will be able to modify settings.json:
```json
{
  "VisualSettings": {
    "NoteHighlightColor": "#FFFF0000",  // Red
    "FretboardBackgroundColor": "#FFD3D3D3"  // Light gray
  }
}
```

### Can I select specific tracks from a multi-track MIDI?

Not yet. TABFRET currently processes all tracks together. **Track selection** is planned for a future release.

### Can I loop a section?

Section looping is not available yet but is planned for version 2.0.

### Can TABFRET detect chords?

Automatic chord detection and labeling is not currently available but is planned for a future enhancement.

### Does TABFRET support bass guitar?

Yes! MIDI notes that map to the lower range will display on the appropriate strings. However, TABFRET doesn't have a specific bass guitar mode yet.

### Can I print the tablature?

There's no built-in print function. For now, you can:
1. Take a screenshot (Windows+Shift+S)
2. Paste into a document
3. Print from there

**PDF export** is planned for a future release.

---

## Troubleshooting

### The application won't start

**Solutions:**
1. Install [.NET 6.0 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/6.0)
2. Make sure you extracted the ZIP (don't run from inside the ZIP)
3. Check Windows Event Viewer for error details
4. Try running as Administrator

See [TROUBLESHOOTING.md](TROUBLESHOOTING.md#installation-issues) for more help.

### MIDI file won't load

**Check:**
1. Is it a valid MIDI file? Try in another MIDI player
2. Is the file path too long? (< 260 characters)
3. Do you have read permissions for the file?
4. Is the file corrupted? Try a different file

### The fretboard is blank

**Possible causes:**
1. No MIDI file loaded yet
2. MIDI file has no notes
3. Window needs to be resized
4. Graphics driver issue

### Playback is choppy or slow

**Solutions:**
1. Close other applications
2. Try a smaller/simpler MIDI file
3. Check CPU usage in Task Manager
4. Update graphics drivers

### Notes appear in unexpected positions

This is usually normal - TABFRET picks the lowest fret position. You can play the same note in different positions if you prefer.

### More troubleshooting help?

See the complete [TROUBLESHOOTING.md](TROUBLESHOOTING.md) guide.

---

## Development & Contributing

### Can I contribute to TABFRET?

Absolutely! Contributions are welcome. See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

### How do I build TABFRET from source?

```bash
git clone https://github.com/GizzZmo/TABFRET.git
cd TABFRET
dotnet restore TABFRET.sln
dotnet build TABFRET.sln --configuration Release
dotnet run --project src/TABFRET.csproj
```

See [DEVELOPMENT.md](DEVELOPMENT.md) for detailed instructions.

### What technologies does TABFRET use?

- **Framework**: WPF (Windows Presentation Foundation)
- **Language**: C# (.NET 6.0)
- **Pattern**: MVVM (Model-View-ViewModel)
- **Libraries**: 
  - Melanchall.DryWetMidi (MIDI parsing)
  - NAudio (Audio support)

See [ARCHITECTURE.md](ARCHITECTURE.md) for details.

### I found a bug. How do I report it?

1. Check [existing issues](https://github.com/GizzZmo/TABFRET/issues) first
2. Create a [new issue](https://github.com/GizzZmo/TABFRET/issues/new?labels=bug)
3. Include:
   - Windows version
   - TABFRET version
   - Steps to reproduce
   - Expected vs actual behavior
   - Screenshots if applicable

### I have a feature idea. How do I suggest it?

Create a [feature request](https://github.com/GizzZmo/TABFRET/issues/new?labels=enhancement) on GitHub with:
- Clear description of the feature
- Use case (why it's valuable)
- How it might work
- Any examples or mockups

### What's the development roadmap?

**Version 2.0 Plans:**
- Audio playback
- Speed control
- Section looping
- Alternate tunings

**Future Ideas:**
- PDF export
- Multi-track support
- Chord recognition
- Practice modes
- Mobile companion app

See the [README Roadmap](README.md#roadmap) for more details.

### Can I use TABFRET code in my own project?

Yes! TABFRET is MIT licensed, which allows:
- ✅ Using the code in your own projects
- ✅ Modifying the code
- ✅ Distributing your modified version
- ✅ Commercial use

You must include the original copyright notice and license.

---

## More Questions?

### Still need help?

- 📖 **[User Guide](USER_GUIDE.md)**: Comprehensive user documentation
- 🐛 **[Troubleshooting](TROUBLESHOOTING.md)**: Detailed troubleshooting guide
- 💬 **[Discussions](https://github.com/GizzZmo/TABFRET/discussions)**: Ask questions
- 🐛 **[Issues](https://github.com/GizzZmo/TABFRET/issues)**: Report bugs
- 👨‍💻 **[Development Guide](DEVELOPMENT.md)**: Developer documentation

### Can't find your question?

Ask in [GitHub Discussions](https://github.com/GizzZmo/TABFRET/discussions) - we're happy to help!

---

*Last updated: 2025*
