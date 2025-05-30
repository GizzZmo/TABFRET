using System.Collections.ObjectModel;
using TABFRET.Models;
using TABFRET.Services;
using TABFRET.Utils;
using Microsoft.Win32;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using System.Linq;

namespace TABFRET.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly MidiParser _midiParser = new MidiParser();
        private readonly TabMapper _tabMapper = new TabMapper();
        private MidiPlaybackService _playbackService = new MidiPlaybackService();
        private readonly PlaybackEngine _playbackEngine = new PlaybackEngine();

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

        private long _currentPlaybackTick;
        public long CurrentPlaybackTick
        {
            get => _currentPlaybackTick;
            set { _currentPlaybackTick = value; OnPropertyChanged(nameof(CurrentPlaybackTick)); }
        }

        public ObservableCollection<TabNote> HighlightedTabNotes { get; } = new ObservableCollection<TabNote>();

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

            _playbackEngine.PlaybackTick += OnPlaybackTick;
            _playbackEngine.PlaybackStopped += OnPlaybackStopped;
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
            var notes = _midiParser.Notes.Select(n => new MidiNote
            {
                NoteNumber = FretboardHelper.Transpose(n.NoteNumber, TransposeSemitones),
                StartTimeTicks = n.StartTimeTicks,
                DurationTicks = n.DurationTicks,
                Channel = n.Channel,
                Velocity = n.Velocity
            }).ToList();

            var tabNotes = _tabMapper.MapMidiNotesToTab(notes);
            TabNotes.Clear();
            foreach (var tabNote in tabNotes)
                TabNotes.Add(tabNote);

            _playbackService.LoadMidiEvents(notes, _midiParser.TicksPerQuarterNote);
            _playbackEngine.Load(notes, _midiParser.TicksPerQuarterNote);
        }

        public void PlayMidi()
        {
            _playbackService.Play();

            // Prepare notes for visualization (use the mapped notes to include transposition)
            var midiNotesForPlayback = TabNotes.Select(tn => new MidiNote
            {
                NoteNumber = tn.OriginalMidiNoteNumber,
                StartTimeTicks = tn.StartTimeTicks,
                DurationTicks = tn.DurationTicks,
                Channel = 0,
                Velocity = 100
            }).ToList();

            _playbackEngine.Load(midiNotesForPlayback, _midiParser.TicksPerQuarterNote);
            _playbackEngine.Play();
        }

        private void OnPlaybackTick(long tick)
        {
            CurrentPlaybackTick = tick;
            HighlightedTabNotes.Clear();
            foreach (var note in TabNotes)
            {
                if (tick >= note.StartTimeTicks && tick < note.StartTimeTicks + note.DurationTicks)
                    HighlightedTabNotes.Add(note);
            }
        }

        private void OnPlaybackStopped()
        {
            HighlightedTabNotes.Clear();
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
