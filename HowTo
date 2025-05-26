TABFRET: How-to Wiki - Source Code Guide
This document serves as a comprehensive guide to the source code of the TABFRET application, a conceptual WPF (Windows Presentation Foundation) tool for visualizing MIDI data as guitar tablature. It details each C# file and the settings.json configuration, explaining its role and providing its complete content.

The project follows an MVVM (Model-View-ViewModel) architectural pattern for clear separation of concerns.

Project Structure Overview
TABFRET/
├── TABFRET.sln             # Visual Studio Solution file
├── TABFRET.csproj          # C# Project file
├── App.xaml                # Application entry point
├── App.xaml.cs
├── MainWindow.xaml         # Main UI window
├── MainWindow.xaml.cs      # UI logic (minimal in MVVM)
├── Models/                 # Data models
│   ├── MidiNote.cs         # Defines the structure for representing MIDI notes
│   └── TabNote.cs          # Defines the structure for representing tablature notes
├── Views/                  # UI Components
│   ├── GuitarNeckView.xaml   # Guitar neck visualization
│   └── GuitarNeckView.xaml.cs # Code-behind for GuitarNeckView
├── ViewModels/             # UI logic and data preparation for views
│   └── MainViewModel.cs    # Manages the UI logic in the MVVM framework
├── Services/               # Business logic and external interactions
│   ├── MidiParser.cs       # Parses MIDI files and extracts note data
│   └── TabMapper.cs        # Converts MIDI notes into guitar tablature
├── Utils/                  # Helper functions
│   └── FretboardHelper.cs  # Contains utility functions for fretboard calculations
├── Config/                 # Configuration files
│   └── settings.json       # User settings file
└── README.md               # Project documentation

Source Code Files
1. App.xaml
Path: App.xaml

Purpose: This is the application definition file, serving as the entry point for your WPF application. It's where you can define application-wide resources and specify the startup URI (which window to open first).

<Application x:Class="TABFRET.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:TABFRET"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        </Application.Resources>
</Application>

2. App.xaml.cs
Path: App.xaml.cs

Purpose: The code-behind for App.xaml. This file handles application-level events and can contain startup logic. For a typical WPF application, it often remains largely empty unless specific application-wide initialization is required.

using System.Configuration;
using System.Data;
using System.Windows;

namespace TABFRET
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
}

3. MainWindow.xaml
Path: MainWindow.xaml

Purpose: This XAML file defines the main user interface window for your application. It includes a basic layout with control buttons and integrates the GuitarNeckView for visualization. It also sets the DataContext to an instance of MainViewModel, enabling data binding.

<Window x:Class="TABFRET.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:TABFRET"
        mc:Ignorable="d"
        Title="TABFRET: MIDI to Guitar Tab Visualizer" Height="600" Width="900"
        MinHeight="450" MinWidth="700">
    <Window.DataContext>
        <local:MainViewModel/>
    </Window.DataContext>
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>    <RowDefinition Height="*"/>       <RowDefinition Height="Auto"/>    </Grid.RowDefinitions>

        <StackPanel Grid.Row="0" Orientation="Horizontal" HorizontalAlignment="Center" Margin="10">
            <Button Content="Load MIDI" Command="{Binding LoadMidiCommand}" Padding="10,5" Margin="5"
                    Background="#4CAF50" Foreground="White" FontWeight="Bold" BorderBrush="#388E3C" BorderThickness="1"/>
            <Button Content="Play" Command="{Binding PlayCommand}" Padding="10,5" Margin="5"
                    Background="#2196F3" Foreground="White" FontWeight="Bold" BorderBrush="#1976D2" BorderThickness="1"/>
            <Button Content="Pause" Command="{Binding PauseCommand}" Padding="10,5" Margin="5"
                    Background="#FFC107" Foreground="Black" FontWeight="Bold" BorderBrush="#FFA000" BorderThickness="1"/>
            <Button Content="Stop" Command="{Binding StopCommand}" Padding="10,5" Margin="5"
                    Background="#F44336" Foreground="White" FontWeight="Bold" BorderBrush="#D32F2F" BorderThickness="1"/>
        </StackPanel>

        <local:GuitarNeckView Grid.Row="1" Margin="10" Background="#F0F0F0" BorderBrush="LightGray" BorderThickness="1" CornerRadius="5"/>

        <StatusBar Grid.Row="2" Margin="0,5,0,0" Background="#E0E0E0">
            <StatusBarItem>
                <TextBlock Text="{Binding StatusMessage}" FontWeight="SemiBold" Foreground="#333333"/>
            </StatusBarItem>
            <StatusBarItem HorizontalAlignment="Right">
                <TextBlock Text="{Binding MidiFilePath}" FontStyle="Italic" Foreground="#555555"/>
            </StatusBarItem>
        </StatusBar>
    </Grid>
</Window>

4. MainWindow.xaml.cs
Path: MainWindow.xaml.cs

Purpose: The code-behind file for MainWindow.xaml. In an MVVM application, its role is typically minimal, often just calling InitializeComponent() and potentially handling window-level events or programmatic DataContext setup if not done in XAML.

using System.Windows;

namespace TABFRET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // The DataContext is set in MainWindow.xaml: <local:MainViewModel/>
            // So, no explicit setting is needed here unless you want to do it programmatically.
            // Example if setting programmatically:
            // this.DataContext = new MainViewModel();
        }
    }
}

5. Models/MidiNote.cs
Path: Models/MidiNote.cs

Purpose: Defines the structure for representing a single MIDI note within the application. This struct holds essential information extracted from a MIDI file, such as note number, start time, duration, velocity, and track number.

using System;

namespace TABFRET
{
    /// <summary>
    /// Defines the structure for representing a single MIDI note in the application.
    /// This struct holds the essential information extracted from a MIDI file for a note.
    /// </summary>
    public struct MidiNote
    {
        public int MidiNoteNumber { get; set; } // MIDI note number (0-127, e.g., 60 for Middle C)
        public long StartTimeTicks { get; set; } // The start time of the note in MIDI ticks
        public long DurationTicks { get; set; }  // The duration of the note in MIDI ticks
        public int Velocity { get; set; }       // The velocity (how hard the note was played, 0-127)
        public int TrackNumber { get; set; }    // The MIDI track number the note belongs to

        /// <summary>
        /// Provides a string representation of the MidiNote for debugging or display.
        /// </summary>
        public override string ToString()
        {
            return $"Note: {MidiNoteNumber}, Start: {StartTimeTicks}, Duration: {DurationTicks}, Track: {TrackNumber}, Velocity: {Velocity}";
        }
    }
}

6. Models/TabNote.cs
Path: Models/TabNote.cs

Purpose: Defines the structure for representing a single note on the guitar tablature. This includes the string and fret number, derived from the MIDI note, along with its timing information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace TABFRET
{
    /// <summary>
    /// Defines the standard guitar strings and their open MIDI note numbers.
    /// </summary>
    public enum GuitarString
    {
        HighE = 1, // MIDI Note 64 (E4)
        B = 2,     // MIDI Note 59 (B3)
        G = 3,     // MIDI Note 55 (G3)
        D = 4,     // MIDI Note 50 (D3)
        A = 5,     // MIDI Note 45 (A2)
        LowE = 6   // MIDI Note 40 (E2)
    }

    /// <summary>
    /// Represents a single note on the guitar tablature.
    /// </summary>
    public struct TabNote
    {
        public int StringNumber { get; set; } // 1 (High E) to 6 (Low E)
        public int FretNumber { get; set; }   // 0 for open string
        public long StartTimeTicks { get; set; } // From original MIDI event
        public long DurationTicks { get; set; }  // From original MIDI event
        public int OriginalMidiNoteNumber { get; set; } // For reference

        public override string ToString()
        {
            return $"String: {StringNumber}, Fret: {FretNumber}, Start: {StartTimeTicks}";
        }
    }
}

7. Views/GuitarNeckView.xaml
Path: Views/GuitarNeckView.xaml

Purpose: This XAML file defines the visual structure of the guitar neck. It primarily consists of a Canvas element where the fretboard, strings, and dynamically generated notes will be drawn by its code-behind.

<UserControl x:Class="TABFRET.GuitarNeckView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
             xmlns:local="clr-namespace:TABFRET"
             mc:Ignorable="d"
             d:DesignHeight="300" d:DesignWidth="600">
    <Grid>
        <Canvas x:Name="FretboardCanvas" Background="LightGray"
                HorizontalAlignment="Stretch" VerticalAlignment="Stretch"/>
    </Grid>
</UserControl>

8. Views/GuitarNeckView.xaml.cs
Path: Views/GuitarNeckView.xaml.cs

Purpose: The code-behind for GuitarNeckView.xaml. This file contains the logic for drawing the static elements of the guitar neck (strings, frets, fret markers) and dynamically updating the visual representation of notes based on the MainViewModel's data and playback position.

using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System;
using System.Linq;

namespace TABFRET
{
    /// <summary>
    /// Interaction logic for GuitarNeckView.xaml
    /// NOTE: This code-behind assumes a corresponding GuitarNeckView.xaml file exists
    /// with a Canvas named 'FretboardCanvas' and other UI elements.
    /// </summary>
    public partial class GuitarNeckView : UserControl
    {
        private MainViewModel _viewModel; // Reference to the ViewModel

        public GuitarNeckView()
        {
            // InitializeComponent(); // This would be called if a .xaml file exists

            // This is a placeholder for the XAML initialization.
            // In a real WPF app, you would have a GuitarNeckView.xaml file
            // that defines the UI elements like a Canvas for drawing.
            // For example:
            // <UserControl x:Class="TABFRET.GuitarNeckView" ...>
            //     <Grid>
            //         <Canvas x:Name="FretboardCanvas" Background="LightGray"/>
            //     </Grid>
            // </UserControl>

            // For this conceptual code, we'll create a mock canvas to avoid null reference.
            // In a real app, FretboardCanvas would be defined in XAML.
            var mockCanvas = new Canvas { Background = Brushes.LightGray };
            this.Content = mockCanvas; // Set the mock canvas as content

            this.Loaded += GuitarNeckView_Loaded;
            this.SizeChanged += GuitarNeckView_SizeChanged;
        }

        /// <summary>
        /// Handles the Loaded event of the UserControl.
        /// </summary>
        private void GuitarNeckView_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the DataContext and cast it to MainViewModel
            if (this.DataContext is MainViewModel viewModel)
            {
                _viewModel = viewModel;
                // Subscribe to PropertyChanged events for relevant properties
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;
                // Subscribe to CollectionChanged for CurrentTabNotes
                _viewModel.CurrentTabNotes.CollectionChanged += CurrentTabNotes_CollectionChanged;

                // Initial draw based on current ViewModel state
                DrawGuitarNeck();
                UpdateVisualNotes();
            }
        }

        /// <summary>
        /// Handles changes in the ViewModel's properties.
        /// </summary>
        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // React to changes in the ViewModel that affect the visual display
            if (e.PropertyName == nameof(MainViewModel.CurrentPlaybackPositionTicks) ||
                e.PropertyName == nameof(MainViewModel.IsPlaying))
            {
                // Update note highlighting based on playback position
                Dispatcher.Invoke(UpdateVisualNotes);
            }
            else if (e.PropertyName == nameof(MainViewModel.IsMidiLoaded))
            {
                // Redraw if MIDI load state changes (e.g., clear notes on unload)
                Dispatcher.Invoke(() =>
                {
                    DrawGuitarNeck(); // Redraw base neck
                    UpdateVisualNotes(); // Update notes
                });
            }
            // Add more conditions for other properties that might affect the view
        }

        /// <summary>
        /// Handles changes in the CurrentTabNotes collection.
        /// </summary>
        private void CurrentTabNotes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // When the collection of notes changes (e.g., new MIDI loaded), redraw notes.
            Dispatcher.Invoke(UpdateVisualNotes);
        }

        /// <summary>
        /// Handles the SizeChanged event to redraw the neck when the control resizes.
        /// </summary>
        private void GuitarNeckView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawGuitarNeck();
            UpdateVisualNotes();
        }

        /// <summary>
        /// Draws the static elements of the guitar neck (strings, frets, fret markers).
        /// </summary>
        private void DrawGuitarNeck()
        {
            // In a real WPF app, FretboardCanvas would be a direct reference.
            // For this conceptual code, we need to cast Content back to Canvas.
            if (!(this.Content is Canvas FretboardCanvas)) return;

            FretboardCanvas.Children.Clear(); // Clear existing drawings

            double canvasWidth = FretboardCanvas.ActualWidth;
            double canvasHeight = FretboardCanvas.ActualHeight;

            if (canvasWidth == 0 || canvasHeight == 0) return; // Avoid drawing on zero size

            // Draw strings (horizontal lines)
            for (int i = 1; i <= 6; i++) // 6 strings
            {
                double yPos = FretboardHelper.GetYPositionForString(i, canvasHeight);
                Line stringLine = new Line
                {
                    X1 = 0,
                    Y1 = yPos,
                    X2 = canvasWidth,
                    Y2 = yPos,
                    Stroke = Brushes.Black,
                    StrokeThickness = (i == 6) ? 2 : 1 // Thicker for low E
                };
                FretboardCanvas.Children.Add(stringLine);
            }

            // Draw frets (vertical lines)
            int numberOfFretsToDraw = 15; // Draw up to 15 frets for visual representation
            for (int i = 0; i <= numberOfFretsToDraw; i++) // 0 is the nut
            {
                double xPos = FretboardHelper.GetXPositionForFret(i, canvasWidth, numberOfFretsToDraw);
                Line fretLine = new Line
                {
                    X1 = xPos,
                    Y1 = 0,
                    X2 = xPos,
                    Y2 = canvasHeight,
                    Stroke = Brushes.DarkGray,
                    StrokeThickness = (i == 0) ? 3 : 1 // Thicker for the nut
                };
                FretboardCanvas.Children.Add(fretLine);

                // Add fret numbers (optional)
                if (i > 0)
                {
                    TextBlock fretNumberText = new TextBlock
                    {
                        Text = i.ToString(),
                        Foreground = Brushes.Gray,
                        FontSize = 10,
                        Margin = new Thickness(xPos + 5, 5, 0, 0) // Position near the fret line
                    };
                    FretboardCanvas.Children.Add(fretNumberText);
                }
            }

            // Draw fret markers (dots) at 3, 5, 7, 9, 12 (double), 15...
            DrawFretMarkers(FretboardCanvas, canvasWidth, canvasHeight);
        }

        /// <summary>
        /// Draws the fret markers (dots) on the fretboard.
        /// </summary>
        private void DrawFretMarkers(Canvas canvas, double canvasWidth, double canvasHeight)
        {
            int[] singleDotFrets = { 3, 5, 7, 9, 15 };
            int[] doubleDotFrets = { 12 };

            foreach (int fret in singleDotFrets)
            {
                double xPos = FretboardHelper.GetXPositionForFret(fret, canvasWidth, 15);
                double nextXPos = FretboardHelper.GetXPositionForFret(fret + 1, canvasWidth, 15);
                double centerX = xPos + (nextXPos - xPos) / 2; // Center of the fret space

                Ellipse dot = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brushes.White,
                    Margin = new Thickness(centerX - 5, canvasHeight / 2 - 5, 0, 0) // Centered vertically
                };
                canvas.Children.Add(dot);
            }

            foreach (int fret in doubleDotFrets)
            {
                double xPos = FretboardHelper.GetXPositionForFret(fret, canvasWidth, 15);
                double nextXPos = FretboardHelper.GetXPositionForFret(fret + 1, canvasWidth, 15);
                double centerX = xPos + (nextXPos - xPos) / 2;

                Ellipse dot1 = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brushes.White,
                    Margin = new Thickness(centerX - 15, canvasHeight / 2 - 5, 0, 0)
                };
                Ellipse dot2 = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brushes.White,
                    Margin = new Thickness(centerX + 5, canvasHeight / 2 - 5, 0, 0)
                };
                canvas.Children.Add(dot1);
                canvas.Children.Add(dot2);
            }
        }

        /// <summary>
        /// Updates the visual representation of notes on the guitar neck based on
        /// the current tablature data and playback position.
        /// </summary>
        private void UpdateVisualNotes()
        {
            // In a real WPF app, FretboardCanvas would be a direct reference.
            if (!(this.Content is Canvas FretboardCanvas)) return;

            // Remove only the note visuals, keep the neck drawing
            var notesToRemove = FretboardCanvas.Children.OfType<Ellipse>().Where(e => e.Tag?.ToString() == "Note").ToList();
            foreach (var noteEllipse in notesToRemove)
            {
                FretboardCanvas.Children.Remove(noteEllipse);
            }

            if (_viewModel == null || !_viewModel.IsMidiLoaded || !_viewModel.CurrentTabNotes.Any())
            {
                return; // No notes to draw
            }

            double canvasWidth = FretboardCanvas.ActualWidth;
            double canvasHeight = FretboardCanvas.ActualHeight;

            foreach (var note in _viewModel.CurrentTabNotes)
            {
                // Calculate position for the note
                double xPos = FretboardHelper.GetXPositionForFret(note.FretNumber, canvasWidth, 15);
                double nextXPos = FretboardHelper.GetXPositionForFret(note.FretNumber + 1, canvasWidth, 15);
                double centerX = xPos + (nextXPos - xPos) / 2; // Center of the fret space

                double yPos = FretboardHelper.GetYPositionForString(note.StringNumber, canvasHeight);

                Ellipse noteEllipse = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = Brushes.Blue, // Default color
                    Stroke = Brushes.DarkBlue,
                    StrokeThickness = 1,
                    Tag = "Note" // Tag to identify note ellipses for removal
                };

                // Position the ellipse centered on the string and within the fret
                Canvas.SetLeft(noteEllipse, centerX - noteEllipse.Width / 2);
                Canvas.SetTop(noteEllipse, yPos - noteEllipse.Height / 2);

                // Highlight notes currently being played
                if (_viewModel.IsPlaying &&
                    _viewModel.CurrentPlaybackPositionTicks >= note.StartTimeTicks &&
                    _viewModel.CurrentPlaybackPositionTicks < (note.StartTimeTicks + note.DurationTicks))
                {
                    noteEllipse.Fill = Brushes.Red; // Highlight color
                }

                FretboardCanvas.Children.Add(noteEllipse);

                // Add fret number text on the note (optional)
                TextBlock fretText = new TextBlock
                {
                    Text = note.FretNumber.ToString(),
                    Foreground = Brushes.White,
                    FontSize = 10,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Tag = "Note" // Tag as part of the note visual
                };
                Canvas.SetLeft(fretText, Canvas.GetLeft(noteEllipse) + (noteEllipse.Width / 2) - (fretText.ActualWidth / 2));
                Canvas.SetTop(fretText, Canvas.GetTop(noteEllipse) + (noteEllipse.Height / 2) - (fretText.ActualHeight / 2));
                FretboardCanvas.Children.Add(fretText);
            }
        }
    }
}

9. ViewModels/MainViewModel.cs
Path: ViewModels/MainViewModel.cs

Purpose: The central ViewModel for the TABFRET application. It manages the UI state, exposes data to the views via properties that implement INotifyPropertyChanged, and handles user commands (e.g., Load, Play, Pause, Stop) by interacting with the MidiParser and TabMapper services. It also controls the simulated playback.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading; // Required for DispatcherTimer in WPF

namespace TABFRET
{
    /// <summary>
    /// A simple ICommand implementation for WPF commands.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
    }

    /// <summary>
    /// The main ViewModel for the TABFRET application.
    /// Manages UI state, commands, and orchestrates data flow between parser/mapper.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly MidiParser _midiParser;
        private readonly TabMapper _tabMapper;
        private DispatcherTimer _playbackTimer;

        private string _midiFilePath;
        private bool _isMidiLoaded;
        private ObservableCollection<TabNote> _currentTabNotes;
        private long _currentPlaybackPositionTicks;
        private bool _isPlaying;
        private string _statusMessage;
        private GuitarString _selectedTuningString; // For simplicity, let's just allow changing one string's tuning concept if needed, or make it a full tuning object.

        // Properties bound to UI
        public string MidiFilePath
        {
            get => _midiFilePath;
            set => SetProperty(ref _midiFilePath, value);
        }

        public bool IsMidiLoaded
        {
            get => _isMidiLoaded;
            set => SetProperty(ref _isMidiLoaded, value);
        }

        public ObservableCollection<TabNote> CurrentTabNotes
        {
            get => _currentTabNotes;
            set => SetProperty(ref _currentTabNotes, value);
        }

        public long CurrentPlaybackPositionTicks
        {
            get => _currentPlaybackPositionTicks;
            set => SetProperty(ref _currentPlaybackPositionTicks, value);
        }

        public bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                if (SetProperty(ref _isPlaying, value))
                {
                    // Update command states when IsPlaying changes
                    ((RelayCommand)PlayCommand).CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                    ((RelayCommand)PauseCommand).CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                    ((RelayCommand)StopCommand).CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public GuitarString SelectedTuningString // Example: if you wanted to allow changing individual string tunings
        {
            get => _selectedTuningString;
            set => SetProperty(ref _selectedTuningString, value);
        }

        // Commands bound to UI buttons/actions
        public ICommand LoadMidiCommand { get; }
        public ICommand PlayCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand StopCommand { get; }

        public MainViewModel()
        {
            _midiParser = new MidiParser();
            _tabMapper = new TabMapper();
            _currentTabNotes = new ObservableCollection<TabNote>();
            _statusMessage = "Ready.";

            // Initialize commands
            LoadMidiCommand = new RelayCommand(async _ => await LoadMidiFile(), _ => true);
            PlayCommand = new RelayCommand(_ => Play(), _ => IsMidiLoaded && !IsPlaying);
            PauseCommand = new RelayCommand(_ => Pause(), _ => IsPlaying);
            StopCommand = new RelayCommand(_ => Stop(), _ => IsMidiLoaded);

            InitializePlaybackTimer();
        }

        /// <summary>
        /// Initializes the DispatcherTimer for simulating playback.
        /// </summary>
        private void InitializePlaybackTimer()
        {
            _playbackTimer = new DispatcherTimer();
            _playbackTimer.Interval = TimeSpan.FromMilliseconds(50); // Update every 50ms
            _playbackTimer.Tick += PlaybackTimer_Tick;
        }

        /// <summary>
        /// Handles the timer tick event during playback.
        /// </summary>
        private void PlaybackTimer_Tick(object sender, EventArgs e)
        {
            // This is a simplified playback. In a real app, you'd use BPM from MIDI
            // and calculate ticks per millisecond for accurate synchronization.
            long ticksPerMillisecond = 10; // Arbitrary value for mock playback speed
            CurrentPlaybackPositionTicks += (long)(_playbackTimer.Interval.TotalMilliseconds * ticksPerMillisecond);

            // Stop if we've passed the last note's end time (or a calculated song end time)
            if (CurrentTabNotes.Any() && CurrentPlaybackPositionTicks > CurrentTabNotes.Max(n => n.StartTimeTicks + n.DurationTicks))
            {
                Stop();
            }
        }

        /// <summary>
        /// Prompts user to select a MIDI file, parses it, and maps it to tablature.
        /// </summary>
        private async Task LoadMidiFile()
        {
            // In a real WPF app, you'd use OpenFileDialog here.
            // For this conceptual code, we'll just use a mock path.
            // Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            // openFileDialog.Filter = "MIDI Files (*.mid)|*.mid|All files (*.*)|*.*";
            // if (openFileDialog.ShowDialog() == true)
            // {
            //     MidiFilePath = openFileDialog.FileName;
            // }
            // else
            // {
            //     StatusMessage = "File selection cancelled.";
            //     return;
            // }

            MidiFilePath = "mock_song.mid"; // Using a mock file path for demonstration
            StatusMessage = "Loading MIDI file...";
            IsMidiLoaded = false;
            CurrentTabNotes.Clear();
            CurrentPlaybackPositionTicks = 0;
            IsPlaying = false;

            try
            {
                List<MidiNote> midiNotes = await _midiParser.ParseMidiFile(MidiFilePath);
                if (midiNotes != null && midiNotes.Any())
                {
                    List<TabNote> tabNotes = _tabMapper.MapMidiNotesToTab(midiNotes);

                    // Clear and add notes to ObservableCollection on the UI thread
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        CurrentTabNotes.Clear();
                        foreach (var note in tabNotes)
                        {
                            CurrentTabNotes.Add(note);
                        }
                    });

                    IsMidiLoaded = true;
                    StatusMessage = $"MIDI file loaded and mapped: {MidiFilePath} ({CurrentTabNotes.Count} notes)";
                }
                else
                {
                    StatusMessage = "No notes found in MIDI file.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error loading MIDI: {ex.Message}";
                Console.WriteLine($"Error: {ex}");
            }
        }

        /// <summary>
        /// Starts playback of the tablature.
        /// </summary>
        private void Play()
        {
            if (IsMidiLoaded && !IsPlaying)
            {
                IsPlaying = true;
                _playbackTimer.Start();
                StatusMessage = "Playing...";
            }
        }

        /// <summary>
        /// Pauses playback.
        /// </summary>
        private void Pause()
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                _playbackTimer.Stop();
                StatusMessage = "Paused.";
            }
        }

        /// <summary>
        /// Stops playback and resets the position.
        /// </summary>
        private void Stop()
        {
            if (IsMidiLoaded)
            {
                IsPlaying = false;
                _playbackTimer.Stop();
                CurrentPlaybackPositionTicks = 0; // Reset playback position
                StatusMessage = "Stopped.";
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Helper method to set property value and raise PropertyChanged event.
        /// </summary>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

10. Services/MidiParser.cs
Path: Services/MidiParser.cs

Purpose: Responsible for parsing MIDI files and extracting raw note data. This is a crucial service that converts the complex binary MIDI format into a structured collection of MidiNote objects that the application can understand and process. (Note: The provided implementation is a mock and would require a dedicated MIDI parsing library for real-world use).

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TABFRET
{
    /// <summary>
    /// Parses MIDI files and extracts note data.
    /// NOTE: This is a MOCK implementation. A real MIDI parser would require
    /// a dedicated library (e.g., DryWetMidi, NAudio) or extensive byte-level parsing.
    /// </summary>
    public class MidiParser
    {
        // For a real implementation, you'd define constants like:
        // private const int MIDI_HEADER_CHUNK_ID = 0x4D546864; // MThd
        // private const int MIDI_TRACK_CHUNK_ID = 0x4D54726B;  // MTrk

        /// <summary>
        /// Parses a MIDI file and returns a list of MidiNote objects.
        /// </summary>
        /// <param name="filePath">The path to the MIDI file.</param>
        /// <returns>A list of parsed MIDI note events.</returns>
        public async Task<List<MidiNote>> ParseMidiFile(string filePath)
        {
            // In a real application, you would use a MIDI library here.
            // Example (conceptual, using a hypothetical library):
            // var midiFile = MidiFile.Read(filePath);
            // var notes = midiFile.GetNotes(); // Simplified
            // return notes.Select(n => new MidiNote { ... }).ToList();

            Console.WriteLine($"Mocking MIDI parsing for: {filePath}");

            // --- MOCK DATA GENERATION ---
            // This simulates parsing a simple melody or chord progression.
            var parsedNotes = new List<MidiNote>();
            long currentTime = 0;
            long quarterNoteTicks = 480; // Assuming 480 ticks per quarter note

            // C Major Chord (C4, E4, G4)
            parsedNotes.Add(new MidiNote { MidiNoteNumber = 60, StartTimeTicks = currentTime, DurationTicks = quarterNoteTicks * 2, Velocity = 100, TrackNumber = 0 }); // C4
            parsedNotes.Add(new MidiNote { MidiNoteNumber = 64, StartTimeTicks = currentTime, DurationTicks = quarterNoteTicks * 2, Velocity = 100, TrackNumber = 0 }); // E4
            parsedNotes.Add(new MidiNote { MidiNoteNumber = 67, StartTimeTicks = currentTime, DurationTicks = quarterNoteTicks * 2, Velocity = 100, TrackNumber = 0 }); // G4
            currentTime += quarterNoteTicks * 2;

            // D Minor Chord (D4, F4, A4)
            parsedNotes.Add(new MidiNote { MidiNoteNumber = 62, StartTimeTicks = currentTime, DurationTicks = quarterNoteTicks * 2, Velocity = 95, TrackNumber = 0 }); // D4
            parsedNotes.Add(new MidiNote { MidiNoteNumber = 65, StartTimeTicks = currentTime, DurationTicks = quarterNoteTicks * 2, Velocity = 95, TrackNumber = 0 }); // F4
            parsedNotes.Add(new MidiNote { MidiNoteNumber = 69, StartTimeTicks = currentTime, DurationTicks = quarterNoteTicks * 2, Velocity = 95, TrackNumber = 0 }); // A4
            currentTime += quarterNoteTicks * 2;

            // Simple Melody (C5, B4, A4, G4)
            parsedNotes.Add(new MidiNote { MidiNoteNumber = 72, StartTimeTicks = currentTime, DurationTicks = quarterNoteTicks, Velocity = 110, TrackNumber = 1 }); // C5
            currentTime += quarterNoteTicks;
            parsedNotes.Add(new MidiNote { MidiNoteNumber = 71, StartTimeTicks = currentTime, DurationTicks = quarterNoteTicks, Velocity = 105, TrackNumber = 1 }); // B4
            currentTime += quarterNoteTicks;
            parsedNotes.Add(new MidiNote { MidiNoteNumber = 69, StartTimeTicks = currentTime, DurationTicks = quarterNoteTicks, Velocity = 100, TrackNumber = 1 }); // A4
            currentTime += quarterNoteTicks;
            parsedNotes.Add(new MidiNote { MidiNoteNumber = 67, StartTimeTicks = currentTime, DurationTicks = quarterNoteTicks, Velocity = 98, TrackNumber = 1 }); // G4
            currentTime += quarterNoteTicks;

            // Simulate some asynchronous work
            await Task.Delay(500); // Simulate file reading time

            return parsedNotes;
        }
    }
}

11. Services/TabMapper.cs
Path: Services/TabMapper.cs

Purpose: This service is responsible for converting MIDI notes into guitar tablature, assigning them to appropriate strings and frets. It contains the core logic for guitar transcription, including a basic strategy for mapping notes to playable positions.

using System;
using System.Collections.Generic;
using System.Linq;

namespace TABFRET
{
    /// <summary>
    /// Defines the standard guitar strings and their open MIDI note numbers.
    /// </summary>
    public enum GuitarString
    {
        HighE = 1, // MIDI Note 64 (E4)
        B = 2,     // MIDI Note 59 (B3)
        G = 3,     // MIDI Note 55 (G3)
        D = 4,     // MIDI Note 50 (D3)
        A = 5,     // MIDI Note 45 (A2)
        LowE = 6   // MIDI Note 40 (E2)
    }

    /// <summary>
    /// Represents a single note on the guitar tablature.
    /// </summary>
    public struct TabNote
    {
        public int StringNumber { get; set; } // 1 (High E) to 6 (Low E)
        public int FretNumber { get; set; }   // 0 for open string
        public long StartTimeTicks { get; set; } // From original MIDI event
        public long DurationTicks { get; set; }  // From original MIDI event
        public int OriginalMidiNoteNumber { get; set; } // For reference

        public override string ToString()
        {
            return $"String: {StringNumber}, Fret: {FretNumber}, Start: {StartTimeTicks}";
        }
    }

    /// <summary>
    /// Converts MIDI notes into guitar tablature, assigning them to strings and frets.
    /// </summary>
    public class TabMapper
    {
        // Standard EADGBe tuning MIDI note numbers for open strings
        private static readonly Dictionary<GuitarString, int> StandardTuningMidiNotes = new Dictionary<GuitarString, int>
        {
            { GuitarString.HighE, 64 }, // E4
            { GuitarString.B, 59 },     // B3
            { GuitarString.G, 55 },     // G3
            { GuitarString.D, 50 },     // D3
            { GuitarString.A, 45 },     // A2
            { GuitarString.LowE, 40 }   // E2
        };

        private const int MaxFret = 24; // Maximum playable fret to consider

        /// <summary>
        /// Maps a list of MIDI note events to guitar tablature notes.
        /// </summary>
        /// <param name="midiNotes">The list of parsed MIDI note events.</param>
        /// <param name="tuning">The guitar tuning to use for mapping (currently only supports standard).</param>
        /// <returns>A list of TabNote objects.</returns>
        public List<TabNote> MapMidiNotesToTab(List<MidiNote> midiNotes, Dictionary<GuitarString, int> tuning = null)
        {
            var tabNotes = new List<TabNote>();
            var currentTuning = tuning ?? StandardTuningMidiNotes; // Use standard if no tuning provided

            // Sort notes by start time to process them chronologically
            midiNotes = midiNotes.OrderBy(n => n.StartTimeTicks).ToList();

            foreach (var midiNote in midiNotes)
            {
                TabNote? bestTabNote = null;
                int lowestFret = int.MaxValue;

                // Iterate through strings from High E (1) to Low E (6)
                // This prioritizes higher strings for the same note, which is often desirable.
                // You might reverse this or add more complex logic for different fingering preferences.
                foreach (var stringEntry in currentTuning.OrderBy(s => (int)s.Key))
                {
                    int stringNumber = (int)stringEntry.Key;
                    int openStringMidiNote = stringEntry.Value;

                    // Calculate the fret number for this MIDI note on the current string
                    int fret = midiNote.MidiNoteNumber - openStringMidiNote;

                    // Check if the fret is valid and within the playable range
                    if (fret >= 0 && fret <= MaxFret)
                    {
                        // Simple strategy: pick the lowest fret possible for a note.
                        // For more advanced mapping, you'd consider:
                        // - Keeping notes on the same string for melodic lines.
                        // - Avoiding awkward stretches for chords.
                        // - Prioritizing open strings.
                        if (fret < lowestFret)
                        {
                            lowestFret = fret;
                            bestTabNote = new TabNote
                            {
                                StringNumber = stringNumber,
                                FretNumber = fret,
                                StartTimeTicks = midiNote.StartTimeTicks,
                                DurationTicks = midiNote.DurationTicks,
                                OriginalMidiNoteNumber = midiNote.MidiNoteNumber
                            };
                        }
                    }
                }

                if (bestTabNote.HasValue)
                {
                    tabNotes.Add(bestTabNote.Value);
                }
                else
                {
                    Console.WriteLine($"Warning: Could not map MIDI note {midiNote.MidiNoteNumber} to a playable fret/string.");
                }
            }

            return tabNotes;
        }

        /// <summary>
        /// Returns the MIDI note number for a given open guitar string.
        /// </summary>
        public static int GetOpenStringMidiNote(GuitarString guitarString)
        {
            return StandardTuningMidiNotes[guitarString];
        }
    }
}

12. Utils/FretboardHelper.cs
Path: Utils/FretboardHelper.cs

Purpose: A static helper class that provides utility functions for calculating positions on the visual fretboard (e.g., X-coordinate for frets, Y-coordinate for strings) and other related musical calculations like MIDI note to frequency conversion.

using System;
using System.Windows; // For Point, Rect (if used)

namespace TABFRET
{
    /// <summary>
    /// Contains utility functions for fretboard calculations and MIDI-to-fret positioning.
    /// </summary>
    public static class FretboardHelper
    {
        // Constants for musical calculations
        private const double A4_FREQUENCY = 440.0; // Frequency of A4
        private const int A4_MIDINOTE = 69;       // MIDI note number for A4
        private const double TWELFTH_ROOT_OF_2 = 1.0594635; // 2^(1/12)

        // Guitar neck visual constants (adjust as needed for your UI)
        private const double NUT_TO_BRIDGE_LENGTH_VISUAL = 1000.0; // A conceptual length for proportional calculations

        /// <summary>
        /// Calculates the horizontal (X) position of a given fret on the visual fretboard.
        /// Uses a logarithmic scale to simulate frets getting closer together.
        /// </summary>
        /// <param name="fretNumber">The fret number (0 for nut).</param>
        /// <param name="canvasWidth">The total width of the drawing canvas.</param>
        /// <param name="totalFretsToDisplay">The number of frets you intend to display on the canvas.</param>
        /// <returns>The X-coordinate for the fret line.</returns>
        public static double GetXPositionForFret(int fretNumber, double canvasWidth, int totalFretsToDisplay)
        {
            // This is a simplified logarithmic scale.
            // A more accurate calculation might use the 18th rule or similar.
            double scaleFactor = canvasWidth / NUT_TO_BRIDGE_LENGTH_VISUAL;
            double fretDistance = NUT_TO_BRIDGE_LENGTH_VISUAL * (1 - Math.Pow(0.5, fretNumber / 12.0)); // Simplified, not exact
            // A more common formula for fret spacing:
            // double distance = NUT_TO_BRIDGE_LENGTH_VISUAL - (NUT_TO_BRIDGE_LENGTH_VISUAL / Math.Pow(TWELFTH_ROOT_OF_2, fretNumber));
            // return distance * (canvasWidth / NUT_TO_BRIDGE_LENGTH_VISUAL); // Scale to canvas width

            // For simplicity and visual consistency across a limited number of frets,
            // we'll use a more linear distribution for the displayed frets,
            // but still with a slight compression.
            double proportionalPosition = (double)fretNumber / totalFretsToDisplay;
            return canvasWidth * (proportionalPosition * 0.95 + (fretNumber > 0 ? 0.05 : 0)); // Slight offset and compression

            // Or, a simple linear distribution for demonstration:
            // return (double)fretNumber / totalFretsToDisplay * canvasWidth;
        }

        /// <summary>
        /// Calculates the vertical (Y) position for a given guitar string.
        /// </summary>
        /// <param name="stringNumber">The string number (1 for High E, 6 for Low E).</param>
        /// <param name="canvasHeight">The total height of the drawing canvas.</param>
        /// <returns>The Y-coordinate for the string line.</returns>
        public static double GetYPositionForString(int stringNumber, double canvasHeight)
        {
            // Distribute strings evenly across the canvas height
            double stringSpacing = canvasHeight / 7; // 6 strings + padding
            return stringSpacing * stringNumber; // String 1 at stringSpacing, String 6 at stringSpacing * 6
        }

        /// <summary>
        /// Converts a MIDI note number to its corresponding frequency in Hertz (Hz).
        /// </summary>
        /// <param name="midiNoteNumber">The MIDI note number.</param>
        /// <returns>The frequency in Hz.</returns>
        public static double MidiToFrequency(int midiNoteNumber)
        {
            return A4_FREQUENCY * Math.Pow(TWELFTH_ROOT_OF_2, midiNoteNumber - A4_MIDINOTE);
        }

        /// <summary>
        /// Converts a frequency in Hertz (Hz) to the closest MIDI note number.
        /// </summary>
        /// <param name="frequency">The frequency in Hz.</param>
        /// <returns>The closest MIDI note number.</returns>
        public static int FrequencyToMidi(double frequency)
        {
            return (int)Math.Round(A4_MIDINOTE + (12 * Math.Log(frequency / A4_FREQUENCY, 2)));
        }

        /// <summary>
        /// Gets the standard musical notation for a MIDI note number (e.g., "C#4").
        /// </summary>
        /// <param name="midiNoteNumber">The MIDI note number.</param>
        /// <returns>The note name string.</returns>
        public static string GetMidiNoteName(int midiNoteNumber)
        {
            string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            int octave = (midiNoteNumber / 12) - 1; // MIDI 0 is C-1
            int noteIndex = midiNoteNumber % 12;
            return $"{noteNames[noteIndex]}{octave}";
        }
    }
}

13. Config/settings.json
Path: Config/settings.json

Purpose: A JSON file intended to store user-configurable settings and preferences for the application. This allows for easy modification of various parameters without recompiling the code.

{
  "GeneralSettings": {
    "DefaultMidiFolder": "",
    "AutoLoadLastMidi": false
  },
  "VisualSettings": {
    "NoteHighlightColor": "#FFFF0000",
    "FretboardBackgroundColor": "#FFD3D3D3",
    "ShowFretNumbers": true
  },
  "PlaybackSettings": {
    "PlaybackSpeedMultiplier": 1.0,
    "MetronomeEnabled": false
  },
  "GuitarTuning": {
    "DefaultTuning": "StandardEADGBe"
  }
}
