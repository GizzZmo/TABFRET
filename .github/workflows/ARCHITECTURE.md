# CI/CD Architecture

## Workflow Overview

```
┌─────────────────────────────────────────────────────────────────┐
│                        TABFRET CI/CD System                      │
└─────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│                         TRIGGER EVENTS                           │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│  Push to main/master  │  Pull Request  │  Version Tag  │ Manual │
│         ↓                    ↓              ↓             ↓      │
└─────────────────────────────────────────────────────────────────┘
                 │                │              │             │
                 ├────────────────┤              │             │
                 ↓                               ↓             ↓
        ┌─────────────────┐            ┌──────────────────────────┐
        │  CI Workflow    │            │   Release Workflow       │
        │   (ci.yml)      │            │   (release.yml)          │
        └─────────────────┘            └──────────────────────────┘
                 │                                  │
                 ↓                                  ↓
        ┌─────────────────┐            ┌──────────────────────────┐
        │ Build & Test    │            │ Build & Test             │
        │  - Restore      │            │  - Restore               │
        │  - Build        │            │  - Build                 │
        │  - Test         │            │  - Test                  │
        │  - Publish      │            │  - Publish               │
        └─────────────────┘            └──────────────────────────┘
                 │                                  │
                 ↓                                  ↓
        ┌─────────────────┐            ┌──────────────────────────┐
        │   Artifacts     │            │   Release Package        │
        │                 │            │                          │
        │ • Build-{SHA}   │            │ • TABFRET-{version}.zip  │
        │   (30 days)     │            │                          │
        │                 │            │ • GitHub Release         │
        │ • Release-{SHA} │            │ • Auto Release Notes     │
        │   (90 days)     │            │                          │
        │   [main only]   │            └──────────────────────────┘
        └─────────────────┘
```

## Workflow Details

### CI Workflow (ci.yml)

**Purpose:** Continuous integration for all code changes

**Triggers:**
- ✅ Push to main/master branches
- ✅ Pull requests to main/master branches
- ✅ Manual dispatch

**Steps:**
1. **Checkout** - Clone repository
2. **Setup .NET** - Install .NET 6.0 SDK
3. **Restore** - Download NuGet packages
4. **Build** - Compile solution in Release mode
5. **Test** - Run all unit tests
6. **Publish** - Create publish directory with all dependencies
7. **Upload Artifacts** - Store build artifacts (all commits)
8. **Create ZIP** - Package release (main/master only)
9. **Upload ZIP** - Store release package (main/master only)

**Artifacts Produced:**
- `TABFRET-build-{SHA}` - Full build output (30 day retention)
- `TABFRET-Release-{SHA}` - ZIP package for main/master (90 day retention)

### Release Workflow (release.yml)

**Purpose:** Automated releases from version tags

**Triggers:**
- ✅ Version tags (v*.*.*, e.g., v1.0.0)
- ✅ Manual dispatch

**Steps:**
1. **Checkout** - Clone repository at tag
2. **Setup .NET** - Install .NET 6.0 SDK
3. **Restore** - Download NuGet packages
4. **Build** - Compile solution in Release mode
5. **Test** - Run all unit tests (quality gate)
6. **Publish** - Create publish directory
7. **Create ZIP** - Package as TABFRET-{version}.zip
8. **GitHub Release** - Create release with:
   - Version tag as title
   - ZIP artifact attached
   - Auto-generated release notes

**Artifacts Produced:**
- `TABFRET-{version}.zip` - Attached to GitHub Release
- GitHub Release with auto-generated notes

## Platform Requirements

- **OS:** Windows (windows-latest runner)
- **SDK:** .NET 6.0
- **Framework:** net6.0-windows (WPF)

## Artifact Structure

```
TABFRET-{version}.zip
├── TABFRET.exe                           # Main executable
├── TABFRET.dll                           # Application DLL
├── TABFRET.deps.json                     # Dependencies manifest
├── TABFRET.runtimeconfig.json            # Runtime configuration
├── *.dll                                 # All NuGet dependencies
│   ├── Melanchall.DryWetMidi.dll
│   ├── NAudio.*.dll
│   └── Microsoft.Extensions.*.dll
└── Native Libraries                      # Platform-specific MIDI support
    ├── Melanchall_DryWetMidi_Native32.dll
    └── Melanchall_DryWetMidi_Native64.dll
```

## Retention Policies

| Artifact Type | Retention | Scope |
|--------------|-----------|-------|
| Build artifacts | 30 days | All commits |
| Release packages | 90 days | main/master only |
| GitHub Releases | Permanent | Version tags only |

## Benefits

✅ **Automated Quality Assurance** - Tests run on every build  
✅ **Consistent Builds** - Same process every time  
✅ **Easy Distribution** - One-click downloads from Releases  
✅ **Version Tracking** - Clear version history with artifacts  
✅ **Fast Feedback** - Immediate build/test results  
✅ **Release Notes** - Auto-generated from commits  

## Security

- Workflows run in isolated environments
- No secrets required for building
- GitHub token auto-provided for releases
- Artifacts stored securely by GitHub

## Monitoring

- **Build Status:** README badges show current status
- **Actions Tab:** Full workflow history and logs
- **Releases Page:** All published versions
- **Artifacts:** Available in workflow run details
