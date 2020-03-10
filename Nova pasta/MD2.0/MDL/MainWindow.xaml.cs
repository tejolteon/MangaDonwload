using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace MDR
{
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
            Init();
        }

        Reader reader;
        bool dual = true;

        private void Init()
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png" };

            if (dialog.ShowDialog() == true)
            {
                reader = new Reader();

                reader.Loader(dialog.FileName);

                Next();

                if (dual)
                    ConfigDualPage();
                else
                    ConfigSinglePage();
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

                //if (!(position % 2 == 0))
                //    position -= 2;
                //else if (position >= 0)
                //    position -= 1;
                Previous();
                dual = true;
                Next();
            }
        }

        private void MitSingle_Click(object sender, RoutedEventArgs e)
        {
            ConfigSinglePage();
        }

        private void MitDual_Click(object sender, RoutedEventArgs e)
        {
            ConfigDualPage();
        }
    }
}
