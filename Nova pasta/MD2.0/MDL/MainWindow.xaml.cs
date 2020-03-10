using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace MDR
{
    using Source.Reader;

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

        int position = 0;
        Reader reader;
        int total = 0;

        private void Init()
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png" };

            if (dialog.ShowDialog() == true)
            {
                reader = new Reader();

                reader.Loader(dialog.FileName);
                total = reader.ImagesUrl.Length;

                reader.NextImage(position);
                imageLeft.Source = reader.Image;

                position++;
                reader.NextImage(position);

                if (position < total)
                    imageRight.Source = reader.Image;
            }
        }

        private void Win_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                position++;
                reader.NextImage(position);

                if (position < total)
                    imageLeft.Source = reader.Image;

                position++;
                reader.NextImage(position);

                if (position < total)
                    imageRight.Source = reader.Image;
            }
        }
    }
}
