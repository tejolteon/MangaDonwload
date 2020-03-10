using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace MDR.Source.Reader
{
    class Reader
    {
        private string BaseDirectory { get; set; }
        public string[] ImagesUrl { get; private set; }
        public BitmapImage Image { get; private set; }

        public void Loader(string fileName)
        {
            BaseDirectory = Directory.GetParent(fileName).FullName;
            ImagesUrl = Directory.GetFiles(BaseDirectory);
        }

        public void NextImage(int position) =>
            Image = new BitmapImage(new Uri(ImagesUrl[position]));

    }
}
