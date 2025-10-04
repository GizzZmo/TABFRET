# Quick Start Guide: CI/CD System

## 🚀 Getting Started

The TABFRET project now has a fully automated CI/CD system powered by GitHub Actions.

## 📋 What Happens Automatically

### When you push code:
1. ✅ Code is automatically built
2. ✅ Tests are run
3. ✅ Build artifacts are created
4. ✅ If on main/master: Release package is created

### When you create a version tag:
1. ✅ Release build is created
2. ✅ Tests are run
3. ✅ ZIP package is created
4. ✅ GitHub Release is automatically published

## 🔧 For Developers

### Running Builds
- **Automatic:** Push to any branch triggers a build
- **Manual:** Go to Actions → CI Build & Test → Run workflow

### Getting Build Artifacts
1. Go to [Actions](https://github.com/GizzZmo/TABFRET/actions)
2. Click on a workflow run
3. Scroll to "Artifacts" section
4. Download `TABFRET-build-{sha}` or `TABFRET-Release-{sha}`

### Testing Locally
```powershell
# Full build process
dotnet restore TABFRET.sln
dotnet build TABFRET.sln --configuration Release --no-restore
dotnet test TABFRET.sln --configuration Release --no-build
dotnet publish ./src/TABFRET.csproj --configuration Release --output ./publish --no-build
```

## 📦 Creating a Release

### Step-by-Step

1. **Update version** (if you have versioning in code)
   
2. **Commit your changes**
   ```bash
   git add .
   git commit -m "Prepare for release v1.0.0"
   ```

3. **Create and push a version tag**
   ```bash
   git tag v1.0.0
   git push origin v1.0.0
   ```

4. **Done!** The release workflow will:
   - Build the application
   - Run tests
   - Create a ZIP package
   - Publish a GitHub Release

### Accessing Releases

Go to the [Releases page](https://github.com/GizzZmo/TABFRET/releases) to:
- Download the latest version
- View release notes
- See all available versions

## 📊 Monitoring Builds

### Build Status Badges
The README shows real-time build status:
- ✅ Green = Passing
- ❌ Red = Failed
- 🟡 Yellow = In Progress

### Viewing Build Details
1. Click on a status badge
2. Or go to the [Actions tab](https://github.com/GizzZmo/TABFRET/actions)
3. Click on any workflow run to see:
   - Build logs
   - Test results
   - Artifacts

## 🛠️ Workflow Files

| File | Purpose |
|------|---------|
| `.github/workflows/ci.yml` | Main CI workflow - builds and tests on every push/PR |
| `.github/workflows/release.yml` | Release workflow - creates releases from version tags |
| `.github/workflows/README.md` | Detailed workflow documentation |

## 🐛 Troubleshooting

### Build Failed?
1. Check the Actions tab for error logs
2. Common issues:
   - Compilation errors
   - Test failures
   - Missing dependencies

### Release Not Created?
1. Ensure tag follows pattern `v*.*.*` (e.g., `v1.0.0`)
2. Check if tests passed
3. Verify workflow has necessary permissions

## 💡 Tips

- **Test First:** Run `dotnet test` locally before pushing
- **Check Logs:** Always review workflow logs for warnings
- **Tag Format:** Use semantic versioning (v1.0.0, v2.1.3, etc.)
- **Clean Builds:** Artifacts are built fresh each time

## 📚 More Information

- [Full CI/CD Documentation](.github/workflows/README.md)
- [Implementation Summary](CI_CD_SUMMARY.md)
- [Main README](README.md)

## ❓ Need Help?

- Open an issue for build problems
- Check existing workflow runs for examples
- Review the documentation files listed above
