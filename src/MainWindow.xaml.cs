using System.Windows;

namespace TABFRET
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0 && files[0].ToLower().EndsWith(".mid"))
                {
                    // Assuming DataContext is MainViewModel
                    if (DataContext is TABFRET.ViewModels.MainViewModel vm)
                    {
                        // You may want to add a LoadMidiFile(string path) method to MainViewModel
                        vm.StatusMessage = $"MIDI file dropped: {files[0]}";
                        // TODO: Call actual MIDI loading logic
                    }
                }
            }
        }
    }
}
