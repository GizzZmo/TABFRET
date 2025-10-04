# CI/CD Workflows

This repository uses GitHub Actions for continuous integration and deployment.

## Workflows

### CI Build & Test (`ci.yml`)

**Triggers:**
- Push to `main` or `master` branch
- Pull requests to `main` or `master` branch  
- Manual trigger via workflow_dispatch

**Jobs:**
1. **Restore dependencies** - Restores all NuGet packages for the solution
2. **Build solution** - Builds the entire solution in Release configuration
3. **Run tests** - Executes all unit tests in the solution
4. **Publish application** - Creates a self-contained publish directory
5. **Upload build artifacts** - Uploads the published application as a build artifact
   - Artifact name: `TABFRET-build-{SHA}` (e.g., `TABFRET-build-abc123`)
   - Retention: 30 days
6. **Create release package** (main/master only) - Creates a ZIP file of the release
7. **Upload release package** (main/master only) - Uploads the release ZIP
   - Artifact name: `TABFRET-Release-{SHA}`
   - Retention: 90 days

### Release (`release.yml`)

**Triggers:**
- Push of version tags (e.g., `v1.0.0`, `v2.1.3`)
- Manual trigger via workflow_dispatch

**Jobs:**
1. **Restore dependencies** - Restores all NuGet packages
2. **Build solution** - Builds in Release configuration  
3. **Run tests** - Executes all tests to ensure quality
4. **Publish application** - Creates the release build
5. **Create release package** - Creates a ZIP file named `TABFRET-{version}.zip`
6. **Create GitHub Release** - Automatically creates a GitHub Release with:
   - The version tag as the release name
   - The ZIP artifact attached
   - Auto-generated release notes

## Downloading Artifacts

### From Workflow Runs
1. Go to the [Actions tab](../../actions)
2. Click on a workflow run
3. Scroll to the "Artifacts" section at the bottom
4. Download the desired artifact

### From Releases
1. Go to the [Releases page](../../releases)
2. Find the desired version
3. Download the `TABFRET-v*.*.*.zip` file from Assets

## Creating a Release

To create a new release:

1. Update the version in your project files (if applicable)
2. Commit your changes
3. Create and push a version tag:
   ```bash
   git tag v1.0.0
   git push origin v1.0.0
   ```
4. The Release workflow will automatically:
   - Build the project
   - Run tests
   - Create a ZIP package
   - Create a GitHub Release with the artifact

## Requirements

- **Runner:** Windows (windows-latest)
- **.NET SDK:** 6.0.x
- **Target Framework:** net6.0-windows (WPF application)

## Artifact Contents

The published artifacts include:
- TABFRET.exe (main executable)
- TABFRET.dll
- All required dependencies (NAudio, DryWetMidi, etc.)
- Runtime configuration files
- Native libraries for MIDI support

## Testing Locally

To test the build process locally:

```powershell
# Restore dependencies
dotnet restore TABFRET.sln

# Build solution
dotnet build TABFRET.sln --configuration Release --no-restore

# Run tests
dotnet test TABFRET.sln --configuration Release --no-build

# Publish application
dotnet publish ./src/TABFRET.csproj --configuration Release --output ./publish --no-build
```
