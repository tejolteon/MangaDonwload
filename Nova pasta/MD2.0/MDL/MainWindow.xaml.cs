using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace MDR
{
    using MDR.Source.Utils;
    using Source.Reader;
    using System.Windows.Controls;

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

        private void Init()
        {
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
                imageLeft.Source = reader.Image;
            else
                return;

            if (dual)
            {
                reader.NextImage();
                imageRight.Source = reader.Image;
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
                imageLeft.Source = reader.Image;

            if (dual)
            {
                reader.NextImage();
                imageRight.Source = reader.Image;
            }
        }

        public void ConfigSinglePage()
        {
            if (dual)
            {
                dual = false;
                gridAllImage.ColumnDefinitions.Clear();

                gridImageLeft.HorizontalAlignment = HorizontalAlignment.Center;
                imageLeft.HorizontalAlignment = HorizontalAlignment.Center;
                imageRight.Source = null;

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
                imageLeft.HorizontalAlignment = HorizontalAlignment.Right;
                gridImageRight.HorizontalAlignment = HorizontalAlignment.Left;
                imageRight.HorizontalAlignment = HorizontalAlignment.Left;

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
                Next();
            }
            else if (e.Key == Key.Left)
            {
                Previous();
            }

            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
               ZoomBorder.IsCtrlPressed = true;
        } 

        private void Win_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                ZoomBorder.IsCtrlPressed = false;
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
    }
}
