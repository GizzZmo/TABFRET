# Assets Directory

This directory contains screenshots and visual assets for the TABFRET application.

## Required Screenshots

1. **screenshot.png** - Main application window showing the MIDI to guitar tab conversion
2. **fretboard_demo.png** - Fretboard visualization demo

## How to Generate Screenshots

To generate screenshots of the TABFRET application:

1. Build and run the application on Windows:
   ```powershell
   dotnet run --project src/TABFRET.csproj
   ```

2. Load a sample MIDI file

3. Take screenshots:
   - **Main Window**: Capture the full application window showing:
     - Toolbar with controls (Open MIDI, Play, Pause, etc.)
     - Guitar neck visualization
     - MIDI notes data grid
     - Status bar
   
   - **Fretboard Demo**: Capture the guitar neck visualization showing highlighted notes during playback

4. Save screenshots in this directory with the appropriate names

## Screenshot Specifications

- Format: PNG
- Recommended resolution: At least 1024x768 for main screenshot
- Use high DPI settings for clarity
- Include realistic MIDI data for demonstration

## Placeholder Images

Until actual screenshots are generated, placeholder images should indicate:
- Application name (TABFRET)
- Brief description of what the screenshot will show
