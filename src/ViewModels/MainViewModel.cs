using System.Collections.ObjectModel;
using TABFRET.Models;
using TABFRET.Services;
using Microsoft.Win32;
using System.Windows.Input;
using System.ComponentModel;

namespace TABFRET.ViewModels
{
    /// <summary>
    /// Main ViewModel: Handles MIDI parsing, tab mapping, and exposes data to the UI.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly MidiParser _midiParser = new MidiParser();
        private readonly TabMapper _tabMapper = new TabMapper();

        public ObservableCollection<TabNote> TabNotes { get; } = new ObservableCollection<TabNote>();
        public ObservableCollection<MidiNote> MidiNotes { get; } = new ObservableCollection<MidiNote>();

        private string _currentFilePath;
        public string CurrentFilePath
        {
            get => _currentFilePath;
            set { _currentFilePath = value; OnPropertyChanged(nameof(CurrentFilePath)); }
        }

        public ICommand LoadMidiCommand { get; }

        public MainViewModel()
        {
            LoadMidiCommand = new RelayCommand(_ => LoadMidiFile());
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
                _midiParser.LoadMidiFile(CurrentFilePath);

                MidiNotes.Clear();
                foreach (var note in _midiParser.Notes)
                    MidiNotes.Add(note);

                var tabNotes = _tabMapper.MapMidiNotesToTab(_midiParser.Notes);
                TabNotes.Clear();
                foreach (var tabNote in tabNotes)
                    TabNotes.Add(tabNote);

                OnPropertyChanged(nameof(TabNotes));
                OnPropertyChanged(nameof(MidiNotes));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // Simple ICommand implementation for binding
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
