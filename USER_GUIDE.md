# TABFRET User Guide

Welcome to TABFRET! This guide will help you get started with converting MIDI files to guitar tablature and visualizing them on a virtual fretboard.

## Table of Contents

- [Introduction](#introduction)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [User Interface Overview](#user-interface-overview)
- [Loading MIDI Files](#loading-midi-files)
- [Playback Controls](#playback-controls)
- [Understanding the Display](#understanding-the-display)
- [Tips and Tricks](#tips-and-tricks)
- [Frequently Asked Questions](#frequently-asked-questions)
- [Troubleshooting](#troubleshooting)

## Introduction

### What is TABFRET?

TABFRET (MIDI Guitar Tab) is a Windows desktop application that helps guitarists learn and practice songs from MIDI files. It converts MIDI notes into guitar tablature and displays them on an interactive virtual guitar neck.

### Key Features

- **MIDI to Guitar Tab Conversion**: Automatically converts MIDI files to playable guitar tablature
- **Visual Fretboard**: See exactly where to place your fingers on the guitar neck
- **Real-time Playback**: Watch notes highlight as the song plays
- **Standard Tuning Support**: Works with standard guitar tuning (EADGBe)
- **Easy-to-use Interface**: Simple controls for loading and playing files

### Who is TABFRET For?

- **Guitar Learners**: Learn new songs visually
- **Music Teachers**: Demonstrate finger positions to students
- **Transcribers**: Convert MIDI files to guitar notation
- **Hobbyists**: Explore MIDI files in guitar context

## Installation

### System Requirements

- **Operating System**: Windows 10 (version 1809 or later) or Windows 11
- **Processor**: 1 GHz or faster
- **RAM**: 2 GB minimum, 4 GB recommended
- **Disk Space**: 500 MB free space
- **.NET Runtime**: .NET 6.0 Desktop Runtime or later

### Download TABFRET

1. Visit the [Releases page](https://github.com/GizzZmo/TABFRET/releases)
2. Download the latest `TABFRET-v*.*.*.zip` file
3. Extract the ZIP file to a folder of your choice (e.g., `C:\Program Files\TABFRET`)

### Install .NET Runtime (if needed)

If you don't have .NET 6.0 installed:

1. Download [.NET 6.0 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/6.0)
2. Run the installer
3. Follow the installation wizard

### First Launch

1. Navigate to the folder where you extracted TABFRET
2. Double-click `TABFRET.exe`
3. If Windows Defender SmartScreen appears:
   - Click "More info"
   - Click "Run anyway"
4. The application should launch successfully

## Quick Start

### Your First MIDI File

1. **Launch TABFRET**
   - Double-click `TABFRET.exe`

2. **Load a MIDI File**
   - Click the "Load MIDI" button
   - Browse to a MIDI file on your computer
   - Click "Open"

3. **View the Tablature**
   - The guitar neck will display notes from the MIDI file
   - Each circle shows where to press on the fretboard

4. **Play the File**
   - Click the "Play" button
   - Watch as notes highlight in real-time
   - Notes turn red when they should be played

5. **Control Playback**
   - **Pause**: Temporarily stop playback
   - **Stop**: Stop and return to beginning

## User Interface Overview

### Main Window Layout

```
┌─────────────────────────────────────────────────────┐
│  [Load MIDI]  [Play]  [Pause]  [Stop]               │  ← Control Bar
├─────────────────────────────────────────────────────┤
│                                                       │
│          🎸 Guitar Fretboard Visualization 🎸        │
│                                                       │  ← Fretboard Display
│  E |─●─────────────5───────●───9─────────12─────●─│ │
│  B |─────────────────────────────────────────────── │ │
│  G |───────●─────────────────────────────────────── │ │
│  D |─────────────────────●───────────────────────── │ │
│  A |─────────────────────────────────────────────── │ │
│  E |─────────────────────────────────────────────── │ │
│                                                       │
├─────────────────────────────────────────────────────┤
│  Status: Playing...    File: MySong.mid             │  ← Status Bar
└─────────────────────────────────────────────────────┘
```

### Control Buttons

#### Load MIDI (Green Button)
- Opens file browser to select a MIDI file
- Supported formats: `.mid`, `.midi`
- Loads and converts file to guitar tablature

#### Play (Blue Button)
- Starts playback from current position
- Highlights notes in real-time
- Enabled only when a MIDI file is loaded

#### Pause (Yellow Button)
- Pauses playback at current position
- Click Play to resume
- Enabled only during playback

#### Stop (Red Button)
- Stops playback
- Returns to beginning of song
- Enabled when a file is loaded

### Fretboard Display

#### String Lines (Horizontal)
From top to bottom:
1. **Thin E** (High E, 1st string)
2. **B** (2nd string)
3. **G** (3rd string)
4. **D** (4th string)
5. **A** (5th string)
6. **Thick E** (Low E, 6th string)

#### Fret Lines (Vertical)
- **Thick line on left**: The nut (fret 0)
- **Thin lines**: Individual frets (1-15)
- **Numbers above frets**: Fret position markers

#### Fret Markers (Dots)
Standard guitar fret markers at positions:
- 3, 5, 7, 9: Single dot
- 12: Double dots
- 15: Single dot

#### Note Circles
- **Blue circles**: Notes to be played
- **Red circles**: Currently playing notes
- **Number inside**: Fret number to press

### Status Bar

Shows:
- **Left side**: Current status (Ready, Loading, Playing, Paused, Stopped)
- **Right side**: Loaded MIDI file name and path

## Loading MIDI Files

### Supported MIDI Files

TABFRET works with standard MIDI files:
- **Format**: MIDI Type 0 or Type 1
- **Extension**: `.mid` or `.midi`
- **Content**: Files with note data (not just drum tracks)

### Best MIDI Files for Guitar

For best results, use MIDI files that:
- Were created for guitar or are guitar-friendly
- Don't have too many simultaneous notes
- Stay within guitar range (MIDI notes 40-88)
- Have clear melodic lines

### Finding MIDI Files

**Free MIDI Resources:**
- [FreeMIDI.org](https://freemidi.org)
- [Midiworld.com](http://www.midiworld.com)
- [BitMidi](https://bitmidi.com)
- Guitar Pro files exported to MIDI

**Creating Your Own:**
- Use DAW software (FL Studio, Ableton, etc.)
- Convert from Guitar Pro/PowerTab
- Record from MIDI keyboard/guitar

### Load Process

1. **Click "Load MIDI"**
2. **Navigate to your MIDI file**
   - Default location: Documents, Downloads, or Music folder
   - Can browse to any location on your computer

3. **Select the file and click "Open"**
   - Status bar shows "Loading MIDI file..."
   - Application parses the file
   - Converts MIDI notes to guitar tablature

4. **View Results**
   - Status bar shows: "MIDI file loaded: [filename] (X notes)"
   - Fretboard displays all notes
   - Ready for playback

### What Happens During Loading

1. **File Reading**: MIDI file is parsed
2. **Note Extraction**: All note events are identified
3. **Tab Mapping**: Each MIDI note is assigned to a guitar string and fret
4. **Visualization**: Notes are drawn on the fretboard

## Playback Controls

### Starting Playback

1. Load a MIDI file
2. Click the **Play** button (blue)
3. Watch the visualization

**What you'll see:**
- Notes highlight in red as they play
- Playback progresses from left to right (by time)
- Status bar shows "Playing..."

### Pausing Playback

- Click **Pause** button (yellow) during playback
- Playback stops at current position
- Click **Play** to continue from where you left off

**Use Cases:**
- Study a particular section
- Take a break
- Match your practice tempo

### Stopping Playback

- Click **Stop** button (red)
- Playback stops and returns to beginning
- All note highlights are cleared

**When to use Stop:**
- Finished listening
- Want to start over
- Loading a new file

### Playback Speed

**Current Version**: Fixed playback speed

**Future Version**: Will include:
- Speed control slider
- Slow down for practice
- Loop specific sections

## Understanding the Display

### Reading the Fretboard

#### Note Positions

Each blue circle represents a note you should play:

```
String  │  Fret Numbers →
E (1st) │  ─●─────5───────●─
        │   ↑           ↑
      Open          5th fret
      string
```

#### Fret Numbers

The number inside each circle tells you which fret to press:
- **0**: Play the open string (don't press any fret)
- **1**: Press behind the 1st fret
- **2**: Press behind the 2nd fret
- ...and so on

#### Multiple Notes

**Chord (vertical alignment)**:
```
E │ ─●─  ← Play all these
B │ ─●─     notes together
G │ ─●─     (a chord)
D │ ───
A │ ───
E │ ───
```

**Sequence (spread out)**:
```
E │ ─●─────────●─────────●──  ← Play these
B │ ───────────────────────     notes one
G │ ───────────────────────     after another
```

### Color Coding

- **Light Gray Background**: The fretboard
- **Black Lines**: Strings and frets
- **White Dots**: Fret position markers
- **Blue Circles**: Notes in the song
- **Red Circles**: Currently playing note
- **Dark Blue Border**: Note outline

### Reading Tab Notation (Traditional)

If you're familiar with traditional tab, here's the equivalent:

**TABFRET Display:**
```
G │ ───●───
    (fret 3)
```

**Traditional Tab:**
```
G|---3---
```

Both mean: Play the 3rd fret on the G string.

## Tips and Tricks

### Learning a Song

1. **First Listen**: Play through once to hear the song
2. **Visual Study**: Pause and study note positions
3. **Section Practice**: Stop and replay difficult sections
4. **Slow Practice**: (Future: use speed control)
5. **Full Tempo**: Play along when comfortable

### Understanding Note Mapping

TABFRET chooses the **lowest fret** position for each note:

**Example**: MIDI note E4 (64) could be:
- 5th fret on B string, OR
- Open high E string ✓ (chosen)

**Why?**: Generally easier to play lower positions, but you can play the same note higher up the neck if you prefer.

### Chord Recognition

When multiple notes are at the same time (vertically aligned):
- Look up the chord shape
- Practice the chord form separately
- Common chords will become familiar

### Practice Tips

1. **Start Slow**: Don't rush
2. **Muscle Memory**: Repeat sections multiple times
3. **Use a Metronome**: Keep steady time
4. **Record Yourself**: Check your progress
5. **Take Breaks**: Avoid hand fatigue

### File Organization

Create a folder structure:
```
My MIDI Files/
├── Songs/
│   ├── Rock/
│   ├── Blues/
│   └── Classical/
└── Practice/
    ├── Exercises/
    └── Scales/
```

## Frequently Asked Questions

### General Questions

**Q: Does TABFRET work on Mac or Linux?**
A: Currently, TABFRET is Windows-only due to WPF. Cross-platform support may come in future versions.

**Q: Is TABFRET free?**
A: Yes, TABFRET is open source and free to use.

**Q: Can I use TABFRET commercially?**
A: Check the LICENSE file for specific terms.

### File Questions

**Q: Why won't my MIDI file load?**
A: The file may be:
- Corrupted or incomplete
- Not a valid MIDI format
- Too large
Try a different MIDI file to test.

**Q: Can I load MP3 or WAV files?**
A: No, TABFRET only works with MIDI files, which contain note data. Audio files would need to be converted to MIDI first using other software.

**Q: My MIDI file loads but shows no notes. Why?**
A: The MIDI file might:
- Only contain drum tracks
- Have notes outside guitar range
- Use continuous controller data instead of notes

### Display Questions

**Q: Why are some notes on high frets?**
A: The MIDI file contains notes that require higher frets. You can play them lower on a different string if you know the equivalent positions.

**Q: Can I change the tuning?**
A: Current version uses standard tuning (EADGBe). Support for alternate tunings is planned.

**Q: The notes overlap and I can't see them all. What do I do?**
A: Very complex MIDI files may be hard to visualize. Try:
- Loading a simpler file
- Extracting individual tracks from your MIDI file

### Playback Questions

**Q: Can I hear the sound during playback?**
A: Current version has visual-only playback. Audio playback is planned for a future release.

**Q: Can I slow down the playback?**
A: Speed control is planned but not yet available.

**Q: Can I loop a section?**
A: Section looping is planned for a future release.

## Troubleshooting

### Installation Issues

#### "Application failed to start"
**Cause**: Missing .NET Runtime
**Solution**: 
1. Download .NET 6.0 Desktop Runtime
2. Install and restart TABFRET

#### "Windows protected your PC"
**Cause**: Windows SmartScreen
**Solution**:
1. Click "More info"
2. Click "Run anyway"
3. TABFRET is safe; SmartScreen is just cautious with new applications

### Loading Issues

#### "Error loading MIDI file"
**Troubleshooting steps**:
1. Verify the file is a valid MIDI file
2. Try opening in another MIDI player
3. Try a different MIDI file
4. Check file isn't corrupted
5. Ensure file path doesn't contain special characters

#### "No notes found in MIDI file"
**Solutions**:
- Try a different MIDI file
- Check the file contains note events (not just controllers)
- Ensure notes are in guitar range

### Performance Issues

#### "Application is slow or laggy"
**Solutions**:
- Close other applications
- Try a smaller/simpler MIDI file
- Restart TABFRET
- Restart your computer

#### "Playback is choppy"
**Solutions**:
- Close background applications
- Check CPU usage in Task Manager
- Try a simpler MIDI file

### Display Issues

#### "Fretboard not showing"
**Solutions**:
- Resize the window
- Restart TABFRET
- Check display scaling settings

#### "Notes appear in wrong positions"
This shouldn't happen. If it does:
- Report as a bug on GitHub
- Include the MIDI file if possible

### Getting Help

If you encounter issues not covered here:

1. **Check GitHub Issues**: [TABFRET Issues](https://github.com/GizzZmo/TABFRET/issues)
2. **Search for Similar Problems**: Others may have found solutions
3. **Report New Issue**: Provide:
   - Windows version
   - TABFRET version
   - Steps to reproduce
   - Screenshots if applicable
   - MIDI file (if safe to share)

## Keyboard Shortcuts

**Current Version**: Mouse/click only

**Future Versions** (planned):
- `Space`: Play/Pause
- `Ctrl+O`: Open MIDI file
- `Ctrl+Q`: Quit
- `←/→`: Seek backward/forward
- `↑/↓`: Speed up/slow down

## Tips for Best Experience

1. **Use Quality MIDI Files**: Better source = better results
2. **Keep It Simple**: Start with simpler songs
3. **Practice Regularly**: Use TABFRET for daily practice
4. **Learn Theory**: Understanding music helps interpret tablature
5. **Combine with Other Tools**: Use with metronome, tuner, etc.

## What's Next?

Check out these resources to enhance your TABFRET experience:

- **[GitHub Repository](https://github.com/GizzZmo/TABFRET)**: Latest updates and releases
- **[Contributing Guide](CONTRIBUTING.md)**: Help improve TABFRET
- **[Issue Tracker](https://github.com/GizzZmo/TABFRET/issues)**: Report bugs or request features

Happy playing! 🎸

---

*This user guide is for TABFRET v1.x. For older versions or development builds, functionality may differ.*
