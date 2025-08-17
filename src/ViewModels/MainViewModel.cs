
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TABFRET.Models;
using TABFRET.Services;
using TABFRET.Utils;



namespace TABFRET.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMidiParser _midiParser;
        private readonly ISettingsService _settingsService;

        private ObservableCollection<MidiNote> _midiNotes = new();
        public ObservableCollection<MidiNote> MidiNotes
        {
            get => _midiNotes;
            set => SetProperty(ref _midiNotes, value);
        }

        private UserSettings _settings = new();
        public UserSettings Settings
        {
            get => _settings;
            set => SetProperty(ref _settings, value);
        }

        public ICommand LoadMidiCommand { get; }
        public ICommand SaveSettingsCommand { get; }
        public ICommand PlayCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand ResumeCommand { get; }

        private double _playbackSpeed = 1.0;
        public double PlaybackSpeed
        {
            get => _playbackSpeed;
            set => SetProperty(ref _playbackSpeed, value);
        }

        private bool _isMetronomeOn = false;
        public bool IsMetronomeOn
        {
            get => _isMetronomeOn;
            set => SetProperty(ref _isMetronomeOn, value);
        }

        public string StatusMessage { get; set; } = string.Empty; // Properly initialized property for UI feedback
        private bool _isLoading = false;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private ObservableCollection<string> _recentFiles = new();
        public ObservableCollection<string> RecentFiles
        {
            get => _recentFiles;
            set => SetProperty(ref _recentFiles, value);
        }

    private string? _tuningString;
        public string TuningString
        {
            get => _tuningString ?? string.Join(",", Settings.Tuning);
            set
            {
                if (SetProperty(ref _tuningString, value))
                {
                    // Validate and update Settings.Tuning
                    var parts = value.Split(',');
                    var newTuning = new System.Collections.Generic.List<int>();
                    bool valid = true;
                    foreach (var part in parts)
                    {
                        if (int.TryParse(part.Trim(), out int midiNote))
                            newTuning.Add(midiNote);
                        else
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (valid && newTuning.Count > 0)
                    {
                        Settings.Tuning = newTuning;
                        StatusMessage = "Tuning updated.";
                    }
                    else
                    {
                        StatusMessage = "Invalid tuning input. Please enter comma-separated MIDI note numbers.";
                    }
                }
            }
        }

        private PlaybackEngine _playbackEngine = new PlaybackEngine();

        public MainViewModel(IMidiParser midiParser, ISettingsService settingsService)
        {
            _midiParser = midiParser;
            _settingsService = settingsService;

            LoadMidiCommand = new RelayCommand(async _ => await LoadMidiAsync());
            SaveSettingsCommand = new RelayCommand(async _ => await SaveSettingsAsync());
            PlayCommand = new RelayCommand(_ => Play(), _ => !_playbackEngine.IsPlaying);
            PauseCommand = new RelayCommand(_ => Pause(), _ => _playbackEngine.IsPlaying);
            ResumeCommand = new RelayCommand(_ => Resume(), _ => !_playbackEngine.IsPlaying);

            _playbackEngine.PlaybackStopped += () => StatusMessage = "Playback stopped.";
        }

        private void Play()
        {
            _playbackEngine.SetBpm(Settings != null ? Settings.NumberOfStrings : 120); // Example, replace with actual BPM property
            _playbackEngine.Play();
            StatusMessage = "Playback started.";
        }

        private void Pause()
        {
            _playbackEngine.Stop();
            StatusMessage = "Playback paused.";
        }

        private void Resume()
        {
            _playbackEngine.Play();
            StatusMessage = "Playback resumed.";
        }

        private async Task LoadMidiAsync(string? filePath = null)
        {
            IsLoading = true;
            try
            {
                // Use a dialog service to get the file path in production.
                if (string.IsNullOrEmpty(filePath))
                {
                    filePath = "choose with dialog";
                }
                Logger.Log($"Loading MIDI file: {filePath}");
                var notes = await _midiParser.ParseMidiFileAsync(filePath);
                MidiNotes = new ObservableCollection<MidiNote>(notes); // Batch add for performance
                StatusMessage = "MIDI file loaded successfully.";
                Logger.Log($"MIDI file loaded: {filePath}");
                if (!RecentFiles.Contains(filePath))
                {
                    RecentFiles.Insert(0, filePath);
                    // Limit recent files to 10 entries
                    while (RecentFiles.Count > 10)
                        RecentFiles.RemoveAt(RecentFiles.Count - 1);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error loading MIDI: {ex.Message}";
                Logger.Log($"Error loading MIDI: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task LoadSettingsAsync()
        {
            IsLoading = true;
            try
            {
                Settings = await _settingsService.LoadAsync();
                StatusMessage = "Settings loaded successfully.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error loading settings: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task SaveSettingsAsync()
        {
            IsLoading = true;
            try
            {
                await _settingsService.SaveAsync(Settings);
                StatusMessage = "Settings saved successfully.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error saving settings: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
