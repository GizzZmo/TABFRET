using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using TABFRET.Models;

namespace TABFRET.Views
{
    public partial class GuitarNeckView : UserControl
    {
        public static readonly DependencyProperty TabNotesProperty =
            DependencyProperty.Register("TabNotes", typeof(ObservableCollection<TabNote>), typeof(GuitarNeckView),
                new PropertyMetadata(null, OnTabNotesChanged));

        public ObservableCollection<TabNote> TabNotes
        {
            get => (ObservableCollection<TabNote>)GetValue(TabNotesProperty);
            set => SetValue(TabNotesProperty, value);
        }

        public GuitarNeckView()
        {
            InitializeComponent();
        }

        private static void OnTabNotesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GuitarNeckView control)
            {
                if (e.OldValue is ObservableCollection<TabNote> oldCol)
                    oldCol.CollectionChanged -= control.TabNotes_CollectionChanged;
                if (e.NewValue is ObservableCollection<TabNote> newCol)
                    newCol.CollectionChanged += control.TabNotes_CollectionChanged;
                control.RenderFretboard();
            }
        }

        private void TabNotes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RenderFretboard();
        }

        private void RenderFretboard()
        {
            FretboardCanvas.Children.Clear();
            int strings = 6;
            int frets = 21;
            double width = FretboardCanvas.ActualWidth == 0 ? 800 : FretboardCanvas.ActualWidth;
            double height = FretboardCanvas.ActualHeight == 0 ? 200 : FretboardCanvas.ActualHeight;

            // Draw strings (horizontal lines)
            for (int s = 0; s < strings; s++)
            {
                double y = height / (strings - 1) * s;
                var line = new Line
                {
                    X1 = 0, Y1 = y,
                    X2 = width, Y2 = y,
                    Stroke = Brushes.Gray,
                    StrokeThickness = 2
                };
                FretboardCanvas.Children.Add(line);
            }

            // Draw frets (vertical lines)
            for (int f = 0; f < frets; f++)
            {
                double x = width / (frets - 1) * f;
                var line = new Line
                {
                    X1 = x, Y1 = 0,
                    X2 = x, Y2 = height,
                    Stroke = Brushes.DarkGoldenrod,
                    StrokeThickness = (f == 0) ? 4 : 1
                };
                FretboardCanvas.Children.Add(line);
            }

            // Draw notes
            if (TabNotes != null)
            {
                foreach (var note in TabNotes)
                {
                    if (note.FretNumber < 0 || note.FretNumber >= frets) continue;
                    if (note.StringNumber < 1 || note.StringNumber > strings) continue;
                    double x = width / (frets - 1) * note.FretNumber;
                    double y = height / (strings - 1) * (note.StringNumber - 1);
                    var ellipse = new Ellipse
                    {
                        Width = 20,
                        Height = 20,
                        Fill = Brushes.Crimson,
                        Stroke = Brushes.Black,
                        StrokeThickness = 2
                    };
                    Canvas.SetLeft(ellipse, x - 10);
                    Canvas.SetTop(ellipse, y - 10);
                    FretboardCanvas.Children.Add(ellipse);
                }
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            RenderFretboard();
        }
    }
}
