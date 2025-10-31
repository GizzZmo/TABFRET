# Contributing to TABFRET

Thank you for your interest in contributing to TABFRET! This document provides guidelines and instructions for contributing to the project.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Setup](#development-setup)
- [How to Contribute](#how-to-contribute)
- [Coding Standards](#coding-standards)
- [Pull Request Process](#pull-request-process)
- [Reporting Bugs](#reporting-bugs)
- [Suggesting Enhancements](#suggesting-enhancements)

## Code of Conduct

We are committed to providing a welcoming and inclusive environment for all contributors. Please be respectful and constructive in all interactions.

## Getting Started

1. **Fork the repository** on GitHub
2. **Clone your fork** locally:
   ```bash
   git clone https://github.com/YOUR-USERNAME/TABFRET.git
   cd TABFRET
   ```
3. **Add upstream remote**:
   ```bash
   git remote add upstream https://github.com/GizzZmo/TABFRET.git
   ```

## Development Setup

### Prerequisites

- **Windows 10 or higher** (required for WPF development)
- **Visual Studio 2022** or later (Community Edition is fine)
  - Install the ".NET desktop development" workload
- **.NET 6.0 SDK** or later
- **Git** for version control

### Building the Project

1. **Restore NuGet packages**:
   ```bash
   dotnet restore TABFRET.sln
   ```

2. **Build the solution**:
   ```bash
   dotnet build TABFRET.sln --configuration Release
   ```

3. **Run tests**:
   ```bash
   dotnet test TABFRET.sln --configuration Release
   ```

4. **Run the application**:
   ```bash
   dotnet run --project src/TABFRET.csproj
   ```

### Development with Visual Studio

1. Open `TABFRET.sln` in Visual Studio
2. Press `Ctrl+Shift+B` to build
3. Press `F5` to run with debugging
4. Use Test Explorer to run unit tests

## How to Contribute

### Types of Contributions

We welcome various types of contributions:

- **Bug fixes**: Fix issues reported in the issue tracker
- **New features**: Add new functionality to the application
- **Documentation**: Improve or add documentation
- **Tests**: Add or improve test coverage
- **Performance improvements**: Optimize existing code
- **UI/UX improvements**: Enhance the user interface and experience

### Workflow

1. **Create a branch** for your work:
   ```bash
   git checkout -b feature/your-feature-name
   ```
   Use prefixes like `feature/`, `bugfix/`, `docs/`, etc.

2. **Make your changes** following our coding standards

3. **Test your changes** thoroughly:
   - Run existing tests: `dotnet test`
   - Add new tests for new functionality
   - Test the application manually

4. **Commit your changes**:
   ```bash
   git add .
   git commit -m "Brief description of your changes"
   ```
   
   Write clear, descriptive commit messages:
   - Use present tense ("Add feature" not "Added feature")
   - Keep the first line under 50 characters
   - Add details in the body if needed

5. **Push to your fork**:
   ```bash
   git push origin feature/your-feature-name
   ```

6. **Create a Pull Request** on GitHub

## Coding Standards

### C# Style Guidelines

- Follow [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Keep methods focused and small (Single Responsibility Principle)

### MVVM Pattern

TABFRET uses the MVVM (Model-View-ViewModel) pattern:

- **Models**: Data structures and business entities
- **Views**: XAML files for UI
- **ViewModels**: UI logic and data preparation
- **Services**: Business logic and external interactions

### Code Organization

```
src/
├── Models/          # Data models
├── Views/           # XAML views
├── ViewModels/      # ViewModels for MVVM
├── Services/        # Business logic services
├── Utils/           # Helper utilities
└── Config/          # Configuration files
```

### Best Practices

1. **Use dependency injection** where appropriate
2. **Implement INotifyPropertyChanged** in ViewModels
3. **Keep Views thin** - logic belongs in ViewModels
4. **Write unit tests** for business logic
5. **Handle exceptions** appropriately
6. **Use async/await** for I/O operations

## Pull Request Process

1. **Update documentation** if needed (README, code comments, etc.)
2. **Add tests** for new functionality
3. **Ensure all tests pass** locally
4. **Update the CHANGELOG** (if we have one) with your changes
5. **Request a review** from maintainers
6. **Address review comments** promptly
7. **Squash commits** if requested before merging

### Pull Request Checklist

Before submitting, ensure:

- [ ] Code builds without errors
- [ ] All tests pass
- [ ] New functionality has tests
- [ ] Documentation is updated
- [ ] Code follows style guidelines
- [ ] No unnecessary files are included (build artifacts, etc.)
- [ ] Commit messages are clear and descriptive
- [ ] PR description explains what and why

## Reporting Bugs

### Before Submitting a Bug Report

- **Check existing issues** to avoid duplicates
- **Try the latest version** to see if the issue still exists
- **Gather information** about the bug

### How to Submit a Bug Report

Use the GitHub issue tracker and include:

1. **Title**: Clear, descriptive summary
2. **Description**: Detailed explanation of the bug
3. **Steps to reproduce**:
   - Step 1
   - Step 2
   - Step 3
4. **Expected behavior**: What should happen
5. **Actual behavior**: What actually happens
6. **Environment**:
   - OS version (Windows 10/11)
   - .NET version
   - TABFRET version
7. **Screenshots or logs**: If applicable
8. **Sample MIDI file**: If the bug is related to specific MIDI files

### Bug Report Template

```markdown
## Bug Description
[Clear description of the bug]

## Steps to Reproduce
1. [First Step]
2. [Second Step]
3. [And so on...]

## Expected Behavior
[What you expected to happen]

## Actual Behavior
[What actually happened]

## Environment
- OS: Windows [version]
- .NET: [version]
- TABFRET: [version]

## Additional Context
[Any other relevant information, screenshots, logs, etc.]
```

## Suggesting Enhancements

### Before Submitting an Enhancement

- **Check the roadmap** (if available)
- **Search existing issues** for similar suggestions
- **Consider if it fits** the project's goals

### How to Submit an Enhancement Suggestion

Create an issue on GitHub with:

1. **Title**: Clear summary of the enhancement
2. **Use case**: Why is this enhancement needed?
3. **Proposed solution**: How should it work?
4. **Alternatives considered**: Other approaches you thought about
5. **Additional context**: Mockups, examples, references

### Enhancement Suggestion Template

```markdown
## Enhancement Description
[Clear description of the proposed enhancement]

## Use Case
[Why is this enhancement valuable? What problem does it solve?]

## Proposed Solution
[How should this enhancement work?]

## Alternatives Considered
[Other approaches you've thought about]

## Additional Context
[Mockups, screenshots, examples, references, etc.]
```

## Questions?

If you have questions about contributing, feel free to:

- Open an issue with the "question" label
- Reach out to the maintainers
- Check existing documentation

## Recognition

Contributors will be recognized in:

- The project's README (Credits section)
- Release notes for significant contributions
- GitHub's contributor graph

Thank you for contributing to TABFRET! 🎸
