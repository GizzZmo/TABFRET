using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TABFRET.Models;
using TABFRET.Services;
namespace TABFRET.ViewModels;
  

public class MainViewModel : ViewModelBase
{
    private readonly IMidiParser _midiParser;
    private readonly ISettingsService _settingsService;

    public ObservableCollection<MidiNote> MidiNotes { get; set; } = new();
    public UserSettings Settings { get; private set; } = new();

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
        // Use dialog service or inject file path for testability
        string filePath = "choose with dialog";
        var notes = await _midiParser.ParseMidiFileAsync(filePath);
        MidiNotes.Clear();
        foreach (var note in notes) MidiNotes.Add(note);
    }

    public async Task LoadSettingsAsync() => Settings = await _settingsService.LoadAsync();
    public async Task SaveSettingsAsync() => await _settingsService.SaveAsync(Settings);
}
