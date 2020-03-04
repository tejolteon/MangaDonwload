using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace MDR.Source.Reader
{
    class Reader
    {
        public BitmapImage Loader(string directory) =>
            new BitmapImage(new Uri(directory));
    }
}
