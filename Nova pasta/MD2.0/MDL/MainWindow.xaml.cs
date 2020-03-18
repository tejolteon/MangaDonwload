using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace MDR
{
    using MDR.Source.Utils;
    using MDR.Source;
    using Source.Reader;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Reader reader;
        bool dual = false;
        bool isCtrlPressed = false;

        private void Init()
        {
            TransformGroup group = new TransformGroup();
            ScaleTransform st = new ScaleTransform();
            group.Children.Add(st);
            TranslateTransform tt = new TranslateTransform();
            group.Children.Add(tt);
            gridAllImage.RenderTransform = group;

            OpenFileDialog dialog = new OpenFileDialog { Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png" };

            if (dialog.ShowDialog() == true)
            {
                reader = new Reader();

                reader.Loader(dialog.FileName);

                Next();

                if (dual)
                {
                    dual = false;
                    ConfigDualPage();
                }
                else
                {
                    dual = true;
                    ConfigSinglePage();
                }
            }
        }

        public void Next()
        {
            reader.NextImage();

            if (!(reader.Image is null))
            {
                imageRight.Source = reader.Image;
                Reset();
            }
            else
                return;

            if (dual)
            {
                reader.NextImage();
                imageLeft.Source = reader.Image;
            }
        }

        public void Previous()
        {
            if (dual)
            {
                reader.PreviousImage();
                reader.PreviousImage();
            }

            reader.PreviousImage();
            if (!(reader.Image is null))
            {
                imageRight.Source = reader.Image;
                Reset();
            }

            if (dual)
            {
                reader.NextImage();
                imageLeft.Source = reader.Image;
            }
        }

        public void ConfigSinglePage()
        {
            if (dual)
            {
                dual = false;
                gridAllImage.ColumnDefinitions.Clear();

                gridImageRight.HorizontalAlignment = HorizontalAlignment.Center;
                imageRight.HorizontalAlignment = HorizontalAlignment.Center;
                imageLeft.Source = null;

                Previous();
            }
        }

        public void ConfigDualPage()
        {
            if (!dual)
            {
                gridAllImage.ColumnDefinitions.Clear();

                var col = new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                };
                var col2 = new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                };

                gridAllImage.ColumnDefinitions.Add(col);
                gridAllImage.ColumnDefinitions.Add(col2);

                gridImageLeft.HorizontalAlignment = HorizontalAlignment.Right;
                imageRight.HorizontalAlignment = HorizontalAlignment.Right;
                gridImageRight.HorizontalAlignment = HorizontalAlignment.Left;
                imageLeft.HorizontalAlignment = HorizontalAlignment.Left;

                if (!(reader.CurrentPage % 2 == 0))
                    reader.SetPosition(-2);
                else if (reader.CurrentPage >= 0)
                    reader.SetPosition(-1);

                dual = true;
                Next();
            }
        }

        private void Win_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                if (!(imageRight.Source is null))
                    Next();
            }
            else if (e.Key == Key.Left)
            {
                if (!(imageRight.Source is null))
                    Previous();
            }

            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                isCtrlPressed = true;
        }

        private void Win_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                isCtrlPressed = false;
        }

        private void MitSingle_Click(object sender, RoutedEventArgs e)
        {
            ConfigSinglePage();
        }

        private void MitDual_Click(object sender, RoutedEventArgs e)
        {
            ConfigDualPage();
        }

        private void MitOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void SldZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Zoom.SliderZoom(gridAllImage, e);
        }

        private void GridAllImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (isCtrlPressed)
                sldZoom.Value = Zoom.MouseZoom(gridAllImage, e);
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        void Reset()
        {
            sldZoom.Value = 1;
            Zoom.Reset(gridAllImage);
        }

        private void GridAllImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double w = 0;
            double h = 0;
            double ht = 0;

            if (!(imageLeft is null))
            {
                w += imageLeft.ActualWidth * sldZoom.Value;
                ht = imageLeft.ActualHeight * sldZoom.Value;

                if (h < ht * ((sldZoom.Value - 1) / sldZoom.Value))
                    h = ht * ((sldZoom.Value - 1) / sldZoom.Value);
            }

            if (!(imageRight is null))
            {
                w += imageRight.ActualWidth * sldZoom.Value;
                ht = imageRight.ActualHeight * sldZoom.Value;

                if (h < ht * ((sldZoom.Value - 1) / sldZoom.Value))
                    h = ht * ((sldZoom.Value - 1) / sldZoom.Value);
            }

            var imgp = new Vector(w, h);

            Zoom.ClickMoveDown(gridAllImage, e.GetPosition(grdMain), imgp);
            gridAllImage.CaptureMouse();
        }

        private void GridAllImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            gridAllImage.ReleaseMouseCapture();
        }

        private void GridAllImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (gridAllImage.IsMouseCaptured)
                Zoom.Move(gridAllImage, e.GetPosition(grdMain));
        }
    }
}
