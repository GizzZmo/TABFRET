using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TABFRET.Models;
using TABFRET.Services;

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

        public MainViewModel(IMidiParser midiParser, ISettingsService settingsService)
        {
            _midiParser = midiParser;
            _settingsService = settingsService;

            LoadMidiCommand = new RelayCommand(async _ => await LoadMidiAsync());
            SaveSettingsCommand = new RelayCommand(async _ => await SaveSettingsAsync());
        }

        private async Task LoadMidiAsync()
        {
            // Use a dialog service to get the file path in production.
            string filePath = "choose with dialog";
            var notes = await _midiParser.ParseMidiFileAsync(filePath);
            MidiNotes.Clear();
            foreach (var note in notes) MidiNotes.Add(note);
        }

        public async Task LoadSettingsAsync() => Settings = await _settingsService.LoadAsync();
        public async Task SaveSettingsAsync() => await _settingsService.SaveAsync(Settings);
    }
}
