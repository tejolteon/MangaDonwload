using MDR.Source.Reader;
using System.Windows;
using System.Windows.Input;

namespace MDR
{
    /// <summary>
    /// Lógica interna para Viewer.xaml
    /// </summary>
    public partial class Viewer : Window
    {
        public Viewer()
        {
            InitializeComponent();
            ConfigurationViewer.Begin();
            Image1.Source = ConfigurationViewer.CurrentImage;
            GridImage.RenderTransform = ConfigurationViewer.DefineTransform();
        }

        private void GridImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ConfigurationViewer.SetZoom(GridImage, e.Delta > 0 ? .2 : -.2);
        }

        private void GridImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ConfigurationViewer.MoveDown(GridImage);
        }

        private void GridImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ConfigurationViewer.MoveUp(GridImage);
        }

        private void GridImage_MouseMove(object sender, MouseEventArgs e)
        {
            ConfigurationViewer.MoveImage(GridImage);
        }

        private void GridImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ConfigurationViewer.GridSize(GridImage);
        }

        private void GridMain_MouseMove(object sender, MouseEventArgs e)
        {
            ConfigurationViewer.GridPosition = e.GetPosition(GridMain);
        }
    }
}
