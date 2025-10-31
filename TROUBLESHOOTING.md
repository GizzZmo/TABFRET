# Troubleshooting Guide

This guide helps you resolve common issues when using or developing TABFRET.

## Table of Contents

- [Installation Issues](#installation-issues)
- [Runtime Issues](#runtime-issues)
- [MIDI File Issues](#midi-file-issues)
- [Display Issues](#display-issues)
- [Performance Issues](#performance-issues)
- [Development Issues](#development-issues)
- [Getting Additional Help](#getting-additional-help)

---

## Installation Issues

### "Application failed to start" or "Missing .NET Runtime"

**Symptoms:**
- Error message about missing .NET Framework or Runtime
- Application doesn't launch at all
- Error: "This application requires .NET Desktop Runtime 6.0"

**Solutions:**

1. **Install .NET 6.0 Desktop Runtime**
   ```
   Download from: https://dotnet.microsoft.com/download/dotnet/6.0
   Select: ".NET Desktop Runtime 6.0.x" for Windows x64
   ```

2. **Verify installation**
   ```bash
   # Open Command Prompt and run:
   dotnet --list-runtimes
   
   # You should see a line like:
   # Microsoft.WindowsDesktop.App 6.0.x [C:\Program Files\dotnet\shared\...]
   ```

3. **Restart your computer** after installing .NET Runtime

---

### "Windows protected your PC" (SmartScreen Warning)

**Symptoms:**
- Blue window with "Windows protected your PC"
- "Microsoft Defender SmartScreen prevented an unrecognized app from starting"

**Solution:**

This is normal for unsigned applications. TABFRET is safe to run.

1. Click **"More info"** link
2. Click **"Run anyway"** button
3. Application will start normally

**Why this happens:**
- Application is not digitally signed with a code signing certificate
- Windows is being cautious with unfamiliar executables

**Future:** We may sign the application in future releases to avoid this warning.

---

### "Cannot find TABFRET.exe"

**Symptoms:**
- Extracted folder doesn't contain TABFRET.exe
- Only see DLL files

**Solutions:**

1. **Re-download the release package**
   - Go to [Releases page](https://github.com/GizzZmo/TABFRET/releases)
   - Download the complete ZIP file (not source code)
   - Look for `TABFRET-v*.*.*.zip`

2. **Extract all files**
   - Right-click the ZIP file
   - Select "Extract All..."
   - Don't just open the ZIP; fully extract it

3. **Check Windows doesn't block the file**
   - Right-click `TABFRET.exe`
   - Select "Properties"
   - If you see "Unblock" checkbox at bottom, check it
   - Click "Apply" and "OK"

---

### Application Starts Then Immediately Closes

**Symptoms:**
- Window flashes briefly then disappears
- No error message shown

**Solutions:**

1. **Run from Command Prompt to see errors**
   ```bash
   cd "C:\Path\To\TABFRET"
   TABFRET.exe
   ```
   This will show any error messages

2. **Check Event Viewer**
   - Press `Win+R`, type `eventvwr.msc`
   - Navigate to: Windows Logs → Application
   - Look for recent errors from TABFRET

3. **Verify all files are present**
   - Make sure all DLL files are in the same folder as TABFRET.exe
   - Re-extract the ZIP if files are missing

---

## Runtime Issues

### "Error Loading MIDI File"

**Symptoms:**
- Error dialog when trying to load a MIDI file
- Message: "Error loading MIDI: ..."

**Solutions:**

1. **Verify file is a valid MIDI file**
   ```
   - File extension should be .mid or .midi
   - Try opening in another MIDI player (Windows Media Player, VLC)
   - File size should be reasonable (not 0 bytes)
   ```

2. **Try a different MIDI file**
   - Download a test MIDI file from [FreeMIDI.org](https://freemidi.org)
   - If test file works, original file may be corrupted

3. **Check file path**
   - Avoid special characters in folder names
   - Don't use network drives (copy to local drive)
   - Path length should be < 260 characters

4. **File permissions**
   - Ensure you have read permissions for the file
   - Try copying file to Documents folder

---

### "No Notes Found in MIDI File"

**Symptoms:**
- MIDI file loads successfully
- Fretboard remains empty
- Status shows "0 notes"

**Possible Causes & Solutions:**

1. **File contains only drums/percussion**
   - Solution: Use a MIDI file with melodic instruments
   - Drum tracks don't have pitch information needed for tabs

2. **Notes outside guitar range**
   - MIDI notes below 40 (E2) or above 88 (E6) may not display
   - Solution: Use guitar-appropriate MIDI files

3. **Empty or metadata-only file**
   - Some MIDI files only have tempo/time signature data
   - Solution: Try a different MIDI file

4. **All notes on muted tracks**
   - Solution: Future versions will have track selection

---

### Playback Doesn't Start

**Symptoms:**
- Click "Play" but nothing happens
- No notes highlight
- Status doesn't change to "Playing"

**Solutions:**

1. **Ensure MIDI file is loaded**
   - Load a MIDI file first using "Load MIDI" button
   - Status bar should show the filename

2. **Check for notes**
   - If file loaded with "0 notes", playback won't work
   - Try a different MIDI file

3. **Try Stop then Play**
   - Click "Stop" button
   - Wait a second
   - Click "Play" again

4. **Restart application**
   - Close TABFRET
   - Relaunch and try again

---

## MIDI File Issues

### Notes Appear in Wrong Positions

**Symptoms:**
- Notes don't match expected tab positions
- Unusual fret numbers (very high or very low)

**Understanding:**

TABFRET uses an algorithm to map MIDI notes to guitar positions:
- Chooses the **lowest fret** for each note
- Uses **standard tuning** (EADGBe)

**Solutions:**

1. **This might be correct**
   - The same note can be played on different strings
   - TABFRET picks one valid position
   - You can play it differently if you prefer

2. **Check MIDI file octave**
   - Some MIDI files may be transposed incorrectly
   - If all notes are too high/low, file may be in wrong octave

3. **Future feature**: Custom position preferences

---

### Too Many Notes Overlapping

**Symptoms:**
- Fretboard is cluttered
- Can't see individual notes
- Circles overlap each other

**Solutions:**

1. **Use simpler MIDI files**
   - Try files with single melody lines
   - Avoid complex orchestral arrangements

2. **Extract specific tracks**
   - Use MIDI editing software to isolate guitar track
   - Software: MuseScore, Reaper, etc.

3. **Future feature**: Track filtering and note filtering

---

### Wrong Tempo/Speed

**Symptoms:**
- Playback is too fast or too slow
- Doesn't match expected song tempo

**Current Limitation:**
- Current version uses fixed playback speed
- Not reading actual MIDI tempo (planned for v2.0)

**Workaround:**
- None currently available

**Future Release (v2.0):**
- Proper tempo reading from MIDI
- Speed control slider
- Tempo change support

---

## Display Issues

### Fretboard Not Showing

**Symptoms:**
- Main window shows but fretboard area is blank
- No strings or frets visible

**Solutions:**

1. **Resize the window**
   - Drag window corners to make it larger
   - Minimum size: 800x600 pixels

2. **Check display scaling**
   - Windows Settings → Display
   - If scaling > 100%, try reducing it
   - Or try a different monitor

3. **Update graphics drivers**
   - Especially for Intel integrated graphics
   - Download latest from manufacturer's website

4. **Restart application**

---

### Text or Controls Cut Off

**Symptoms:**
- Button text is truncated
- Status bar text not fully visible
- Window too small

**Solutions:**

1. **Maximize the window**
   - Click maximize button
   - Or double-click title bar

2. **Adjust display scaling**
   - Windows Settings → Display → Scale
   - Try 100% scaling

3. **Change screen resolution**
   - Right-click desktop → Display settings
   - Use at least 1024x768 resolution

---

### Blurry or Pixelated Display

**Symptoms:**
- Text appears blurry
- Graphics look pixelated
- Interface seems low quality

**Solutions:**

1. **Disable display scaling override**
   - Right-click TABFRET.exe
   - Properties → Compatibility
   - Click "Change high DPI settings"
   - Check "Override high DPI scaling behavior"
   - Select "Application" from dropdown
   - Click OK

2. **Adjust Windows scaling**
   - Try 100%, 125%, or 150% scaling
   - Some scales work better than others

---

## Performance Issues

### Application is Slow or Laggy

**Symptoms:**
- Interface responds slowly to clicks
- Playback is choppy
- General sluggishness

**Solutions:**

1. **Close other applications**
   - Free up CPU and RAM
   - Check Task Manager for resource hogs

2. **Try a smaller MIDI file**
   - Very large files (>10,000 notes) may be slow
   - Test with a simple file first

3. **Restart your computer**
   - Fresh start can resolve memory issues

4. **Check system requirements**
   - Minimum: Windows 10, 2GB RAM, 1GHz processor
   - Recommended: Windows 11, 4GB RAM, 2GHz processor

5. **Update Windows**
   - Windows Update may have performance improvements

---

### High CPU Usage

**Symptoms:**
- CPU usage stays high even when idle
- Fan runs constantly
- Computer gets hot

**Solutions:**

1. **Stop playback**
   - Click "Stop" button
   - CPU should drop to near zero

2. **Close and restart TABFRET**

3. **Update .NET Runtime**
   - Newer versions may have performance improvements

4. **Report the issue**
   - This shouldn't happen when idle
   - Please report on GitHub Issues

---

### Application Freezes or Hangs

**Symptoms:**
- Application becomes unresponsive
- "Not Responding" in title bar
- Can't click buttons

**Solutions:**

1. **Wait a moment**
   - Large MIDI files may take time to process
   - Look for "Loading..." in status bar

2. **Force close if truly frozen**
   - Press `Ctrl+Alt+Delete`
   - Task Manager → TABFRET → End Task

3. **Try a smaller file**
   - Test with known-good, small MIDI file

4. **Check available RAM**
   - Task Manager → Performance tab
   - Close memory-hungry applications

---

## Development Issues

### Build Errors

#### "The type or namespace name 'System' could not be found"

**Solution:**
```bash
# Restore NuGet packages
dotnet restore TABFRET.sln --force

# Clean and rebuild
dotnet clean
dotnet build
```

---

#### "Could not find SDK"

**Solution:**
```bash
# Check .NET SDK installation
dotnet --info

# Install .NET 6.0 SDK if needed
# Download from: https://dotnet.microsoft.com/download/dotnet/6.0
```

---

#### "Package restore failed"

**Solution:**
```bash
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore packages
dotnet restore TABFRET.sln --force
```

---

### Runtime Errors During Development

#### "WPF is only supported on Windows"

**Cause:** Trying to build/run on Mac/Linux

**Solution:**
- WPF requires Windows
- Use Windows for development
- Or wait for cross-platform version (using Avalonia UI)

---

#### "Could not load file or assembly..."

**Solution:**
```bash
# Rebuild entire solution
dotnet clean TABFRET.sln
dotnet build TABFRET.sln

# Or in Visual Studio: Build → Rebuild Solution
```

---

### Test Failures

**Solution:**
```bash
# Run tests with detailed output
dotnet test --verbosity detailed

# Run specific test
dotnet test --filter "FullyQualifiedName~TestName"

# Check test results for error details
```

---

### IntelliSense Not Working (Visual Studio)

**Solutions:**

1. **Rebuild solution**
   - Build → Rebuild Solution

2. **Clear cache**
   - Close Visual Studio
   - Delete `.vs` folder in solution directory
   - Reopen solution

3. **Restart Visual Studio**

4. **Update Visual Studio**
   - Check for updates in Help menu

---

## Getting Additional Help

### Before Asking for Help

1. **Check this troubleshooting guide** thoroughly
2. **Search existing [GitHub Issues](https://github.com/GizzZmo/TABFRET/issues)**
3. **Try the solutions** listed above
4. **Gather information**:
   - Windows version
   - TABFRET version
   - Exact error message
   - Steps to reproduce
   - Screenshots if applicable

### How to Get Help

1. **GitHub Issues** (preferred for bugs)
   - [Report a bug](https://github.com/GizzZmo/TABFRET/issues/new?labels=bug)
   - Include all gathered information
   - Attach sample MIDI file if relevant

2. **GitHub Discussions** (for questions)
   - [Start a discussion](https://github.com/GizzZmo/TABFRET/discussions)
   - Great for "how do I..." questions

3. **Email** (if available)
   - Check README for contact information

### What to Include in Bug Reports

```markdown
**Environment:**
- Windows Version: [e.g., Windows 11 22H2]
- TABFRET Version: [e.g., v1.0.0]
- .NET Runtime Version: [from `dotnet --list-runtimes`]

**Description:**
[Clear description of the problem]

**Steps to Reproduce:**
1. [First step]
2. [Second step]
3. [And so on...]

**Expected Behavior:**
[What you expected to happen]

**Actual Behavior:**
[What actually happened]

**Screenshots:**
[If applicable]

**Additional Context:**
[Any other relevant information]
```

---

## Known Issues

### Current Limitations

1. **No audio playback** - Visual only (planned for v2.0)
2. **Fixed playback speed** - No speed control yet (planned)
3. **Standard tuning only** - No alternate tunings yet (planned)
4. **No looping** - Can't loop sections yet (planned)
5. **Large files may be slow** - Performance optimization ongoing

### Reported Issues

Check [GitHub Issues](https://github.com/GizzZmo/TABFRET/issues) for:
- Known bugs being worked on
- Planned enhancements
- Feature requests

---

## Common Error Messages

| Error Message | Meaning | Solution |
|--------------|---------|----------|
| "Missing .NET Desktop Runtime" | .NET not installed | Install .NET 6.0 Desktop Runtime |
| "File not found" | MIDI file path is invalid | Check file exists and path is correct |
| "Invalid MIDI file" | File is corrupted or not MIDI | Try different file |
| "No notes found" | MIDI contains no note data | Use file with melodic content |
| "Out of memory" | File too large for available RAM | Try smaller file or close other apps |

---

## Still Having Issues?

If this guide didn't solve your problem:

1. **Search GitHub Issues**: [All Issues](https://github.com/GizzZmo/TABFRET/issues?q=is%3Aissue)
2. **Ask in Discussions**: [Discussions](https://github.com/GizzZmo/TABFRET/discussions)
3. **Report New Bug**: [New Issue](https://github.com/GizzZmo/TABFRET/issues/new)

We're here to help! 🎸

---

*Last updated: 2025*
