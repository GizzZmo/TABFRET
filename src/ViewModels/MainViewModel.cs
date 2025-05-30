using System.Collections.ObjectModel;
using TABFRET.Models;
using TABFRET.Services;
using TABFRET.Utils;
using Microsoft.Win32;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;

namespace TABFRET.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly MidiParser _midiParser = new MidiParser();
        private readonly TabMapper _tabMapper = new TabMapper();
        private MidiPlaybackService _playbackService = new MidiPlaybackService();

        public ObservableCollection<TabNote> TabNotes { get; } = new ObservableCollection<TabNote>();
        public ObservableCollection<MidiNote> MidiNotes { get; } = new ObservableCollection<MidiNote>();

        private string _currentFilePath;
        public string CurrentFilePath
        {
            get => _currentFilePath;
            set { _currentFilePath = value; OnPropertyChanged(nameof(CurrentFilePath)); }
        }

        private int _transposeSemitones = 0;
        public int TransposeSemitones
        {
            get => _transposeSemitones;
            set
            {
                _transposeSemitones = value;
                OnPropertyChanged(nameof(TransposeSemitones));
                UpdateTabNotes();
            }
        }

        public ICommand LoadMidiCommand { get; }
        public ICommand PlaybackCommand { get; }
        public ICommand TransposeUpCommand { get; }
        public ICommand TransposeDownCommand { get; }

        public MainViewModel()
        {
            LoadMidiCommand = new RelayCommand(_ => LoadMidiFile());
            PlaybackCommand = new RelayCommand(_ => PlayMidi());
            TransposeUpCommand = new RelayCommand(_ => { TransposeSemitones++; });
            TransposeDownCommand = new RelayCommand(_ => { TransposeSemitones--; });
        }

        public void LoadMidiFile()
        {
            var dlg = new OpenFileDialog
            {
                Filter = "MIDI files (*.mid)|*.mid",
                Title = "Open MIDI File"
            };
            if (dlg.ShowDialog() == true)
            {
                CurrentFilePath = dlg.FileName;
                var loaded = _midiParser.LoadMidiFile(CurrentFilePath);
                if (!loaded)
                {
                    MessageBox.Show(_midiParser.ErrorMessage, "MIDI Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MidiNotes.Clear();
                foreach (var note in _midiParser.Notes)
                    MidiNotes.Add(note);
                UpdateTabNotes();
            }
        }

        private void UpdateTabNotes()
        {
            var notes = new List<MidiNote>(_midiParser.Notes);
            if (TransposeSemitones != 0)
            {
                foreach (var note in notes)
                    note.NoteNumber = FretboardHelper.Transpose(note.NoteNumber, TransposeSemitones);
            }
            var tabNotes = _tabMapper.MapMidiNotesToTab(notes);
            TabNotes.Clear();
            foreach (var tabNote in tabNotes)
                TabNotes.Add(tabNote);

            _playbackService.LoadMidiEvents(notes, _midiParser.TicksPerQuarterNote);
        }

        public void PlayMidi()
        {
            _playbackService.Play();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class RelayCommand : ICommand
    {
        private readonly System.Action<object> _execute;
        private readonly System.Predicate<object> _canExecute;

        public RelayCommand(System.Action<object> execute, System.Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute ?? (_ => true);
        }

        public bool CanExecute(object parameter) => _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
