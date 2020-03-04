using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            Reader reader = new Reader();

            imageLeft.Margin = new Thickness(grid.Width / 2, 0, 0, 0);
            imageRight.Margin = new Thickness(0, 0, grid.Width / 2, 0);
            imageLeft.Source = reader.Loader(@"C:\Users\tiago\Pictures\chinchila-cuidar-pet.jpg");

            imageRight.Source = reader.Loader(@"C:\Users\tiago\Pictures\dino.png");
        }
    }
}
