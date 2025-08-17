using System;
using System.Windows.Controls;
using System.Linq;
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

        public static readonly DependencyProperty HighlightedTabNotesProperty =
            DependencyProperty.Register("HighlightedTabNotes", typeof(ObservableCollection<TabNote>), typeof(GuitarNeckView),
                new PropertyMetadata(null, OnHighlightedTabNotesChanged));

        public ObservableCollection<TabNote> TabNotes
        {
            get => (ObservableCollection<TabNote>)GetValue(TabNotesProperty);
            set => SetValue(TabNotesProperty, value);
        }

        public ObservableCollection<TabNote> HighlightedTabNotes
        {
            get => (ObservableCollection<TabNote>)GetValue(HighlightedTabNotesProperty);
            set => SetValue(HighlightedTabNotesProperty, value);
        }

        private ScaleTransform _zoomTransform = new ScaleTransform(1.0, 1.0);
        private TranslateTransform _panTransform = new TranslateTransform(0, 0);
        private TransformGroup _transformGroup = new TransformGroup();

        public GuitarNeckView()
        {
            InitializeComponent();
            _transformGroup.Children.Add(_zoomTransform);
            _transformGroup.Children.Add(_panTransform);
            if (FretboardCanvas != null)
            {
                FretboardCanvas.RenderTransform = _transformGroup;
                FretboardCanvas.MouseWheel += FretboardCanvas_MouseWheel;
                FretboardCanvas.MouseLeftButtonDown += FretboardCanvas_MouseLeftButtonDown;
                FretboardCanvas.MouseMove += FretboardCanvas_MouseMove;
                FretboardCanvas.MouseLeftButtonUp += FretboardCanvas_MouseLeftButtonUp;
            }
        }

        private Point _lastMousePosition;
        private bool _isPanning = false;

        private void FretboardCanvas_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            double zoom = e.Delta > 0 ? 1.1 : 0.9;
            _zoomTransform.ScaleX *= zoom;
            _zoomTransform.ScaleY *= zoom;
        }

        private void FretboardCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _isPanning = true;
            _lastMousePosition = e.GetPosition(FretboardCanvas);
            FretboardCanvas.CaptureMouse();
        }

        private void FretboardCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_isPanning)
            {
                Point currentPosition = e.GetPosition(FretboardCanvas);
                Vector delta = currentPosition - _lastMousePosition;
                _panTransform.X += delta.X;
                _panTransform.Y += delta.Y;
                _lastMousePosition = currentPosition;
            }
        }

        private void FretboardCanvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _isPanning = false;
            FretboardCanvas.ReleaseMouseCapture();
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

        private static void OnHighlightedTabNotesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GuitarNeckView control)
            {
                if (e.OldValue is ObservableCollection<TabNote> oldCol)
                    oldCol.CollectionChanged -= control.HighlightedTabNotes_CollectionChanged;
                if (e.NewValue is ObservableCollection<TabNote> newCol)
                    newCol.CollectionChanged += control.HighlightedTabNotes_CollectionChanged;
                control.RenderFretboard();
            }
        }

        private void TabNotes_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            RenderFretboard();
        }

        private void HighlightedTabNotes_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            RenderFretboard();
        }

        private void RenderFretboard()
        {
            if (FretboardCanvas == null)
                return;
            try
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

                // Draw notes in batches for performance
                if (TabNotes != null)
                {
                    // NOTE: TabNote should implement Equals/GetHashCode for correct highlighting
                    var notesToDraw = TabNotes.Count > 500 ? TabNotes.Take(500) : TabNotes;
                    foreach (var note in notesToDraw)
                    {
                        if (note.FretNumber < 0 || note.FretNumber >= frets) continue;
                        if (note.StringNumber < 1 || note.StringNumber > strings) continue;
                        double x = width / (frets - 1) * note.FretNumber;
                        double y = height / (strings - 1) * (note.StringNumber - 1);

                        bool isHighlighted = HighlightedTabNotes != null && HighlightedTabNotes.Contains(note);
                        var ellipse = new Ellipse
                        {
                            Width = 20,
                            Height = 20,
                            Fill = isHighlighted ? Brushes.LimeGreen : Brushes.Crimson,
                            Stroke = Brushes.Black,
                            StrokeThickness = 2
                        };
                        Canvas.SetLeft(ellipse, x - 10);
                        Canvas.SetTop(ellipse, y - 10);
                        FretboardCanvas.Children.Add(ellipse);
                    }
                }
            }
            catch (Exception)
            {
                // Optionally log exception for diagnostics
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            RenderFretboard();
        }
    }
}
